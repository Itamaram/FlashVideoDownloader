using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashVideoFiles
{
    public class FourCC
    {
        private uint val;
        public FourCC(){
            val = 0;
        }
        public FourCC(uint val)
        {
            this.val = val;
        }
        public static implicit operator FourCC(uint i){
            return new FourCC(i);
        }
        public static implicit operator uint(FourCC fcc)
        {
            return fcc.val;
        }
        public static explicit operator string(FourCC fcc)
        {
            return Enumerable.Range(0, 4).Select(i => (char)((fcc >> (i * 8)) & 0xff)).Reverse().Aggregate("", (s, c) => s += c);
        }
        public override string ToString()
        {
            return (string)this;
        }
        public static bool operator ==(FourCC fcc, string s)
        {
            return (string)fcc == s;
        }
        public static bool operator ==(string s, FourCC fcc)
        {
            return fcc == s;
        }
        public static bool operator !=(FourCC fcc, string s)
        {
            return !(fcc == s);
        }
        public static bool operator !=(string s, FourCC fcc)
        {
            return fcc != s;
        }
    }
}
