using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles.F4VBoxes
{
    /// <summary>
    /// The File Type (ftyp) box helps identify the features that a program needs to support to play a particular file
    /// </summary>
    class FileTypeBox : F4VBox
    {
        /// <summary>
        /// BoxType = 'ftyp'
        /// </summary>
        public override uint BoxType { get { return 0x66747970; } }
        /// <summary>
        /// The primary brand identifier. For an F4V file, MajorBrand is 'f4v' (0x66347620)
        /// </summary>
        public UInt32 MajorBrand { get; set; }
        /// <summary>
        /// MinorVersion is informative only. It shall not be used to determine the conformance of a file to a standard. It may allow for more precise identification of the major brand for inspection, debugging, or improved decoding.
        /// </summary>
        public UInt32 MinorVersion { get; set; }
        /// <summary>
        /// Arbitrary number of compatible brands, until the end of the box
        /// </summary>
        public UInt32[] CompatibleBrands { get; set; }
    }
}
