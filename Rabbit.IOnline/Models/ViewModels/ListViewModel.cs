using PagedList;

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

        public IPagedList<MessageViewModel> Messages { get; set; }
    }
}