using System.Collections.Generic;
using Rabbit.Foundation.Data;

namespace Rabbit.IWasThere.Services
{
    public interface IDataService
    {
        IEnumerable<DataItem> GetCategories(string dataFileUrl);

        string GetCredits(string creditsFileUrl);
    }
}