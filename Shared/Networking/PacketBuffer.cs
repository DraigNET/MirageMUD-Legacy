using System;
using System.Collections.Generic;
using System.IO;

namespace Shared.Networking
{
    /// <summary>
    /// Handles assembling full packets from a TCP stream.
    /// </summary>
    public sealed class PacketBuffer
    {
        private readonly MemoryStream _stream = new();
        private readonly Queue<byte[]> _completedPackets = new();

        /// <summary>
        /// Append newly received bytes into the buffer.
        /// </summary>
        public void Append(byte[] data, int count)
        {
            _stream.Seek(0, SeekOrigin.End);
            _stream.Write(data, 0, count);
            _stream.Seek(0, SeekOrigin.Begin);
            TryExtractPackets();
        }

        /// <summary>
        /// Returns true if there are any full packets ready.
        /// </summary>
        public bool HasPackets => _completedPackets.Count > 0;

        /// <summary>
        /// Pops the next completed packet.
        /// </summary>
        public byte[] PopPacket()
        {
            if (_completedPackets.Count == 0)
                throw new InvalidOperationException("No packets available.");
            return _completedPackets.Dequeue();
        }

        private void TryExtractPackets()
        {
            var lenBuf = new byte[4];

            while (_stream.Length - _stream.Position >= 4)
            {
                long startPos = _stream.Position;

                // Peek length (do not advance permanently)
                _stream.Read(lenBuf, 0, lenBuf.Length);
                int length = BitConverter.ToInt32(lenBuf);

                if (_stream.Length - _stream.Position >= length)
                {
                    // Read packet bytes (length + data)
                    byte[] packet = new byte[4 + length];
                    _stream.Position = startPos;
                    _stream.Read(packet, 0, packet.Length);

                    _completedPackets.Enqueue(packet);
                }
                else
                {
                    // Not enough data yet, rewind
                    _stream.Position = startPos;
                    break;
                }
            }

            // Keep remaining bytes (if partial packet) in a new stream
            if (_stream.Position > 0 && _stream.Position < _stream.Length)
            {
                byte[] leftover = new byte[_stream.Length - _stream.Position];
                _stream.Read(leftover, 0, leftover.Length);
                _stream.SetLength(0);
                _stream.Write(leftover, 0, leftover.Length);
            }
            else if (_stream.Position >= _stream.Length)
            {
                _stream.SetLength(0);
            }

            _stream.Position = _stream.Length; // move to end for next Append
        }
    }
}
