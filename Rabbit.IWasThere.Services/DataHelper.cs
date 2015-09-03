using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rabbit.Foundation.Data;
using Rabbit.Net.WebCrawling;
using Rabbit.SerializationMaster;

namespace Rabbit.IWasThere.Services
{
    public static class DataHelper
    {
        public static IEnumerable<DataItem> ParseJsonFromUrl(string url)
        {
            return
                new WebRequestWorker().DownloadResponse(new CrawlingOption(url))
                    .ReadAsText()
                    .Deserialize<List<DataItem>>()
                    .Where(x => x != null);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<DataItem> items)
        {
            return items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.Key,
            });
        }
    }
}