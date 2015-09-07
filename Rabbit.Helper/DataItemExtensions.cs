using Rabbit.Foundation.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Rabbit.Helper
{
    public static class DataItemExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<DataItem> items)
        {
            return items.Select(x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.Key,
            });
        }
    }
}
