using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    /// <summary>
    /// A consistent header that all boxes have
    /// </summary>
    class F4VBoxHeader
    {
        /// <summary>
        /// The total size of the box in bytes, including this header. 0 indicates that the box extends until the end of the file.
        /// </summary>
        public uint TotalSize { get; private set; }
        /// <summary>
        /// The type of the box, usually as 4CC code
        /// </summary>
        public uint BoxType { get; private set; }
        /// <summary>
        /// The total 64-bit length of the box in bytes, including this header, only used if TotalSize == 1
        /// </summary>
        public UInt64? ExtendedSize { get; private set; }

        public void Parse(Stream s)
        {
            using (var br = new BinaryReader(s))
            {
                TotalSize = br.ReadUInt32();
                BoxType = br.ReadUInt32();
                if (TotalSize == 1)
                    ExtendedSize = br.ReadUInt64();
                else
                    ExtendedSize = null;
            }

        }
    }
}
