using System;
using FlashVideoFiles.Structs;

namespace FlashVideoFiles.F4VBoxes
{
    /// <summary>
    /// The Movie Header (mvhd) box defines playback information that applies to the entire F4V file
    /// </summary>
    class MovieHeaderBox : F4VBox
    {
        /// <summary>
        /// BoxType = 'mvhd' 
        /// </summary>
        public override uint BoxType { get { return 0x6D766864; } }
        /// <summary>
        /// Either 0 or 1
        /// </summary>
        public byte Version { get; set; }
        /// <summary>
        /// Reserved. Set to 0
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
        /// The time coordinate system for the entire F4V file, in number of time units per second. For example, 100 indicates the time units are 1/100 second each
        /// </summary>
        public uint TimeScale { get; set; }
        /// <summary>
        /// The total length of the F4V file presentation, in TimeScale units. This is also the duration of the longest track in the file.
        /// Is a Uint32 if Version == 0
        /// </summary>
        public UInt64 Duration { get; set; }
        /// <summary>
        /// The preferred rate of playback, expressed as a fixed point 16.16 number (commonly 0x00010000 = 1.0, or normal playback rate)
        /// </summary>
        public Int16_16 Rate { get; set; }
        /// <summary>
        /// The master volume of the file, expressed as a fixed point 8.8 number (commonly 0x0100 = 1.0, or full volume)
        /// </summary>
        public Int8_8 Volume { get; set; }
        /// <summary>
        /// Reserved. Set to 0
        /// </summary>
        public UInt16 Reserved { get; set; }
        /// <summary>
        /// Reserved. Set to 0. Array Length is always 2.
        /// </summary>
        public uint[] Reserved_2 { get; set; }
        /// <summary>
        /// Transformation matrix for the F4V file, shall be
        /// {0x00010000, 0, 0,
        /// 0, 0x00010000, 0,
        /// 0, 0, 0x40000000}.
        /// Array Length is always 9.
        /// </summary>
        public int[] Matrix { get; set; }
        /// <summary>
        /// Reserved. Set to 0. Array Length is always 6.
        /// </summary>
        public uint[] Reserved_3 { get; set; }
        /// <summary>
        /// The ID of the next track to be added to the presentation.
        /// This value shall not be 0 but may be all 1’s to indicate an undefined state
        /// </summary>
        public uint NextTrackID { get; set; }
    }
}
