using Rabbit.Foundation.Data;
using System.Collections.Generic;

namespace Rabbit.IWasThere.Services
{
    public interface IDataService
    {
        IEnumerable<DataItem> GetRemoteItems(string fileUri);

        string GetRemoteContent(string fileUri);
    }
}