using System.Collections.Generic;

namespace Rabbit.IOnline.Models.ViewModels
{
    public class SiteCreditsViewModel
    {
        public int MessageCount { get; set; }
        public IList<CategoryStatViewModel> Categories { get; set; }
    }
}