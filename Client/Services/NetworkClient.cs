using Client.Config;
using Client.Game;
using Shared.Networking;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client.Services
{
    public sealed class NetworkClient : IDisposable
    {
        private readonly TcpClient _tcpClient = new();
        private NetworkStream? _stream;
        private readonly PacketBuffer _buffer = new();

        public bool IsConnected => _tcpClient.Connected;

        public async Task<bool> TryConnectAsync(ClientConfig config)
        {
            try
            {
                await ConnectAsync(config);   // your existing method
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task ConnectAsync(ClientConfig config)
        {
            if (IsConnected) return;

            await _tcpClient.ConnectAsync(config.ServerHost, config.ServerPort);
            _stream = _tcpClient.GetStream();
            Console.WriteLine($"[NetworkClient] Connected to {config.ServerHost}:{config.ServerPort}");

            _ = ReceiveLoop();
        }

        private async Task ReceiveLoop()
        {
            if (_stream == null) return;

            byte[] recvBuf = new byte[4096];

            try
            {
                while (true)
                {
                    int bytes = await _stream.ReadAsync(recvBuf, 0, recvBuf.Length);
                    if (bytes == 0)
                    {
                        Console.WriteLine("[NetworkClient] Server closed connection.");
                        break;
                    }

                    Console.WriteLine($"[NetworkClient] Received {bytes} bytes");

                    _buffer.Append(recvBuf, bytes);

                    while (_buffer.HasPackets)
                    {
                        var raw = _buffer.PopPacket();
                        Console.WriteLine($"[NetworkClient] Dispatching packet of {raw.Length} bytes");
                        using var reader = new PacketReader(raw);
                        DataHandler.Handle(reader, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NetworkClient] Error: {ex.Message}");
            }
        }

        public async Task SendAsync(int packetId, Action<PacketWriter> buildPacket)
        {
            if (_stream == null) throw new InvalidOperationException("Not connected.");

            using var writer = new PacketWriter(packetId); // <-- set ID here
            buildPacket(writer);
            var data = writer.ToArray();
            await _stream.WriteAsync(data, 0, data.Length);
        }

        public void Dispose()
        {
            Console.WriteLine("[NetworkClient] Dispose() called!");
            _stream?.Dispose();
            _tcpClient.Dispose();
        }
    }
}