using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashVideoFiles;
using System.IO;

namespace FlashVideoDownloaderCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<manifest xmlns=""http://ns.adobe.com/f4m/1.0"" xmlns:akamai=""uri:akamai.com/f4m/1.0"">
  <akamai:version>2.0</akamai:version>
  <akamai:bw>2000</akamai:bw>
  <id>/VOD/KESHET/master_chef/S03/masterchef3_20_VOD/masterchef3_20_VOD_,500,850,1200,2000,.mp4.csmil_0</id>
  <streamType>recorded</streamType>
  <akamai:streamType>vod</akamai:streamType>
  <duration>4332.520</duration>
  <streamBaseTime>0.000</streamBaseTime>
  <bootstrapInfo profile=""named"" id=""bootstrap_0"">AAAAi2Fic3QAAAAAAAAAAQAAAAPoAAAAAABCG+gAAAAAAAAAAAAAAAAAAQAAABlhc3J0AAAAAAAAAAABAAAAAQAAA2IBAAAARmFmcnQAAAAAAAAD6AAAAAADAAAAAQAAAAAAAAAAAAATiAAAA2IAAAAAAEH+iAAAHWAAAAAAAAAAAAAAAAAAAAAAAA==</bootstrapInfo>
  <bootstrapInfo profile=""named"" id=""bootstrap_1"">AAAAi2Fic3QAAAAAAAAAAQAAAAPoAAAAAABCG+gAAAAAAAAAAAAAAAAAAQAAABlhc3J0AAAAAAAAAAABAAAAAQAAA2IBAAAARmFmcnQAAAAAAAAD6AAAAAADAAAAAQAAAAAAAAAAAAATiAAAA2IAAAAAAEH+iAAAHWAAAAAAAAAAAAAAAAAAAAAAAA==</bootstrapInfo>
  <bootstrapInfo profile=""named"" id=""bootstrap_2"">AAAAi2Fic3QAAAAAAAAAAQAAAAPoAAAAAABCG+gAAAAAAAAAAAAAAAAAAQAAABlhc3J0AAAAAAAAAAABAAAAAQAAA2IBAAAARmFmcnQAAAAAAAAD6AAAAAADAAAAAQAAAAAAAAAAAAATiAAAA2IAAAAAAEH+iAAAHWAAAAAAAAAAAAAAAAAAAAAAAA==</bootstrapInfo>
  <bootstrapInfo profile=""named"" id=""bootstrap_3"">AAAAi2Fic3QAAAAAAAAAAQAAAAPoAAAAAABCG+gAAAAAAAAAAAAAAAAAAQAAABlhc3J0AAAAAAAAAAABAAAAAQAAA2IBAAAARmFmcnQAAAAAAAAD6AAAAAADAAAAAQAAAAAAAAAAAAATiAAAA2IAAAAAAEH+iAAAHWAAAAAAAAAAAAAAAAAAAAAAAA==</bootstrapInfo>
  <media bitrate=""541"" url=""0_e033fbf49e447697_"" bootstrapInfoId=""bootstrap_0"">
    <metadata>AgAKb25NZXRhRGF0YQgAAAAMAAhkdXJhdGlvbgBAsOyFHrhR7AAFd2lkdGgAQH4AAAAAAAAABmhlaWdodABAdoAAAAAAAAANdmlkZW9kYXRhcmF0ZQBAfgxCMhJYAAAJZnJhbWVyYXRlAEA5AAAAAAAAAAx2aWRlb2NvZGVjaWQAQBwAAAAAAAAADWF1ZGlvZGF0YXJhdGUAQE7V2YTgbEMAD2F1ZGlvc2FtcGxlcmF0ZQBA5YiAAAAAAAAPYXVkaW9zYW1wbGVzaXplAEAwAAAAAAAAAAZzdGVyZW8BAQAMYXVkaW9jb2RlY2lkAEAkAAAAAAAAAAhmaWxlc2l6ZQBBsYJ+0wAAAAAACQ==</metadata>
  </media>
  <media bitrate=""911"" url=""1_e033fbf49e447697_"" bootstrapInfoId=""bootstrap_1"">
    <metadata>AgAKb25NZXRhRGF0YQgAAAAMAAhkdXJhdGlvbgBAsOyFHrhR7AAFd2lkdGgAQIQAAAAAAAAABmhlaWdodABAfgAAAAAAAAANdmlkZW9kYXRhcmF0ZQBAipTLqG803AAJZnJhbWVyYXRlAEA5AAAAAAAAAAx2aWRlb2NvZGVjaWQAQBwAAAAAAAAADWF1ZGlvZGF0YXJhdGUAQE7V2YTgbEMAD2F1ZGlvc2FtcGxlcmF0ZQBA5YiAAAAAAAAPYXVkaW9zYW1wbGVzaXplAEAwAAAAAAAAAAZzdGVyZW8BAQAMYXVkaW9jb2RlY2lkAEAkAAAAAAAAAAhmaWxlc2l6ZQBBvXKozAAAAAAACQ==</metadata>
  </media>
  <media bitrate=""1347"" url=""2_e033fbf49e447697_"" bootstrapInfoId=""bootstrap_2"">
    <metadata>AgAKb25NZXRhRGF0YQgAAAAMAAhkdXJhdGlvbgBAsOyFHrhR7AAFd2lkdGgAQIQAAAAAAAAABmhlaWdodABAfgAAAAAAAAANdmlkZW9kYXRhcmF0ZQBAlBospCeuvQAJZnJhbWVyYXRlAEA5AAAAAAAAAAx2aWRlb2NvZGVjaWQAQBwAAAAAAAAADWF1ZGlvZGF0YXJhdGUAQE7V2YTgbEMAD2F1ZGlvc2FtcGxlcmF0ZQBA5YiAAAAAAAAPYXVkaW9zYW1wbGVzaXplAEAwAAAAAAAAAAZzdGVyZW8BAQAMYXVkaW9jb2RlY2lkAEAkAAAAAAAAAAhmaWxlc2l6ZQBBxcKRxYAAAAAACQ==</metadata>
  </media>
  <media bitrate=""2200"" url=""3_e033fbf49e447697_"" bootstrapInfoId=""bootstrap_3"">
    <metadata>AgAKb25NZXRhRGF0YQgAAAAMAAhkdXJhdGlvbgBAsOyFHrhR7AAFd2lkdGgAQIaAAAAAAAAABmhlaWdodABAggAAAAAAAAANdmlkZW9kYXRhcmF0ZQBAoLbSbVRwigAJZnJhbWVyYXRlAEA5AAAAAAAAAAx2aWRlb2NvZGVjaWQAQBwAAAAAAAAADWF1ZGlvZGF0YXJhdGUAQE7V2YTgbEMAD2F1ZGlvc2FtcGxlcmF0ZQBA5YiAAAAAAAAPYXVkaW9zYW1wbGVzaXplAEAwAAAAAAAAAAZzdGVyZW8BAQAMYXVkaW9jb2RlY2lkAEAkAAAAAAAAAAhmaWxlc2l6ZQBB0cM6oYAAAAAACQ==</metadata>
  </media>
</manifest>
";
            var f = F4Manifest.FromXmlString(s);

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
