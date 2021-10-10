using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
//using Microsoft.Build.Evaluation;
//using Microsoft.Build.Execution;
//using Microsoft.Build.Framework;
//using Microsoft.Build.Logging;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace AvaloniaReactorUI.HotReloadVsExtension
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ReloadCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("2ccafafa-95a3-4656-901e-80d77157987b");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private ReloadCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ReloadCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        private EnvDTE.DTE _dte;
        private IVsOutputWindow _outputWindow;
        private Events _events;
        private BuildEvents _buildEvents;
        private CommandEvents _commandEvents;
        private Commands2 _commands;
        private EventWaitHandle _hotReloadEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "AvaloniaReactorUI.HotReload");
        private bool _fullBuildRequired = true;

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in ReloadCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ReloadCommand(package, commandService);

            if (!(await package.GetServiceAsync(typeof(EnvDTE.DTE)) is EnvDTE.DTE dte))
                return;

            if (!(await package.GetServiceAsync(typeof(SVsOutputWindow)) is IVsOutputWindow outputWindow))
                return;

            //IVsOutputWindow outWindow = package.GetServiceAsync(typeof(SVsOutputWindow)) as IVsOutputWindow;

            Instance._dte = dte;
            Instance._outputWindow = outputWindow;

            Instance._events = dte.Events;
            Instance._buildEvents = Instance._events.BuildEvents;
            Instance._commandEvents = Instance._events.get_CommandEvents(null, 0);
            Instance._commands = dte.Commands as EnvDTE80.Commands2;

            Instance._commandEvents.BeforeExecute += Instance.OnBeforeExecute;
            Instance._commandEvents.AfterExecute += Instance.OnAfterExecute;

            Instance._buildEvents.OnBuildDone += Instance.OnBuildDone;
            Instance._buildEvents.OnBuildBegin += Instance.OnBuildBegin;
        }

        private void OnBuildBegin(vsBuildScope Scope, vsBuildAction Action)
        {

        }

        private void OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            _fullBuildRequired = true;
        }

        private void OnBeforeExecute(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
        {

        }

        private void OnAfterExecute(string Guid, int ID, object CustomIn, object CustomOut)
        {
        }

        private string GetCommandName(string Guid, int ID)
        {
            if (Guid == null)
                return "null";

            string result = "";
            if (Instance._commands != null)
            {
                try
                {
                    return Instance._commands.Item(Guid, ID).Name;
                }
                catch (System.Exception)
                {
                }
            }
            return result;
        }

        private IReadOnlyList<EnvDTE.Project> GetAvaloniaProjects()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var projectsFound = new List<EnvDTE.Project>();

            var allProjectsInSolution = _dte.Solution.Projects;
            foreach (var project in allProjectsInSolution.Cast<EnvDTE.Project>())
            {
                if (project.Properties == null)
                    continue;

                var vsproject = project.Object as VSLangProj.VSProject;

                var referenceReactorUIHotReloadPackage = vsproject.References.Cast<VSLangProj.Reference>().Any(_ => _.Name == "AvaloniaReactorUI");
                if (!referenceReactorUIHotReloadPackage)
                    continue;

                projectsFound.Add(project);
            }

            return projectsFound;
        }

        private IVsOutputWindowPane GetVsOutputWindow()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            Guid generalPaneGuid = VSConstants.GUID_OutWindowDebugPane; // P.S. There's also the GUID_OutWindowDebugPane available. (GUID_OutWindowGeneralPane)
            _outputWindow.GetPane(ref generalPaneGuid, out IVsOutputWindowPane generalPane);

            return generalPane;
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            //ThreadHelper.ThrowIfNotOnUIThread();
            var generalPane = GetVsOutputWindow();

            var selectedProject = GetAvaloniaProjects().FirstOrDefault();

            if (selectedProject == null)
            {
                generalPane.OutputString($"Solution doesn't contain a valid ReactorUI hot reload project{Environment.NewLine}");
                generalPane.OutputString($"1) Ensure it references AvaloniaReactorUI{Environment.NewLine}");
                generalPane.OutputString($"2) Ensure that Visual Studio has finished loading the solution{Environment.NewLine}");
                generalPane.Activate(); // Brings this pane into view
                return;
            }

            string projectPath = selectedProject.FullName;

            var outputFilePath = Path.Combine(Path.GetDirectoryName(projectPath),
                selectedProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString(),
                selectedProject.Properties.Item("OutputFileName").Value.ToString());

            if (Path.GetExtension(outputFilePath) != ".dll")
                return;

            var now = DateTime.Now;

            generalPane.Activate(); // Brings this pane into view
            generalPane.OutputString($"Building {outputFilePath}...{Environment.NewLine}");
            generalPane.Activate(); // Brings this pane into view

            //_dte.Solution.SolutionBuild.BuildProject(selectedProject.ConfigurationManager.ActiveConfiguration.ConfigurationName, selectedProject.UniqueName, true);

            _dte.Documents.SaveAll();

            if (await TryBuildProjectAsync(Path.GetDirectoryName(projectPath), outputFilePath, generalPane, _fullBuildRequired))
            {
                _fullBuildRequired = false;
            }

            //if (!RunMsBuild(selectedProject, generalPane))
            //{
            //    // Show a message box to inform user that build was completed with errors
            //    VsShellUtilities.ShowMessageBox(
            //        this.package,
            //        "Build FAILED with errors: please review them in the output window and try again",
            //        "ReactorUI Hot Reload",
            //        OLEMSGICON.OLEMSGICON_WARNING,
            //        OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            //    generalPane.OutputString($"Unable to build Avalonia project, it may contains errors{Environment.NewLine}");
            //    generalPane.Activate(); // Brings this pane into view
            //    return;
            //}
        }

        private bool RunMsBuild(Project project, IVsOutputWindowPane outputPane)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var parameters = new Microsoft.Build.Execution.BuildParameters(new Microsoft.Build.Evaluation.ProjectCollection())
            {
                //Loggers = new Microsoft.Build.Framework.ILogger[] { new Microsoft.Build.Logging.ConsoleLogger() }
                Loggers = new Microsoft.Build.Framework.ILogger[] { new OutputPaneLogger(outputPane) }
            };
            var globalProperty = new Dictionary<string, string>() {
                {"Configuration", project.ConfigurationManager.ActiveConfiguration.ConfigurationName },
                //{"Platform", project.ConfigurationManager.ActiveConfiguration.PlatformName },
            };

            var result = Microsoft.Build.Execution.BuildManager.DefaultBuildManager.Build(
                parameters,
                new Microsoft.Build.Execution.BuildRequestData(project.FullName, globalProperty, null, new[] { "Build" }, null));

            return result.OverallResult == Microsoft.Build.Execution.BuildResultCode.Success;
        }

        private async Task<bool> TryBuildProjectAsync(string projectFolder, string assemblyPath, IVsOutputWindowPane outputPane, bool fullBuild = true)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (projectFolder == null || assemblyPath == null)
            {
                throw new InvalidOperationException();
            }

            try
            {
                var outputFolder = Path.Combine(projectFolder, $"bin/AvaloniaReactorUI/temp_generated");

                var cmdLine = $"build {(!fullBuild ? "--no-restore --no-dependencies" : "")} --output \"{outputFolder}\"";
                outputPane.OutputString($"Executing 'dotnet {cmdLine}'");
                await RunDotnetAndWaitForExit(cmdLine, projectFolder, outputPane);
                
                var pdbAssemblyName = Path.GetFileNameWithoutExtension(assemblyPath) + ".pdb";
                var pdbAssemblyPath = Path.Combine(Path.GetDirectoryName(assemblyPath) ?? throw new InvalidOperationException(), pdbAssemblyName);
                var generatedPdbFilePath = Path.Combine(outputFolder, pdbAssemblyName);

                outputPane.OutputString($"Copy from {generatedPdbFilePath} to {pdbAssemblyPath}{Environment.NewLine}");
                outputPane.Activate(); // Brings this pane into view
                File.Copy(generatedPdbFilePath, pdbAssemblyPath, true);

                var assemblyName = Path.GetFileName(assemblyPath);
                var generatedFilePath = Path.Combine(outputFolder, assemblyName);

                outputPane.OutputString($"Copy from {generatedFilePath} to {assemblyPath}{Environment.NewLine}");
                outputPane.Activate(); // Brings this pane into view
                File.Copy(generatedFilePath, assemblyPath, true);

                _hotReloadEvent.Set();

                return true;
            }
            catch (Exception ex)
            {
                //error while running dotnet build...
                outputPane.OutputStringThreadSafe(ex.ToString());
                outputPane.Activate(); // Brings this pane into view
                return false;
            }
        }

        private static async Task RunDotnetAndWaitForExit(string cmdLine, string workingDirectory, IVsOutputWindowPane outputPane)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var process = new System.Diagnostics.Process();

            process.StartInfo.FileName = "dotnet";
            process.StartInfo.Arguments = cmdLine;
            process.StartInfo.WorkingDirectory = workingDirectory;

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            process.ErrorDataReceived += (sender, args) =>
            {
                outputPane.OutputStringThreadSafe(args.Data + Environment.NewLine);
                outputPane.Activate(); // Brings this pane into view
            };

            process.OutputDataReceived += (sender, args) =>
            {
                outputPane.OutputStringThreadSafe(args.Data + Environment.NewLine);
                outputPane.Activate(); // Brings this pane into view
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await WaitForExitAsync(process);
        }

        /// <summary>
        /// Waits asynchronously for the process to exit.
        /// </summary>
        /// <param name="process">The process to wait for cancellation.</param>
        /// <param name="cancellationToken">A cancellation token. If invoked, the task will return 
        /// immediately as canceled.</param>
        /// <returns>A Task representing waiting for the process to end.</returns>
        private static Task WaitForExitAsync(System.Diagnostics.Process process,
            CancellationToken cancellationToken = default)
        {
            if (process.HasExited) return Task.CompletedTask;

            var tcs = new TaskCompletionSource<object>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.TrySetResult(null);
            if (cancellationToken != default(CancellationToken))
                cancellationToken.Register(() => tcs.SetCanceled());

            return process.HasExited ? Task.CompletedTask : tcs.Task;
        }
    }
}
