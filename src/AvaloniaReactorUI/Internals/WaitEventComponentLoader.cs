using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaReactorUI.Internals
{
    internal class WaitEventComponentLoader : IComponentLoader
    {
        private EventWaitHandle _hotReloadEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "AvaloniaReactorUI.HotReload");
        private Thread? _waitThread = null;
        private bool _exitFlag = false;
        private readonly string _assemblyFileName;

        public WaitEventComponentLoader(string assemblyFileName)
        {
            _assemblyFileName = assemblyFileName;
        }

        public event EventHandler? ComponentAssemblyChanged;

        public RxComponent LoadComponent<T>() where T : RxComponent, new()
        {
            //var assemblyPath = _assemblyFileName;
            //var assemblyPdbPath = Path.Combine(
            //    Path.GetDirectoryName(assemblyPath) ?? throw new InvalidOperationException($"Unable to get directory name of {assemblyPath}"),
            //    Path.GetFileNameWithoutExtension(assemblyPath) + ".pdb");

            //var assembly = File.Exists(assemblyPdbPath) ?
            //    Assembly.Load(Utils.ReadFileBytesWithoutLock(assemblyPath))
            //    :
            //    Assembly.Load(Utils.ReadFileBytesWithoutLock(assemblyPath), Utils.ReadFileBytesWithoutLock(assemblyPdbPath));
            var assembly = Utils.LoadAssemblyWithoutLock(_assemblyFileName);

            //var assembly = Utils.LoadAssemblyWithoutLock(assemblyPath);

            var type = assembly.GetType(typeof(T).FullName ?? throw new InvalidOperationException("Unable to get component type full name"))
                ?? throw new InvalidOperationException("Unable to get type of the component to load");

            return (RxComponent)(Activator.CreateInstance(type) ?? throw new InvalidOperationException($"Unable to create instance of type {type}"));
        }

        public void Run()
        {
            if (_waitThread == null)
            {
                _exitFlag = false;
                _waitThread = new Thread(new ThreadStart(this.WaitHandleFunc));
                _waitThread.Start();
            }
        }

        private void WaitHandleFunc()
        {
            while (!_exitFlag)
            {
                _hotReloadEvent.WaitOne();

                if (!_exitFlag)
                {
                    ComponentAssemblyChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void Stop()
        {
            _exitFlag = true;
            _hotReloadEvent.Set();
        }
    }
}
