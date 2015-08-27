using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
using Rabbit.IWasThere.Domain;
using System;
using System.Web.Mvc;

namespace Rabbit.IOnline.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController()
        {
            _messageRepository = new EfMessageRepository();
        }

        [HttpPost]
        public ActionResult Save(MessageViewModel message)
        {
            if (ModelState.IsValid)
            {
                var msgEntity = new Message()
                {
                    Body = message.Body,
                    CreatedAt = DateTime.Now,
                };
                _messageRepository.Save(msgEntity);

                return RedirectToAction("Detail", new { msgEntity.Id });
            }

            return Redirect("/");
        }

        public ActionResult Detail(Guid id)
        {
            var message = _messageRepository.GetById(id);

            var vm = new MessageViewModel()
            {
                Body = message.Body,
                CreatedAt = message.CreatedAt,
            };

            return View(vm);
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
    }
}
