using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    public class FLVFile
    {
        public byte[] Signature { get { return new byte[] { (byte)'F', (byte)'L', (byte)'V' }; } }

        public byte Version { get { return 1; } }

        public byte TypeFlagReserved { get { return 0; } }

        public bool TypeFlagsAudio { get; set; }

        public bool TypeFlagsResrved2 { get { return false; } }

        public bool TypeFlagVideo { get; set; }

        public uint DataOffset { get { return 9; } }

        public void WriteToFile(BinaryWriter bw)
        {
            bw.Write(Signature);
            bw.Write(Version);
            var magicByte = (TypeFlagReserved << 3);
            magicByte |= (TypeFlagsAudio ? 1 : 0) << 2;
            magicByte |= (TypeFlagsResrved2 ? 1 : 0) << 1;
            magicByte |= (TypeFlagVideo ? 1 : 0);
            bw.Write((byte)magicByte);
            bw.Write(Enumerable.Range(0, 4).Select(i => (byte)((DataOffset & (0xff << (8 * i))) >> (8 * i))).Reverse().ToArray());
        }
    }
}
