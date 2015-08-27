using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
using Rabbit.IWasThere.Domain;
using System;
using System.Web.Mvc;

namespace Rabbit.IOnline.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public AdminController()
        {
            _messageRepository = new EfMessageRepository();
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

        public ActionResult Index()
        {
            return View();
        }
    }
}
