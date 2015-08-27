using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
using Rabbit.IWasThere.Domain;
using Rabbit.IWasThere.Models.ViewModels;
using System;
using System.Web.Mvc;

namespace Rabbit.IOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public HomeController()
        {
            _messageRepository = new EfMessageRepository();
        }

        public ActionResult Index()
        {
            var vm = new IndexViewModel()
            {
                MessageCount = _messageRepository.Count()
            };

            return View(vm);
        }

        public ActionResult GenerateMessages()
        {
            _messageRepository.Save(new Message()
            {
                Body = "Msg at " + DateTime.Now,
                CreatedAt = DateTime.Now
            });
            return new ContentResult();
        }

        public ActionResult List(int? p, int? s)
        {
            var page = p.HasValue ? p.Value : 0;
            var size = s.HasValue ? s.Value : 20;

            var messages = _messageRepository.GetMessages(page, size);
            var vm = new ListViewModel()
            {
                MessageCount = _messageRepository.Count()
            };

            return View(vm);
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
