using Rabbit.Foundation.Data;
using System.Collections.Generic;

namespace Rabbit.IWasThere.Services
{
    public interface IDataService
    {
        IEnumerable<DataItem> GetCategories(string dataFileUrl);

        string GetRemoteContent(string creditsFileUrl);
    }
}