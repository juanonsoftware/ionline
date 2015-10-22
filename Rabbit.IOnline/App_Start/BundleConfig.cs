using System.Web.Optimization;

namespace Rabbit.IOnline.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery", "//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.3.min.js")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap", "//ajax.aspnetcdn.com/ajax/bootstrap/3.3.5/bootstrap.min.js")
                .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-unobtrusive-ajax")
                .Include("~/Scripts/jquery.unobtrusive-*"));

            bundles.Add(
                new ScriptBundle("~/bundles/showdown",
                    "//cdnjs.cloudflare.com/ajax/libs/showdown/1.2.3/showdown.min.js")
                    .Include("~/Scripts/showdown.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/showndown-preview",
                    "//cdn.rawgit.com/netvietdev/showdown-preview/dist/v1.1/jquery.showdown-preview.js").Include(
                        "~/Scripts/jquery.showdown-preview.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap",
                "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css")
                .Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/jumbotron-narrow.css",
                    "~/Content/spinner.css",
                    "~/Content/site.css"));

            bundles.Add(
                new StyleBundle("~/Content/cssbox", "//cdn.rawgit.com/netvietdev/cssbox/dict/v1.2.0/cssbox.min.css")
                    .Include("~/Content/cssbox.css"));
        }
    }
}
