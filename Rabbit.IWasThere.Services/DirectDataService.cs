﻿using Rabbit.Foundation.Data;
using Rabbit.Net.WebCrawling;
using System.Collections.Generic;

namespace Rabbit.IWasThere.Services
{
    public class DirectDataService : IDataService
    {
        public IEnumerable<DataItem> GetCategories(string dataFileUrl)
        {
            return DataHelper.ParseJsonFromUrl(dataFileUrl);
        }

        public string GetCredits(string creditsFileUrl)
        {
            return
               new WebRequestWorker().DownloadResponse(new CrawlingOption(creditsFileUrl))
                   .ReadAsText();
        }
    }
}