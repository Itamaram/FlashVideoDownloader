using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCore
{
    public interface IDownloader
    {
        /// <summary>
        /// Downloads the requested url as an in memory string
        /// </summary>
        /// <param name="url">The requested url</param>
        /// <returns></returns>
        string Get(string url);

        /// <summary>
        /// Downloads the requested url and saves the result locally
        /// </summary>
        /// <param name="url">The requestd url</param>
        /// <param name="savepath">Target path</param>
        /// <returns></returns>
        void Get(string url, string fileName);
    }
}
