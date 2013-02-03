using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    abstract class BinaryReaderHelper
    {
        public static string ReadNullTerminatedString(BinaryReader br)
        {
            var str = "";
            for (char c = br.ReadChar(); (int)c != 0; c = br.ReadChar())
                str += c;
            return str;
        }

        public static UInt24 ReadUInt24(BinaryReader br)
        {
            return new UInt24(br.ReadByte(), br.ReadByte(), br.ReadByte());
        }

        public static UInt64 ReadNBitsUnsigned(BinaryReader br, int n)
        {
            UInt64 result = 0;
            for (int i = 0; i < n; i++)
            {
                result = result << 1;
                result += br.ReadBoolean() ? (ulong)1 : 0;
            }
            return result;
        }
    }
}
