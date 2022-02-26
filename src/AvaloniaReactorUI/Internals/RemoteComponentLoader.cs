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
        public event EventHandler? ComponentAssemblyChanged;

        private readonly HotReloadServer _server;

        private Assembly? _assembly;

        public RxComponent? LoadComponent<T>() where T : RxComponent, new()
        {
            if (_assembly == null)
                return new T();

            var type = _assembly.GetType(typeof(T).FullName ?? throw new InvalidOperationException());

            if (type == null)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Unable to hot reload component {typeof(T).FullName}: type not found in received assembly");
                return null;
                //throw new InvalidOperationException($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
            }

            return (RxComponent?)(Activator.CreateInstance(type) ?? throw new InvalidOperationException());
        }

        public RemoteComponentLoader()
        {
            _server = new HotReloadServer(ReceivedAssemblyFromHost);
        }

        private void ReceivedAssemblyFromHost(Assembly newAssembly)
        {
            _assembly = newAssembly;
            ComponentAssemblyChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Run()
        {
            _server.Run();
        }

        public void Stop()
        {
            _server.Stop();
        }
    }


}
