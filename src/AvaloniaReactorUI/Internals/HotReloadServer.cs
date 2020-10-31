using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaReactorUI.Internals
{
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
