using System.Web.Mvc;
using Rabbit.Configuration;

namespace Rabbit.IOnline.App_Start
{
    public static class AppSettings
    {
        private static int _pageSize;

        private static IConfiguration Configuration
        {
            get
            {
                return DependencyResolver.Current.GetService<IConfiguration>();
            }
        }

        public static int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                {
                    var pageSize = Configuration.Get("PageSize");
                    if (!int.TryParse(pageSize, out _pageSize))
                    {
                        _pageSize = 5;
                    }
                }
                return _pageSize;
            }
        }
    }
}