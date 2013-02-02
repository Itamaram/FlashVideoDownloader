using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashVideoFiles.Structs;

namespace FlashVideoFiles.F4VBoxes
{
    /// <summary>
    /// The Track Header (tkhd) box describes the main properties of a track
    /// </summary>
    class TrackHeaderBox :F4VBox
    {
        /// <summary>
        /// BoxType = 'tkhd'
        /// </summary>
        public override uint BoxType { get { return 0x746B6864; } }
        /// <summary>
        /// Either 0 or 1
        /// </summary>
        public byte Version { get; set; }
        /// <summary>
        /// Bit 0 = the track is enabled
        /// Bit 1 = the track is part of the presentation
        /// Bit 2 = the track should be considered when previewing the F4V file
        /// </summary>
        public UInt24 Flags { get; set; }
        /// <summary>
        /// The creation time of the F4V file, expressed as seconds elapsed since midnight, January 1, 1904 (UTC).
        /// Is a Uint32 if Version == 0
        /// </summary>
        public UInt64 CreationTime { get; set; }
        /// <summary>
        /// The last modification time of the F4V file, expressed as seconds elapsed since midnight, January 1, 1904 (UTC).
        /// Is a Uint32 if Version == 0
        /// </summary>
        public UInt64 ModificationTime { get; set; }
        /// <summary>
        /// The track’s unique identifier
        /// </summary>
        public uint TrackID { get; set; }
        /// <summary>
        /// Reserved. Set to 0
        /// </summary>
        public uint Reserved { get; set; }
        /// <summary>
        /// The duration of the track, in TimeScale units defined in the Movie Header box.
        /// Is a Uint32 if Version == 0
        /// </summary>
        public UInt64 Duration { get; set; }
        /// <summary>
        /// Reserved. Set to 0.
        /// Array Length is always 2.
        /// </summary>
        public uint[] Reserved_2 { get; set; }
        /// <summary>
        /// The position of the front to back ordering of tracks, expected to be 0 for F4V files
        /// </summary>
        public short Layer { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public short AlternateGroup { get; set; }
        /// <summary>
        /// 0x0100 (fixed point 8.8 number representing 1.0) for audio track, otherwise 0
        /// </summary>
        public Int8_8 Volume { get; set; }
        /// <summary>
        /// Reserved. Set to 0
        /// </summary>
        public ushort Reserved_3 { get; set; }
        /// <summary>
        /// Transformation matrix for the F4V file, shall be
        /// {0x00010000, 0, 0,
        /// 0, 0x00010000, 0,
        /// 0, 0, 0x40000000}.
        /// Array Length is always 9.
        /// </summary>
        public int[] TransformMatrix { get; set; }
        /// <summary>
        /// Width expressed as a fixed point 16.16 number
        /// </summary>
        public UInt16_16 Width { get; set; }
        /// <summary>
        /// Height expressed as a fixed point 16.16 number
        /// </summary>
        public UInt16_16 Height { get; set; }
    }
}
