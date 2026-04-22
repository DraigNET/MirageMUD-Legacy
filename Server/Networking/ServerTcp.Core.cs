using Server.Config;
using Shared.Networking;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Networking
{
    public partial class ServerTcp : IDisposable
    {
        private readonly TcpListener _listener;
        private readonly ConcurrentDictionary<int, ClientConnection> _clients = new();
        private int _nextClientId = 1;
        private CancellationTokenSource? _cts;

        public event Action<int, ClientPacketId, ReadOnlyMemory<byte>>? OnPacket;
        public event Action<int, string?, string?>? OnClientDisconnected;

        public ServerTcp() : this(IPAddress.Any, 5000) { } // match your client default port

        public ServerTcp(IPAddress ip, int port)
        {
            _listener = new TcpListener(ip, port);

            // If it's an IPv6 listener, enable DualMode so IPv4 connects too
            if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                _listener.Server.DualMode = true;

            _listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }
        public ServerTcp(ServerConfig config)
        {
            var ip = ParseListenIp(config.ListenIp);

            _listener = new TcpListener(ip, config.Port);

            if (ip.Equals(IPAddress.IPv6Any))
                _listener.Server.DualMode = true;    // accept both IPv4 & IPv6

            _listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }
        private static IPAddress ParseListenIp(string listenIp)
        {
            if (string.IsNullOrWhiteSpace(listenIp)) return IPAddress.Any;
            return listenIp switch
            {
                "0.0.0.0" => IPAddress.Any,
                "::" => IPAddress.IPv6Any,
                _ => IPAddress.Parse(listenIp)
            };
        }

        public void Start()
        {
            _cts = new CancellationTokenSource();
            _listener.Start();
            _ = AcceptLoopAsync(_cts.Token);
            Console.WriteLine($"[ServerTcp] Listening on {_listener.LocalEndpoint}");
        }

        public void Stop()
        {
            _cts?.Cancel();
            _listener.Stop();
            foreach (var kv in _clients)
                kv.Value.Dispose();
            _clients.Clear();
        }

        private async Task AcceptLoopAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var tcp = await _listener.AcceptTcpClientAsync(ct);
                    tcp.NoDelay = true;
                    var id = Interlocked.Increment(ref _nextClientId);
                    var conn = new ClientConnection(id, tcp);
                    if (!_clients.TryAdd(id, conn))
                    {
                        tcp.Dispose();
                        continue;
                    }
                    Console.WriteLine($"[ServerTcp] Client {id} connected.");
                    _ = ReceiveLoopAsync(conn, ct);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                Console.WriteLine($"[ServerTcp] AcceptLoop error: {ex}");
            }
        }

        private async Task ReceiveLoopAsync(ClientConnection c, CancellationToken ct)
        {
            try
            {
                var stream = c.Stream;
                var buf = new byte[8192];
                while (!ct.IsCancellationRequested && c.Tcp.Connected)
                {
                    int n = await stream.ReadAsync(buf.AsMemory(0, buf.Length), ct);
                    if (n <= 0) break;

                    c.Buffer.Append(buf, n);
                    while (c.Buffer.HasPackets)
                    {
                        var packetBytes = c.Buffer.PopPacket();
                        using var reader = new PacketReader(packetBytes);
                        var pid = (ClientPacketId)reader.Id;

                        // Forward raw payload to DataHandler
                        var payloadMem = reader.ReadRemaining();
                        OnPacket?.Invoke(c.Id, pid, payloadMem);
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                Console.WriteLine($"[ServerTcp] ReceiveLoop error (client {c.Id}): {ex}");
            }
            finally
            {
                Disconnect(c.Id);
            }
        }

        private void Disconnect(int clientId)
        {
            if (_clients.TryRemove(clientId, out var c))
            {
                var accountId = c.AccountId;
                var characterId = c.CharacterId;
                c.Dispose();
                OnClientDisconnected?.Invoke(clientId, accountId, characterId);
                Console.WriteLine($"[ServerTcp] Client {clientId} disconnected.");
            }
        }

        private sealed class ClientConnection : IDisposable
        {
            public int Id { get; }
            public string? AccountId { get; set; }
            public string? CharacterId { get; set; }
            public TcpClient Tcp { get; }
            public NetworkStream Stream { get; }
            public PacketBuffer Buffer { get; } = new();

            public ClientConnection(int id, TcpClient tcp)
            {
                Id = id;
                Tcp = tcp;
                Stream = tcp.GetStream();
            }

            public void Dispose()
            {
                Stream.Dispose();
                Tcp.Dispose();
            }
        }

        // Implements the SendBytes() declared in your other partial
        public void SendBytes(int clientId, byte[] data)
        {
            if (_clients.TryGetValue(clientId, out var c))
            {
                try
                {
                    c.Stream.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ServerTcp] Send error (client {clientId}): {ex}");
                    Disconnect(clientId);
                }
            }
        }

        public void Dispose() => Stop();
    }
}
