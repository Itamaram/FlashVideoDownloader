using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles
{
    public class UnknownBox : F4VBox
    {
        public override void Parse(ExtendedBinaryReader br)
        {
            base.Parse(br);

            if (BoxHeader.TotalSize == 0)
                br.ReadToEnd();
            else if (BoxHeader.TotalSize == 1)
            {
                br.ReadChunkedBytes(BoxHeader.ExtendedSize.Value - (ulong)BoxHeader.HeaderSize).Select(s => s);
            }
            else
            {
                foreach (var s in br.ReadChunkedBytes((ulong)BoxHeader.TotalSize - (ulong)BoxHeader.HeaderSize)) ;
            }
        }
    }
}
