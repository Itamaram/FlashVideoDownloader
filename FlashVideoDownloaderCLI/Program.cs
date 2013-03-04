using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashVideoFiles;
using System.IO;
using System.Net;

namespace FlashVideoDownloaderCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var wc = new WebClient { Proxy = null };

            var f = F4Manifest.FromXmlString(wc.DownloadString("http://voda.gua.unlv.edu/scramble203.f4m"));
            f = F4Manifest.FromXmlString(wc.DownloadString(f.Media.First().Href));
            var x = f.Media.First().ManifestBootstrapInfo.BootstrapInfoBox.SegmentRunTableEntries.First().SegmentRunEntryTable.First().FragmentsPerSegment;

            var format = "http://{0}{1}{2}Seg{3}-Frag{4}";
            var flv = new FLVFile
            {
                TypeFlagsAudio = true,
                TypeFlagVideo = true
            };

            var bw = new BinaryWriter(new FileStream("C:\\temp.flv", FileMode.Create));
            flv.WriteToFile(bw);
            bw.Write((int)0);
            bw.Flush();
            for (int i = 1; i <= x; i++)
            {
                Console.WriteLine("Downloading box: {0} of {1}", i, x);
                string fileName = "C:\\FlvTemp\\part" + i + ".frag";
                //wc.DownloadFile(String.Format(format, "voda.gua.unlv.edu/hds-vod/", "scramble203.mp4", "", 1, i), fileName);
                var br = new ExtendedBinaryReader(new FileStream(fileName, FileMode.Open));
                var p = new BoxParser(br);
                F4VBox b;
                for (b = p.ReadBox(); b.BoxHeader.BoxType != "mdat"; b = p.ReadBox()) ;
                var br2 = new ExtendedBinaryReader(new MemoryStream((b as MediaDataBox).Payload));
                var t = FLVTag.Parse(br2);
                br2.Close();
                br2 = new ExtendedBinaryReader(new MemoryStream((b as MediaDataBox).Payload));
                bw.Write(br2.ReadBytes(11 + (int)t.DataSize + 4));
            }
            bw.Flush();
            bw.Close();

            string boot = "AAAAi2Fic3QAAAAAAAAAAQAAAAPoAAAAAABCG+gAAAAAAAAAAAAAAAAAAQAAABlhc3J0AAAAAAAAAAABAAAAAQAAA2IBAAAARmFmcnQAAAAAAAAD6AAAAAADAAAAAQAAAAAAAAAAAAATiAAAA2IAAAAAAEH+iAAAHWAAAAAAAAAAAAAAAAAAAAAAAA==";
            var stream = new MemoryStream(System.Convert.FromBase64String(boot));
            using (var br = new ExtendedBinaryReader(stream))
            {
                BootstrapInfoBox bib = new BootstrapInfoBox();
                bib.Parse(br);
            }

            Console.ReadLine();
        }
    }
}
