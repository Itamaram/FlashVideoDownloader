using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles.F4VBoxes
{
    abstract class F4VBox
    {
        /// <summary>
        /// A consistent header that all boxes have
        /// </summary>
        public F4VBoxHeader BoxHeader { get; private set; }
        /// <summary>
        /// A number of bytes, the length of which is defined by the BOXHEADER
        /// </summary>
        public byte[] PayLoad { get; private set; }
        public abstract uint BoxType { get; }
    }
}
