using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    /// <summary>
    /// The Fragment Run Table (afrt) box can be used to find the fragment that contains a sample corresponding to a given time. 
    /// Fragments are individually identifiable by the URL scheme. Fragments may vary both in duration and in number of samples. The Durations of the Fragments are stored in the afrt box. 
    /// The afrt box uses a compact encoding:
    ///  - A Fragment Run Table may represent fragments for more than one quality level
    ///  - The Fragment Run Table is compactly coded, as each entry gives the first fragment number for a run of fragments with the same duration. The count of fragments having this same duration can be calculated by subtracting the first fragment number in this entry from the first fragment number in the next entry.
    ///  There may be several Fragment Run Table boxes in one Bootstrap Info box, each for different quality levels.
    /// </summary>
    public class FragmentRunTableBox : F4VBox
    {
        /// <summary>
        /// Either 0 or 1
        /// </summary>
        public byte Version { get; private set; }

        /// <summary>
        /// The following values are defined:
        /// 0 = A full table.
        /// 1 = The records in this table are updates (or new entries to be appended) to the previously defined Fragment Run Table. The Update flag in the containing Bootstrap Info box shall be 1 when this flag is 1.
        /// </summary>
        public UInt24 Flags { get; private set; }

        /// <summary>
        /// The number of time units per second, used in the FirstFragmentTimestamp and FragmentDuration fields. Typically, the value is 1.
        /// </summary>
        public uint TimeScale { get; private set; }

        /// <summary>
        /// The number of QualitySegmentUrlModifiers (quality level references) that follow. If 0, this Fragment Run Table applies to all quality levels, and there shall be only one Fragment Run Table box in the Bootstrap Info box
        /// </summary>
        public byte QualityEntryCount { get; private set; }

        /// <summary>
        /// An array of names of the quality levels that this table applies to. The names are null-terminated UTF-8 strings. The array shall be a subset of the QualityEntryTable in the Bootstrap Info (abst) box. The names shall not be present in any other Fragment Run Table in the Bootstrap Info box
        /// </summary>
        public string[] QualitySegmentUrlModifiers { get; private set; }

        /// <summary>
        /// The number of items in this FragmentRunEntryTable. The minimum value is 1.
        /// </summary>
        public uint FragmentRunEntryCount { get; private set; }

        /// <summary>
        /// Array of fragment run entries 
        /// </summary>
        public FragmentRunEntry[] FragmentRunEntryTable { get; private set; }

        public override void Parse(ExtendedBinaryReader br)
        {
            base.Parse(br);

            Version = br.ReadByte();
            Flags = br.ReadUInt24();
            TimeScale = br.ReadUInt32();
            QualityEntryCount = br.ReadByte();
            QualitySegmentUrlModifiers = Enumerable.Range(0, QualityEntryCount).Select(i => br.ReadNullTerminatedString()).ToArray();
            FragmentRunEntryCount = br.ReadUInt32();
            FragmentRunEntryTable = new FragmentRunEntry[FragmentRunEntryCount];
            for (uint i = 0; i < FragmentRunEntryCount; i++)
                FragmentRunEntryTable[i] = FragmentRunEntry.Parse(br);
        }
    }

    public class FragmentRunEntry
    {
        /// <summary>
        /// The identifying number of the first fragment in this run of fragments with the same duration. The fragment corresponding to the FirstFragment in the next FragmentRunEntry will terminate this run. 
        /// </summary>
        public uint FirstFragment{get;private set;}

        /// <summary>
        /// The timestamp of the FirstFragment, in TimeScale units. This field ensures that the fragment timestamps can be accurately represented at the beginning. It also ensures that the timestamps are synchronized when drifts occur due to duration accuracy or timestamp discontinuities
        /// </summary>
        public UInt64 FirstFragmentTimestamp{get;private set;}

        /// <summary>
        /// The duration, in TimeScale units, of each fragment in this run
        /// </summary>
        public uint FragmentDuration{get;private set;}

        /// <summary>
        /// Indicates discontinuities in timestamps, fragment numbers, or both. This field is also used to identify the end of a (live) presentation.
        /// The following values are defined:
        /// 0 = end of presentation.
        /// 1 = a discontinuity in fragment numbering.
        /// 2 = a discontinuity in timestamps.
        /// 3 = a discontinuity in both timestamps and fragment numbering.
        /// All other values are reserved.
        /// Signaling the end of the presentation in-band is useful in live scenarios. Gaps in the presentation are signaled as a run of zero duration fragments with both fragment number and timestamp discontinuities. Fragment number discontinuities are useful to signal jumps in fragment numbering schemes with no discontinuities in the presentation.
        /// </summary>
        public byte? DiscontinuityIndicator{get;private set;}

        public static FragmentRunEntry Parse(ExtendedBinaryReader br)
        {
            var entry = new FragmentRunEntry();
                entry.FirstFragment= br.ReadUInt32();
                entry.FirstFragmentTimestamp = br.ReadUInt64();
                entry.FragmentDuration = br.ReadUInt32();
            if(entry.FragmentDuration == 0)
                entry.DiscontinuityIndicator = br.ReadByte();
            else
                entry.DiscontinuityIndicator = null;
            return entry;
        }
    }
}
