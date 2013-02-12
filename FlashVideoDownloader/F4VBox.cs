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
            BoxHeader = new F4VBoxHeader();
        }

        public virtual void Parse(ExtendedBinaryReader br)
        {
            if(BoxHeader == null)
                BoxHeader.Parse(br);
        }

        public static F4VBox ParseBox(ExtendedBinaryReader br)
        {
            var header = new F4VBoxHeader();
            header.Parse(br);
            F4VBox box;
            switch (header.BoxType.ToString())
            {
                case "abst":
                    box = new BootstrapInfoBox();
                    break;
                case "asrt":
                    box = new SegmentRunTableBox();
                    break;
                case "afrt":
                    box = new FragmentRunTableBox();
                    break;
                case "mdat":
                    box = new MediaDataBox();
                    break;
                default:
                    return null;
            }
            box.BoxHeader = header;
            box.Parse(br);
            return box;
        }

        public override string ToString()
        {
            return BoxHeader.BoxType == null ? "????" : BoxHeader.BoxType.ToString();
        }
    }
}