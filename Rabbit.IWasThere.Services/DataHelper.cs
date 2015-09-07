using Rabbit.Foundation.Data;
using Rabbit.Net.WebCrawling;
using Rabbit.SerializationMaster;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IWasThere.Services
{
    public static class DataHelper
    {
        public static IEnumerable<DataItem> GetRemoteJsonData(string url)
        {
            return
                new WebRequestWorker().DownloadResponse(new CrawlingOption(url))
                    .ReadAsText()
                    .Deserialize<List<DataItem>>()
                    .Where(x => x != null);
        }
    }
}