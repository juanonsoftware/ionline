using PagedList;
using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IOnline.Services;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
using Rabbit.IWasThere.Domain;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Rabbit.IOnline.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IDataService _dataService;

        public MessageController()
        {
            _messageRepository = new EfMessageRepository();
            _dataService = new DataService();
        }

        [HttpPost]
        public ActionResult Save(EditMessageViewModel message)
        {
            var recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Captcha answer cannot be empty.");
            }
            else
            {
                var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
                if (recaptchaResult != RecaptchaVerificationResult.Success)
                {
                    ModelState.AddModelError("", "Incorrect captcha answer.");
                }
            }

            if (ModelState.IsValid)
            {
                var msgEntity = new Message()
                {
                    Body = message.Body,
                    CreatedAt = DateTime.Now,
                    CategoryId = message.CategoryId,
                };
                _messageRepository.Save(msgEntity);

                return RedirectToAction("Detail", new { msgEntity.Id });
            }
            else
            {
                message.Categories =
                    _dataService.GetCategories(ConfigurationManager.AppSettings["CategoryDataFilePath"])
                        .ToSelectListItems()
                        .ToList();

                return View(message);
            }
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
            var categories =
                _dataService.GetCategories(ConfigurationManager.AppSettings["CategoryDataFilePath"]).ToList();

            var messages = _messageRepository.GetMessages(pageIndex - 1, pageSize).Select(x => new MessageViewModel()
            {
                Id = x.Id,
                Body = x.Body,
                CreatedAt = x.CreatedAt,
                CategorySelected = categories.SingleOrDefault(c => string.Equals(c.Key, x.CategoryId.ToString(), StringComparison.InvariantCultureIgnoreCase))
            });

            var pagedList = new StaticPagedList<MessageViewModel>(messages, pageIndex, pageSize, messageCount);
            var vm = new ListViewModel()
            {
                Messages = pagedList
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MessageList", vm);
            }
            else
            {
                return View(vm);
            }
        }
    }
}
