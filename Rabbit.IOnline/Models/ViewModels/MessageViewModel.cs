using System;

namespace Rabbit.IOnline.Models.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}