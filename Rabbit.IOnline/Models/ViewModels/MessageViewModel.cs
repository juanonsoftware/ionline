using System;
using Rabbit.Foundation.Data;

namespace Rabbit.IOnline.Models.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DataItem CategorySelected { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}