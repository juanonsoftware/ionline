using Rabbit.Foundation.Data;
using Rabbit.Net.WebCrawling;
using System.Collections.Generic;

namespace Rabbit.IWasThere.Services.DirectImpl
{
    public class DirectDataService : IDataService
    {
        public IEnumerable<DataItem> GetCategories(string dataFileUrl)
        {
            return DataHelper.GetRemoteJsonData(dataFileUrl);
        }

        public string GetRemoteContent(string creditsFileUrl)
        {
            return
               new WebRequestWorker().DownloadResponse(new CrawlingOption(creditsFileUrl))
                   .ReadAsText();
        }
    }
}