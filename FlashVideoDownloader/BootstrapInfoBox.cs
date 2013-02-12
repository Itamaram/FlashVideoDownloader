using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    /// <summary>
    /// A Bootstrap Info (abst) box contains the information necessary to bootstrap the media-presentation URL requests RFC1630 from the media client to the HTTP server. The media presentation can be either a live or a video-ondemand scenario. This box contains basic information about the server, movie, and segment information. It also contains one or more segment run tables and fragment run tables.
    /// In the HTTP streaming segment, the abst box is optional and precedes the Movie (moov) box. In the HTTP streaming fragment, the abst box is required. For a description of the boxes and structure required for HTTP streaming, see Annex C. HTTP Streaming: File Structure.
    /// </summary>
    public class BootstrapInfoBox : F4VBox
    {
        /// <summary>
        /// Either 0 or 1
        /// </summary>
        public byte Version { get; private set; }

        /// <summary>
        /// Reserved. Set to 0
        /// </summary>
        public UInt24 Flags { get; private set; }

        /// <summary>
        /// The version number of the bootstrap information.
        /// When the Update field is set, BootstrapinfoVersion indicates the version number that is being updated.
        /// </summary>
        public uint BootstrapinfoVersion { get; private set; }

        /// <summary>
        /// Indicates if it is the Named Access (0) or the Range Access (1) Profile. One bit is reserved for future profiles. Length should be 2.
        /// </summary>
        public bool[] Profile { get; private set; }

        /// <summary>
        /// Indicates if the media presentation is live (1) or not.
        /// </summary>
        public bool Live { get; private set; }

        /// <summary>
        /// Indicates if this table is a full version (0) or an update (1) to a previously defined (sent) full version of the bootstrap box or file.
        /// Updates are not complete replacements. They may contain only the changed elements. The server sends the updates only when the bootstrap information changes. The updates apply to the full version with the same BootstrapinfoVersion number. There may be more than one update for the same BootstrapinfoVersion number. 
        /// If the server sends multiple updates, the updates apply to the full version with the same BootstrapinfoVersion number. Each update includes all previous updates to the same BootstrapinfoVersion. For multiple updates to a single full version, the latest update is determined based on the CurrentMediaTime.
        /// </summary>
        public bool Update { get; private set; }

        /// <summary>
        /// Reserved, set to 0. Length should be 4
        /// </summary>
        public bool[] Reserved { get; private set; }

        /// <summary>
        /// The number of time units per second. The field CurrentMediaTime uses this value to represent accurate time. Typically, the value is 1000, for a unit of milliseconds.
        /// </summary>
        public uint TimeScale { get; private set; }

        /// <summary>
        /// The timestamp in TimeScale units of the latest available Fragment in the media presentation. This timestamp is used to request the right fragment number. The CurrentMediaTime can be the total duration. For media presentations that are not live, CurrentMediaTime can be 0.
        /// </summary>
        public UInt64 CurrentMediaTime { get; private set; }

        /// <summary>
        /// The offset of the CurrentMediaTime from the SMPTE time code, converted to milliseconds. This offset is not in TimeScale units. This field is zero when not used. The server uses the SMPTE time code modulo 24 hours to make the offset positive.
        /// </summary>
        public UInt64 SmpteTimeCodeOffset { get; private set; }

        /// <summary>
        /// The identifier of this presentation. The identifier is a null-terminated UTF-8 string. For example, it can be a filename or pathname in a URL. See Annex C.4 URL Construction for usage.
        /// </summary>
        public string MovieIdentifier { get; private set; }

        /// <summary>
        /// The number of ServerEntryTable entries. The minimum value is 0.
        /// </summary>
        public byte ServerEntryCount { get; private set; }

        /// <summary>
        /// Server URLs in descending order of preference 
        /// </summary>
        public ServerEntry[] ServerEntryTable { get; private set; }

        /// <summary>
        /// The number of QualityEntryTable entries, which is also the number of available quality levels. The minimum value is 0. Available quality levels are for, for example, multi bit rate files or trick files.
        /// </summary>
        public byte QualityEntryCount { get; private set; }

        /// <summary>
        /// Quality file references in order from high to low quality
        /// </summary>
        public QualityEntry[] QualityEntryTable { get; private set; }

        /// <summary>
        /// Null or null-terminated UTF-8 string. This string holds Digital Rights Management metadata. Encrypted files use this metadata to get the necessary keys and licenses for decryption and playback.
        /// </summary>
        public string DrmData { get; private set; }

        /// <summary>
        /// Null or null-terminated UTF-8 string that holds metadata
        /// </summary>
        public string MetaData { get; private set; }

        /// <summary>
        /// The number of entries in SegmentRunTableEntries.
        /// The minimum value is 1. Typically, one table contains all segment runs. However, this count provides the flexibility to define the segment runs individually for each quality level (or trick file).
        /// </summary>
        public byte SegmentRunTableCount { get; private set; }

        /// <summary>
        /// Array of SegmentRunTable elements
        /// </summary>
        public SegmentRunTableBox[] SegmentRunTableEntries { get; private set; }

        /// <summary>
        /// The number of entries in FragmentRunTableEntries. The minimum value is 1.
        /// </summary>
        public byte FragmentRunTableCount { get; private set; }

        /// <summary>
        /// Array of FragmentRunTable elements 
        /// </summary>
        public FragmentRunTableBox[] FragmentRunTableEntries { get; private set; }

        public override void Parse(ExtendedBinaryReader br)
        {
            base.Parse(br);

            Version = br.ReadByte();
            Flags = br.ReadUInt24();
            BootstrapinfoVersion = br.ReadUInt32();
            var b = br.ReadByte();
            Profile = (new byte[] { 0x80, 0x40 }).Select(i => (i & b) != 0).ToArray();
            Live = (0x02 & b) != 0;
            Update = (0x01 & b) != 0;
            Reserved = (new byte[] { 0x08, 0x04, 0x02, 0x01 }).Select(i => (i & b) != 0).ToArray();
            TimeScale = br.ReadUInt32();
            CurrentMediaTime = br.ReadUInt64();
            SmpteTimeCodeOffset = br.ReadUInt64();
            MovieIdentifier = br.ReadNullTerminatedString();
            ServerEntryCount = br.ReadByte();
            ServerEntryTable = new ServerEntry[ServerEntryCount];
            for (int i = 0; i < ServerEntryCount; i++)
                ServerEntryTable[i] = ServerEntry.Parse(br);
            QualityEntryCount = br.ReadByte();
            QualityEntryTable = new QualityEntry[QualityEntryCount];
            for (int i = 0; i < QualityEntryCount; i++)
                QualityEntryTable[i] = QualityEntry.Parse(br);
            DrmData = br.ReadNullTerminatedString();
            MetaData = br.ReadNullTerminatedString();
            SegmentRunTableCount = br.ReadByte();
            SegmentRunTableEntries = new SegmentRunTableBox[SegmentRunTableCount];
            for (int i = 0; i < SegmentRunTableCount; i++)
            {
                SegmentRunTableEntries[i] = new SegmentRunTableBox();
                SegmentRunTableEntries[i].Parse(br);
            }
            FragmentRunTableCount = br.ReadByte();
            FragmentRunTableEntries = new FragmentRunTableBox[FragmentRunTableCount];
            for (int i = 0; i < FragmentRunTableCount; i++)
            {
                FragmentRunTableEntries[i] = new FragmentRunTableBox();
                FragmentRunTableEntries[i].Parse(br);
            }
        }

        public static BootstrapInfoBox FromBase64String(string s)
        {
            var stream = new MemoryStream(System.Convert.FromBase64String(s));
            using (var br = new ExtendedBinaryReader(stream))
            {
                BootstrapInfoBox bib = new BootstrapInfoBox();
                bib.Parse(br);
                return bib;
            }
        }
    }

    public class ServerEntry
    {
        /// <summary>
        /// The server base url for this presentation on that server. The value is a null-terminated UTF-8 string, without a trailing "/".
        /// </summary>
        public string ServerBaseURL { get; private set; }

        public static ServerEntry Parse(ExtendedBinaryReader br)
        {
            return new ServerEntry
            {
                ServerBaseURL = br.ReadNullTerminatedString()
            };
        }
    }

    public class QualityEntry
    {
        /// <summary>
        /// Name of the quality (segment) file that is used to construct the right URL for that quality media. The value is a null-terminated UTF-8 string, optionally with a trailing "/".
        /// </summary>
        public string QualitySegmentUrlModifier { get; private set; }

        public static QualityEntry Parse(ExtendedBinaryReader br)
        {
            return new QualityEntry
            {
                QualitySegmentUrlModifier = br.ReadNullTerminatedString()
            };
        }
    }
}
