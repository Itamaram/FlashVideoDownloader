using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles.F4VBoxes
{
    /// <summary>
    /// Each Track (trak) box corresponds to an individual media track within the F4V file and contains boxes that further define the properties of the media track
    /// </summary>
    class TrackBox :F4VBox
    {
        /// <summary>
        /// BoxType = 'trak'
        /// </summary>
        public override uint BoxType { get { return 0x7472616B; } }
        /// <summary>
        /// Arbitrary number of boxes that define the media track
        /// </summary>
        public F4VBox[] Boxes { get; set; }
    }
}
