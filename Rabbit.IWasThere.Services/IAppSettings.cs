using System.Collections.Generic;
using Rabbit.Foundation.Data;

namespace Rabbit.IWasThere.Services
{
    public interface IAppSettings
    {
        int PageSize { get; }

        int PreviewLimit { get; }

        IList<DataItem> Categories { get; }
    }
}