using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashVideoFiles.Structs;

namespace FlashVideoFiles.F4VBoxes
{
    /// <summary>
    /// The Edit List (elst) box contains an explicit time line map in the form of edit list entries. Each entry in the elst box defines a part of the presentation timeline through one of the following means:
    /// - By mapping a part of the media timeline
    /// - By indicating an empty time (that is, a gap)
    /// - By defining a dwell (that is, the location at which a single point of time is held for a period)
    /// An empty edit in an edit list box indicates a gap. To specify a starting offset for a track, insert an empty edit at the beginning of the track. 
    /// An empty edit shall not be the last edit in a track. If the duration specified in the movie header box is different from the track's actual duration, an implicit empty edit is placed at the end of the track. 
    /// The media should have a key frame immediately after the gap. Alternately, Flash Player can use the data in the Sync Sample box to find a key frame after a gap.
    /// </summary>
    class EditListBox :F4VBox
    {
        public override uint BoxType { get { return 0x656C7374; } }
        /// <summary>
        /// Either 0 or 1
        /// </summary>
        public byte Version { get; set; }
        /// <summary>
        /// Reserved. Set to 0
        /// </summary>
        public UInt24 Flags { get; set; }
        /// <summary>
        /// Number of entries in the edit list entry table
        /// </summary>
        public uint EntryCount { get; set; }
        /// <summary>
        /// An array of ElstRecord structures
        /// </summary>
        public ElstRecord[] EditListEntryTable { get; set; }
    }

    /// <summary>
    /// An Edit List Record
    /// </summary>
    struct ElstRecord
    {
        /// <summary>
        /// Duration of this edit segment, in TimeScale units defined in the Movie Header (moov) box.
        /// Is a Uint32 if Version == 0
        /// </summary>
        public UInt64 SegmentDuration { get; set; }
        /// <summary>
        /// Starting time within the media of this edit segment as composition time, in TimeScale units defined in the mdhd? box. A value of -1 specifies an empty edit.
        /// Is a Uint32 if Version == 0
        /// </summary>
        public UInt64 MediaTime { get; set; }
        /// <summary>
        /// Relative rate at which to play the media of this edit segment. The default value is 1. A value of 0 specifies dwell editing
        /// </summary>
        public short MediaRateInteger { get; set; }
        /// <summary>
        /// Reserved. Set to 0
        /// </summary>
        public short MediaRateFraction { get; set; }
    }
}
