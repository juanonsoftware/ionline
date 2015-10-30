using Rabbit.Configuration;
using Rabbit.Foundation.Data;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IWasThere.Services.DirectImpl
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;
        private readonly IDataService _dataService;
        private int _pageSize;
        private int _previewLimit;
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

        public int PreviewLimit
        {
            get
            {
                if (_previewLimit <= 0)
                {
                    var previewLimit = _configuration.Get("PreviewLimit");
                    if (!int.TryParse(previewLimit, out _previewLimit))
                    {
                        _previewLimit = 50;
                    }
                }
                return _previewLimit;
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
