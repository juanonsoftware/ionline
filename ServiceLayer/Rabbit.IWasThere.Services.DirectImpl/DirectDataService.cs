using System.Collections.Generic;
using System.Linq;
using Rabbit.Foundation.Data;
using Rabbit.Net.WebCrawling;
using Rabbit.SerializationMaster;

namespace Rabbit.IWasThere.Services.DirectImpl
{
    public class DirectDataService : IDataService
    {
        public IEnumerable<DataItem> GetRemoteItems(string fileUri)
        {
            return
                new WebRequestWorker().DownloadResponse(new CrawlingOption(fileUri))
                    .ReadAsText()
                    .Deserialize<List<DataItem>>()
                    .Where(x => x != null);
        }

        public string GetRemoteContent(string fileUri)
        {
            return
               new WebRequestWorker().DownloadResponse(new CrawlingOption(fileUri))
                   .ReadAsText();
        }
    }
}