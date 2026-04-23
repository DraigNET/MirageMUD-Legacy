using System;
using System.IO;
using System.Text;

namespace Shared.Networking
{
    public class PacketReader : IDisposable
    {
        private readonly MemoryStream _stream;
        private readonly BinaryReader _reader;

        public int Length { get; }
        public int Id { get; }

        public PacketReader(byte[] data)
        {
            _stream = new MemoryStream(data);
            _reader = new BinaryReader(_stream, Encoding.UTF8, true);

            Length = _reader.ReadInt32();
            Id = _reader.ReadInt32();
        }

        public int ReadInt() => _reader.ReadInt32();
        public long ReadLong() => _reader.ReadInt64();
        public string ReadString() => _reader.ReadString();
        public bool ReadBool() => _reader.ReadBoolean();
        public byte ReadByte() => _reader.ReadByte();
        public byte[] ReadBytes() => _reader.ReadBytes(_reader.ReadInt32());
        public byte[] ReadRemaining()
        {
            long remaining = _stream.Length - _stream.Position;
            return _reader.ReadBytes((int)remaining);
        }

        public void Dispose()
        {
            _reader.Dispose();
            _stream.Dispose();
        }
    }
}
