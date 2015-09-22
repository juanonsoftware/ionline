using PagedList;
using System;

namespace Rabbit.IOnline.Models.ViewModels
{
    public class ListViewModel
    {
        public int MessageCount
        {
            get
            {
                return Messages.GetMetaData().TotalItemCount;
            }
        }

        public int StartingIndex
        {
            get
            {
                var metaData = Messages.GetMetaData();
                return (metaData.PageNumber - 1) * metaData.PageSize;
            }
        }

        public Guid? CategoryId { get; set; }

        public IPagedList<MessageViewModel> Messages { get; set; }

        public bool? PagerEnabled { get; set; }
    }
}