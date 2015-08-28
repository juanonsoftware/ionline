using PagedList;
using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
using Rabbit.IWasThere.Domain;
using System;
using System.Linq;
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
            var pageIndex = p.HasValue ? p.Value : 1;
            var pageSize = s.HasValue ? s.Value : 5;

            var messageCount = _messageRepository.Count();
            var messages = _messageRepository.GetMessages(pageIndex - 1, pageSize).Select(x => new MessageViewModel()
            {
                Id = x.Id,
                Body = x.Body,
                CreatedAt = x.CreatedAt
            });

            var pagedList = new StaticPagedList<MessageViewModel>(messages, pageIndex, pageSize, messageCount);

            var vm = new ListViewModel()
            {
                Messages = pagedList
            };

            return View(vm);
        }
    }
}
