using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles.F4VBoxes
{
    class F4VBoxHeader
    {
        /// <summary>
        /// The total size of the box in bytes, including this header. 0 indicates that the box extends until the end of the file.
        /// </summary>
        public uint TotalSize { get; private set; }
        /// <summary>
        /// The type of the box, usually as Four-character ASCII code
        /// </summary>
        public uint BoxType { get; private set; }
        /// <summary>
        /// The total 64-bit length of the box in bytes, including this header, only used if TotalSize == 1
        /// </summary>
        public uint? ExtendedSize { get; private set; }
    }
}
