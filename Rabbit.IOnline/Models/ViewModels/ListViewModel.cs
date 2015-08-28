using System.Collections.Generic;

namespace Rabbit.IOnline.Models.ViewModels
{
    public class ListViewModel
    {
        public int MessageCount { get; set; }
        public int StartIndex { get; set; }
        public IList<MessageViewModel> Messages { get; set; }
    }
}