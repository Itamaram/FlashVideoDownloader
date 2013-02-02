using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles.F4VBoxes
{
    /// <summary>
    /// The Edit (edts) box maps the presentation timeline to the media timeline, as it is stored in F4V file. The edts box is a container for the edit lists. 
    /// If the file does not contain an edts box, there is an implicit one-to-one mapping of these timelines.
    /// </summary>
    class EditBox :F4VBox
    {
        /// <summary>
        /// BoxType = 'edts'
        /// </summary>
        public override uint BoxType { get { return 0x65647473; } }
        /// <summary>
        /// An explicit time-line map 
        /// </summary>
        public EditListBox EditListBox { get; set; }
    }
}
