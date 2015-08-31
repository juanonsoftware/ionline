using Rabbit.Foundation.Data;
using System.Collections.Generic;

namespace Rabbit.IOnline.Services
{
    public class DataService : IDataService
    {
        public IEnumerable<DataItem> GetCategories(string dataFileUrl)
        {
            return DataHelper.ParseJsonFromUrl(dataFileUrl);
        }
    }
}