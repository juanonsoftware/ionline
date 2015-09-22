using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Rabbit.IOnline.Models.ViewModels
{
    public class EditMessageViewModel
    {
        public EditMessageViewModel()
        {
            IsCreatingMessage = true;
            Categories = new List<SelectListItem>();
        }

        [AllowHtml]
        public string Body { get; set; }
        public Guid CategoryId { get; set; }

        public bool IsCreatingMessage { get; private set; }
        public IList<SelectListItem> Categories { get; set; }
    }
}