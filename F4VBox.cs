using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlashVideoFiles
{
    abstract class F4VBox
    {
        /// <summary>
        /// A consistent header that all boxes have
        /// </summary>
        public F4VBoxHeader BoxHeader { get; private set; }

        public F4VBox()
        {
            BoxHeader = new F4VBoxHeader();
        }

        public virtual void Parse(Stream s)
        {
            BoxHeader.Parse(s);
        }
    }
}