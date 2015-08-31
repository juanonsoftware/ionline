using Rabbit.Foundation.Data;
using System.Collections.Generic;

namespace Rabbit.IOnline.Services
{
    public interface IDataService
    {
        IEnumerable<DataItem> GetCategories(string dataFileUrl);
    }
}