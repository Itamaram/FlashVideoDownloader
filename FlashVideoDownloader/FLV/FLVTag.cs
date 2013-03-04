using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles
{
    public enum TagType{
        Audio = 8,
        Video = 9,
        ScriptData = 18
    }

    /// <summary>
    /// The FLV tag contains metadata for audio, video, or scripts, optional encryption metadata, and the payload.
    /// </summary>
    public class FLVTag
    {
        /// <summary>
        /// Reserved for FMS, should be 0
        /// </summary>
        public byte Reserved { get; private set; }

        /// <summary>
        /// Indicates if packets are filtered.
        /// false = No pre-processing required.
        /// true = Pre-processing (such as decryption) of the packet is required before it can be rendered.
        /// Shall be 0 in unencrypted files, and 1 for encrypted tags.
        /// </summary>
        public bool Filter { get; private set; }

        /// <summary>
        /// Type of contents in this tag. The following types are defined:
        /// </summary>
        public TagType TagType { get; private set; }

        /// <summary>
        /// Length of the message. Number of bytes after StreamID to end of tag (Equal to length of the tag – 11)
        /// </summary>
        public UInt24 DataSize { get; private set; }

        /// <summary>
        /// Time in milliseconds at which the data in this tag applies. This value is relative to the first tag in the FLV file, which always has a timestamp of 0
        /// </summary>
        public UInt24 TimeStamp { get; private set; }

        /// <summary>
        /// Extension of the Timestamp field to form a SI32 value. This field represents the upper 8 bits, while the previous Timestamp field represents the lower 24 bits of the time in milliseconds.
        /// </summary>
        public byte TimestampExtended { get; private set; }

        /// <summary>
        /// Always 0.
        /// </summary>
        public UInt24 StreamID { get; private set; }

        public byte[] HeaderAndBody { get; private set; }

        public static FLVTag Parse(ExtendedBinaryReader br)
        {
            var b = br.ReadByte();
            var tag = new FLVTag
            {
                Reserved = (byte)(b & 0xc0),
                Filter = (b & 0x20) == 0,
                TagType = (TagType)(b & 0x1f),
                DataSize = br.ReadUInt24(),
                TimeStamp = br.ReadUInt24(),
                TimestampExtended = br.ReadByte(),
                StreamID = br.ReadUInt24()
            };
            tag.HeaderAndBody = br.ReadBytes((tag.TimestampExtended << 24) + (int)tag.TimeStamp);
            return tag;
        }

        public byte[] ToByteArray()
        {
            return new byte[]{
                (byte)((Reserved << 6) & ((Filter?1:0) << 5) &((byte)TagType & 0x1f)),
                DataSize.Byte1,DataSize.Byte2,DataSize.Byte3,
                TimeStamp.Byte1, TimeStamp.Byte2, TimeStamp.Byte3,
                TimestampExtended,
                StreamID.Byte1,StreamID.Byte2,StreamID.Byte3
            };
        }
    }
}
