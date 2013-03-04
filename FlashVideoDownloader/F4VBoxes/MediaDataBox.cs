using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    /// <summary>
    /// A Media Data (mdat) box contains the media data payload for the F4V file. All video samples, audio samples, data 
    /// samples, and hint tracks and samples are contained in the mdat box. See 1.8 Supported Media Types.
    /// The mdat box occurs at the top level of an F4V file, along with the Media (moov) box. 
    /// The mdat box cannot be understood on its own, which is why a moov box must be present in the file as well.
    /// </summary>
    public class MediaDataBox :F4VBox
    {
        /// <summary>
        /// Bytes of media data, the structure of which is defined in the file’s moov box
        /// </summary>
        public byte[] Payload { get; set; }

        public override void Parse(ExtendedBinaryReader br)
        {
            base.Parse(br);
            
            //Because the box size is not a reliable metric. So useful.
            Payload = br.ReadToEnd();
        }
    }
}
