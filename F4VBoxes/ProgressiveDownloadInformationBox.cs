using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles.F4VBoxes
{
    /// <summary>
    /// The Progressive Download Information (pdin) box defines information about progressive download.
    /// The payload of a pdin box provides hints about how much data to download before a player can safely begin playback.
    /// </summary>
    class ProgressiveDownloadInformationBox : F4VBox
    {
        /// <summary>
        /// BoxType = 'pdin' 
        /// </summary>
        public override uint BoxType { get { return 0x7064696E; } }
        /// <summary>
        /// Expected to be 0
        /// </summary>
        public byte Version { get; set; }
        /// <summary>
        /// Reserved. Set to 0
        /// </summary>
        public uint Flags { get; set; }
        /// <summary>
        /// Arbitrary number of RATEDELAY records, until the end of the box
        /// </summary>
        public RateDelay[] RateDelay { get; set; }
    }

    /// <summary>
    /// For more information, see section 8.1.3 of ISO/IEC 14496-12:2008.
    /// </summary>
    class RateDelay{
        /// <summary>
        /// The rate (in bytes/second) to be considered for this record
        /// </summary>
        public uint Rate{get;set;}
        /// <summary>
        /// The number of milliseconds to delay before beginning playback at this rate
        /// </summary>
        public uint InitialDelay{get;set;}
    }
}
