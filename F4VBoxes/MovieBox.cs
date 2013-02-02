using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles.F4VBoxes
{
    /// <summary>
    /// The Movie (moov) box is effectively the “header” of an F4V file. The moov box itself contains one or more other boxes, which in turn contain other boxes, which define the structure of the F4V data
    /// </summary>
    class MovieBox : F4VBox
    {
        /// <summary>
        /// BoxType = 'moov'
        /// </summary>
        public override uint BoxType { get { return 0x6D6F6F76; } }
        /// <summary>
        /// Arbitrary number of boxes that define the file structure
        /// </summary>
        public F4VBox[] Boxes { get; set; }
    }
}
