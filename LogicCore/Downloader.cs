using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LogicCore
{
    public class Downloader:IDownloader
    {
        private WebClient WebClient { get; set; }
        public Downloader()
        {
            WebClient = new WebClient();
        }

        public string Get(string url)
        {
            return WebClient.DownloadString(url);
        }

        public void Get(string url, string fileName)
        {
            WebClient.DownloadFile(url, fileName);
        }
    }
}
