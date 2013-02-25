using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles
{
    public class BoxParser : IDisposable
    {
        private ExtendedBinaryReader br;
        private F4VBoxHeader currentHeader;
        public BoxParser(ExtendedBinaryReader br)
        {
            this.br = br;
            currentHeader = null;
        }

        public F4VBoxHeader ReadHeader()
        {
            if (currentHeader != null)
            {
                if (currentHeader.TotalSize == 0)
                    throw new Exception("EOF Found before next header");
                br.SkipBytes(currentHeader.ExtendedSize ?? currentHeader.TotalSize);
            }
            currentHeader = new F4VBoxHeader();
            currentHeader.Parse(br);
            return currentHeader;
        }

        public F4VBox ReadBox()
        {
            if (currentHeader == null)
                ReadHeader();

            F4VBox box;
            switch (currentHeader.BoxType.ToString())
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
            box.BoxHeader = currentHeader;
            box.Parse(br);
            return box;
        }

        public void Dispose()
        {
            br.Dispose();
        }
    }
}
