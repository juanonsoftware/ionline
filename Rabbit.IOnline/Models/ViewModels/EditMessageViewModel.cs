namespace Rabbit.IOnline.Models.ViewModels
{
    public class EditMessageViewModel
    {
        public EditMessageViewModel()
        {
            IsCreatingMessage = true;
        }

        public string Body { get; set; }
        public bool IsCreatingMessage { get; private set; }
    }
}