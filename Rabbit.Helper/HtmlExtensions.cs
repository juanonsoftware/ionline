using System.Web;
using System.Web.Mvc;
using Rabbit.Net.WebCrawling;

namespace Rabbit.Helper
{
    public static class HtmlExtensions
    {
        public static IHtmlString GetRemoteContent(this HtmlHelper html, string fileUri)
        {
            if (string.IsNullOrWhiteSpace(fileUri))
            {
                return MvcHtmlString.Empty;
            }

            var fileContent = new WebRequestWorker().DownloadResponse(new CrawlingOption(fileUri)).ReadAsText();
            return html.Raw(fileContent);
        }
    }
}