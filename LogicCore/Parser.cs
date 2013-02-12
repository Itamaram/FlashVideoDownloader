using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashVideoFiles;

namespace LogicCore
{
    public class Parser
    {
        public static void ParseFrag(ExtendedBinaryReader br)
        {
            F4VBox box;
            for (box = F4VBox.ParseBox(br); box.BoxHeader.BoxType != "mdat"; box = F4VBox.ParseBox(br)) ;
 

        }
    }
}
