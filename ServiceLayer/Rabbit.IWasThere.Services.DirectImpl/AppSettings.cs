using System.Collections.Generic;
using System.Linq;
using Rabbit.Configuration;
using Rabbit.Foundation.Data;

namespace Rabbit.IWasThere.Services.DirectImpl
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;
        private readonly IDataService _dataService;
        private int _pageSize;
        private IList<DataItem> _categories;

        public AppSettings(IConfiguration configuration, IDataService dataService)
        {
            _configuration = configuration;
            _dataService = dataService;
        }

        public int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                {
                    var pageSize = _configuration.Get("PageSize");
                    if (!int.TryParse(pageSize, out _pageSize))
                    {
                        _pageSize = 5;
                    }
                }
                return _pageSize;
            }
        }

        public IList<DataItem> Categories
        {
            get
            {
                var categoryDataFile = _configuration.Get("CategoryDataFilePath");
                return _categories ?? (_categories = _dataService.GetRemoteItems(categoryDataFile).ToList());
            }
        }
    }
}
