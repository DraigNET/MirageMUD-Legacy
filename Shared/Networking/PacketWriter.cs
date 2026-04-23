using System;
using System.IO;
using System.Text;

namespace Shared.Networking
{
    public class PacketWriter : IDisposable
    {
        private readonly MemoryStream _stream;
        private readonly BinaryWriter _writer;

        public PacketWriter(int packetId)
        {
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream, Encoding.UTF8, true);

            // Reserve 4 bytes for length (will overwrite later)
            _writer.Write(0);
            _writer.Write(packetId);
        }

        public void Write(int value) => _writer.Write(value);
        public void Write(long value) => _writer.Write(value);
        public void Write(string value) => _writer.Write(value ?? string.Empty);
        public void Write(bool value) => _writer.Write(value);
        public void Write(byte value) => _writer.Write(value);
        public void Write(byte[] value)
        {
            _writer.Write(value.Length);
            _writer.Write(value);
        }

        public byte[] ToArray()
        {
            _writer.Flush();

            // Fill in the packet length (excluding the length field itself)
            int length = (int)_stream.Length - 4;
            _stream.Position = 0;
            _writer.Write(length);

            return _stream.ToArray();
        }

        public void Dispose()
        {
            _writer.Dispose();
            _stream.Dispose();
        }
    }
}
