using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    /// <summary>
    /// The Segment Run Table (asrt) box can be used to locate a segment that contains a particular fragment.
    /// There may be several asrt boxes, each for different quality levels.
    /// The asrt box uses a compact encoding:
    /// - A Segment Run Table may represent fragment runs for several quality levels.
    /// - The Segment Run Table is compactly coded. Each entry gives the first segment number for a run of segments with the same count of fragments. The count of segments having this same count of fragments can be calculated by subtracting the first segment number in this entry from the first segment number in the next entry.
    /// </summary>
    public class SegmentRunTableBox : F4VBox
    {
        /// <summary>
        /// Either 0 or 1
        /// </summary>
        public byte Version { get; private set; }

        /// <summary>
        /// The following values are defined:
        /// 0 = A full table. 
        /// 1 = The records in this table are updates (or new entries to be appended) to the previously defined Segment Run Table. The Update flag in the containing Bootstrap Info box shall be 1 when this flag is 1.
        /// </summary>
        public UInt24 Flags { get; private set; }

        /// <summary>
        /// The number of QualitySegmentUrlModifiers (quality level references) that follow. If 0, this Segment Run Table applies to all quality levels, and there shall be only one Segment Run Table box in the Bootstrap Info box. 
        /// </summary>
        public byte QualityEntryCount { get; private set; }

        /// <summary>
        /// An array of names of the quality levels that this table applies to. The names are null-terminated UTF-8 strings. The array shall be a subset of the QualityEntryTable in the Bootstrap Info (abst) box. The names shall not be present in any other Segment Run Table in the Bootstrap Info box.
        /// </summary>
        public string[] QualitySegmentUrlModifiers { get; private set; }

        /// <summary>
        /// The number of items in this SegmentRunEntryTable. The minimum value is 1.
        /// </summary>
        public uint SegmentRunEntryCount { get; private set; }

        /// <summary>
        /// Array of segment run entries 
        /// </summary>
        public SegmentRunEntry[] SegmentRunEntryTable { get; private set; }

        public override void Parse(ExtendedBinaryReader br)
        {
            base.Parse(br);

            Version = br.ReadByte();
            Flags = br.ReadUInt24();
            QualityEntryCount = br.ReadByte();
            QualitySegmentUrlModifiers = Enumerable.Range(0, QualityEntryCount).Select(i => br.ReadNullTerminatedString()).ToArray();
            SegmentRunEntryCount = br.ReadUInt32();
            SegmentRunEntryTable = new SegmentRunEntry[SegmentRunEntryCount];
            for (uint i = 0; i < SegmentRunEntryCount; i++)
                SegmentRunEntryTable[i] = SegmentRunEntry.Parse(br);
        }
    }

    public class SegmentRunEntry
    {
        /// <summary>
        /// The identifying number of the first segment in the run of segments containing the same number of fragments. The segment corresponding to the FirstSegment in the next SEGMENTRUNENTRY will terminate this run
        /// </summary>
        public uint FirstSegment { get; private set; }

        /// <summary>
        /// The number of fragments in each segment in this run.
        /// </summary>
        public uint FragmentsPerSegment { get; private set; }

        public static SegmentRunEntry Parse(ExtendedBinaryReader br)
        {
            return new SegmentRunEntry
            {
                FirstSegment = br.ReadUInt32(),
                FragmentsPerSegment = br.ReadUInt32()
            };
        }
    }
}
