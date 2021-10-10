using AvaloniaReactorUI.Internals;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AvaloniaReactorUI.Host
{
    class Program
    {
        public static int Main(string[] args)
        {
            if (args == null ||
                args.Length == 0)
            {
                Console.WriteLine("[AvaloniaReactorUI] Specify the assembly path to load");
                return -1;
            }

            var assemblyPath = args[0];

            if (string.IsNullOrWhiteSpace(assemblyPath))
            {
                Console.WriteLine($"[AvaloniaReactorUI] Invalid assembly path: empty string passed");
                return -1;
            }

            bool isDll = Path.GetExtension(assemblyPath).ToLowerInvariant() == ".dll";
            if (!isDll)
            {
                Console.WriteLine($"[AvaloniaReactorUI] WARNING: Assembly path extension is not '.dll'");
            }

            if (!Path.IsPathRooted(assemblyPath))
            {
                var currentAssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                    ?? throw new InvalidOperationException("Unable to get current assembly folder path");

                assemblyPath = Path.Combine(currentAssemblyFolder, assemblyPath);
            }

            if (!File.Exists(assemblyPath))
            {
                Console.WriteLine($"[AvaloniaReactorUI] The assembly '{assemblyPath}' doesn't exist");
                return -1;
            }

            string folderPath = Path.GetDirectoryName(assemblyPath) ?? throw new InvalidOperationException("Unable to get path of the assembly to hot-reload");
            string assemblyName = Path.GetFileNameWithoutExtension(assemblyPath);

            Console.WriteLine($"[AvaloniaReactorUI] Loading assembly '{assemblyPath}'");

            //<assemblyName>.deps.json --> AvaloniaReactorUI.Host.deps.json
            Console.WriteLine($"[AvaloniaReactorUI] Copying assembly '{Path.Combine(folderPath, assemblyName + ".deps.json")}' to '{Path.Combine(folderPath, "AvaloniaReactorUI.Host.deps.json")}'");
            File.Copy(Path.Combine(folderPath, assemblyName + ".deps.json"), Path.Combine(folderPath, "AvaloniaReactorUI.Host.deps.json"), true);

            Assembly? assembly;
            try
            {
                assembly = Utils.LoadAssemblyWithoutLock(assemblyPath);
            }
            catch (BadImageFormatException ex)
            {
                throw new InvalidOperationException($"Unable to load assembly '{assemblyPath}'{(!isDll ? "Isn't a dll?" : string.Empty)}", ex);
            }

            Environment.SetEnvironmentVariable("AvaloniaReactorUI.Host.HotReloadAssembly", assemblyPath);

            AppDomain currentDomain = AppDomain.CurrentDomain;

            currentDomain.AssemblyResolve += (object? sender, ResolveEventArgs args) =>
            {
                string assemblyPath = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");

                if (!File.Exists(assemblyPath))
                {
                    Console.WriteLine($"[AvaloniaReactorUI] Unable to load '{assemblyPath}'");
                    return null;
                }

                var assembly = Utils.LoadAssemblyWithoutLock(assemblyPath);
                Console.WriteLine($"[AvaloniaReactorUI] Loaded '{assemblyPath}'");
                return assembly;
            };

            var mainMethod = ((assembly ?? throw new InvalidOperationException())
                .EntryPoint ?? throw new InvalidOperationException())
                .Invoke(null, new object[] { args.Skip(1).ToArray() });

            return 0;
        }


    }
}
