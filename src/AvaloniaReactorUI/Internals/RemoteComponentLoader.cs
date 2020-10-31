using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaReactorUI.Internals
{
    internal class RemoteComponentLoader : IComponentLoader
    {
        private Assembly[] _latestAssemblies;
        private readonly HotReloadServer _hotReloadServer;

        public static IComponentLoader Instance { get; set; } = new RemoteComponentLoader();

        public RemoteComponentLoader(int serverPort = 45821)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Only one instance of RxApplication is permitted");
            }

            Instance = this;
            _hotReloadServer = new HotReloadServer(serverPort);
        }

        private void OnHotReloadCommandIssued(object sender, string[] assemblies)
        {
            _latestAssemblies = assemblies.Select(_ => Assembly.LoadFrom(_)).ToArray();
            ComponentAssemblyChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Run()
        {
            _hotReloadServer.HotReloadCommandIssued += OnHotReloadCommandIssued;
            _hotReloadServer.Start();
        }

        public void Stop()
        {
            _hotReloadServer.HotReloadCommandIssued -= OnHotReloadCommandIssued;
            _hotReloadServer.Stop();
        }

        public event EventHandler ComponentAssemblyChanged;

        public RxComponent LoadComponent<T>() where T : RxComponent, new()
        {
            if (_latestAssemblies == null)
                return new T();

            foreach (var assembly in _latestAssemblies)
            {
                var type = assembly.GetType(typeof(T).FullName);

                if (type != null)
                {
                    return (RxComponent)Activator.CreateInstance(type);
                }
            }

            return null;
        }
    }

}
