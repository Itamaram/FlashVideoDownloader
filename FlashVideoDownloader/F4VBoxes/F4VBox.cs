using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    public abstract class F4VBox
    {
        /// <summary>
        /// A consistent header that all boxes have
        /// </summary>
        public F4VBoxHeader BoxHeader { get; set; }

        public F4VBox()
        {
        }

        public virtual void Parse(ExtendedBinaryReader br)
        {
            if (BoxHeader == null)
            {
                BoxHeader = new F4VBoxHeader();
                BoxHeader.Parse(br);
            }
        }

        public override string ToString()
        {
            return BoxHeader.BoxType == null ? "????" : BoxHeader.BoxType.ToString();
        }
    }
}