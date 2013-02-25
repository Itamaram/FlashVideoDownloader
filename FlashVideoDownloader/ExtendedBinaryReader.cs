using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    public class ExtendedBinaryReader : BinaryReader
    {
        public ExtendedBinaryReader(Stream s) : base(s) { }

        public string ReadNullTerminatedString()
        {
            List<byte> s = new List<byte>();
            for (byte b = ReadByte(); b != 0; b = ReadByte())
                s.Add(b);
            if (s.Count == 0)
                return null;
            return System.Text.Encoding.UTF8.GetString(s.ToArray());
        }

        public override short ReadInt16()
        {
            return ReadInt16BigEndian();
        }

        public override ushort ReadUInt16()
        {
            return ReadUInt16BigEndian();
        }

        public override int ReadInt32()
        {
            return ReadInt32BigEndian();
        }

        public override uint ReadUInt32()
        {
            return ReadUInt32BigEndian();
        }

        public override long ReadInt64()
        {
            return ReadInt64BigEndian();
        }

        public override ulong ReadUInt64()
        {
            return ReadUInt64BigEndian();
        }

        public UInt24 ReadUInt24()
        {
            return new UInt24(ReadByte(), ReadByte(), ReadByte());
        }

        //Did you know that ReadBoolean read a byte? I didn't. I ended up being very angry.
        //public bool[] ReadBits(int n)
        //{
        //    return Enumerable.Range(0, n).Select(i => ReadBoolean()).Reverse().ToArray();
        //}

        public static UInt64 BitsToValue(bool[] bits)
        {
            return bits.Select((b, i) => (b ? 1ul : 0ul) << i).Aggregate(0ul, (x, y) => x + y);
        }

        private T BitConverterWrapper<T>(Func<byte[], int, T> f, byte[] b)
        {
            if (BitConverter.IsLittleEndian)
                return f(b.Reverse().ToArray(), 0);
            else
                return f(b, 0);
        }

        public short ReadInt16BigEndian()
        {
            return BitConverterWrapper<short>(BitConverter.ToInt16, ReadBytes(2));
        }

        public ushort ReadUInt16BigEndian()
        {
            return BitConverterWrapper<ushort>(BitConverter.ToUInt16, ReadBytes(2));
        }

        public int ReadInt32BigEndian()
        {
            return BitConverterWrapper<int>(BitConverter.ToInt32, ReadBytes(4));
        }

        public uint ReadUInt32BigEndian()
        {
            return BitConverterWrapper<uint>(BitConverter.ToUInt32, ReadBytes(4));
        }

        public long ReadInt64BigEndian()
        {
            return BitConverterWrapper<long>(BitConverter.ToInt64, ReadBytes(8));
        }

        public ulong ReadUInt64BigEndian()
        {
            return BitConverterWrapper<ulong>(BitConverter.ToUInt64, ReadBytes(8));
        }

        /// <summary>
        /// Reads to the end of the stream
        /// </summary>
        /// <returns></returns>
        public byte[] ReadToEnd()
        {
            using (var ms = new MemoryStream())
            {
                BaseStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public IEnumerable<byte[]> ReadChunkedBytes(ulong u, int buffersize = 4096)
        {
            while (u > 0)
            {
                var bytesToRead = u < (ulong)buffersize ? (int)u : buffersize;
                u -= (ulong)bytesToRead;
                yield return ReadBytes(bytesToRead);
            }
        }

        public void SkipBytes(ulong count)
        {
            foreach (var b in ReadChunkedBytes(count)) ;
        }
    }
}
