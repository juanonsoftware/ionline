using System.Collections.Generic;
using Rabbit.Foundation.Data;

namespace Rabbit.IWasThere.Services
{
    public class DirectDataService : IDataService
    {
        public IEnumerable<DataItem> GetCategories(string dataFileUrl)
        {
            return DataHelper.ParseJsonFromUrl(dataFileUrl);
        }
    }
}