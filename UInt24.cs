using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles
{
    class UInt24
    {
        public byte Byte1 { get; private set; }
        public byte Byte2 { get; private set; }
        public byte Byte3 { get; private set; }
        public UInt24(byte b1, byte b2, byte b3)
        {
            Byte1 = b1;
            Byte2 = b2;
            Byte3 = b3;
        }
        public static bool operator ==(UInt24 ui24, int i)
        {
            return (ui24.Byte1 + ui24.Byte2 <<8 + ui24.Byte3 << 16) ==   i;
        }
        public static bool operator ==(int i, UInt24 ui24)
        {
            return ui24 == i;
        }
        public static bool operator !=(UInt24 ui24, int i)
        {
            return !(ui24 == i);
        }
        public static bool operator !=(int i, UInt24 ui24)
        {
            return ui24 != i;
        }
    }
}
