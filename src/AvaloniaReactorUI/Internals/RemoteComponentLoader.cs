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

    internal class HotReloadServer
    {
        private readonly int _listenPort;
        private CancellationTokenSource _cancellationTokenSource;
        private TcpListener _serverSocket;

        public HotReloadServer(int listenPort)
        {
            _listenPort = listenPort;
        }

        public async void Start()
        {
            lock (this)
            {
                if (_cancellationTokenSource != null)
                    return;

                _cancellationTokenSource = new CancellationTokenSource();
            }

            var cancellationToken = _cancellationTokenSource.Token;
            _serverSocket = new TcpListener(IPAddress.Loopback, _listenPort);

            try
            {
                _serverSocket.Start(1);

                while (!cancellationToken.IsCancellationRequested)
                {
                    var connectedClient = await _serverSocket.AcceptTcpClientAsync();

                    HandleClientConnect(connectedClient);
                }
            }
            catch (TaskCanceledException)
            {
                //stop called
            }
            catch (ObjectDisposedException)
            {
                //stop called
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            finally
            {
                _serverSocket.Stop();
                _serverSocket = null;

                lock (this)
                    _cancellationTokenSource = null;
            }
        }

        private void HandleClientConnect(TcpClient connectedClient)
        {
            try
            {
                connectedClient.ReceiveTimeout = 5000;
                using var sr = new StreamReader(connectedClient.GetStream());
                var command = sr.ReadLine().Split('|');
                if (command[0] == "RELOAD")
                {
                    HotReloadCommandIssued?.Invoke(this, command.Skip(1).ToArray());
                }
            }
            catch (Exception)
            { 
            }
        }

        public void Stop()
        {
            lock (this)
            {
                if (_cancellationTokenSource == null)
                    return;

                if (!_cancellationTokenSource.IsCancellationRequested)
                    _cancellationTokenSource.Cancel();
            }

            _serverSocket?.Stop();
        }

        public event EventHandler<string[]> HotReloadCommandIssued;
    }
}
