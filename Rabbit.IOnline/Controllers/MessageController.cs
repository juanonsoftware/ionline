using PagedList;
using Rabbit.Foundation.Text;
using Rabbit.Helper;
using Rabbit.IOnline.Data.RevenDB;
using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Domain;
using Rabbit.IWasThere.Services;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using ServiceStack;
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
        private readonly IMessageCounter _messageCounter;

        public MessageController()
        {
            var documentStore = DocumentStoreManager.GetCurrent(ConfigurationManager.AppSettings["RavenDbUrl"], ConfigurationManager.AppSettings["RavenDbApiKey"]);

            _messageRepository = new RevenDbMessageRepository(documentStore);
            _messageCounter = new RavenDbMessageCounter(documentStore);

            _dataService = DataServiceFactory.Create();
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

        public ActionResult List(int? p, int? s, Guid? catid)
        {
            var pageIndex = p.HasValue ? p.Value : 1;
            var pageSize = s.HasValue ? s.Value : 5;

            var categories =
                _dataService.GetCategories(ConfigurationManager.AppSettings["CategoryDataFilePath"]).ToList();

            var messageCount = GetMessageCount(catid);

            var listOfMessages = catid.HasValue
                 ? _messageRepository.GetMessages(catid.Value, pageIndex - 1, pageSize).ToList()
                 : _messageRepository.GetMessages(pageIndex - 1, pageSize).ToList();

            var messages = listOfMessages.Select(x => new MessageViewModel()
            {
                Id = x.Id,
                Body = x.Body.StripMarkdownMarkup().GetSubstring(50, new string[] { " ", "." }),
                CreatedAt = x.CreatedAt,
                CategorySelected =
                    categories.SingleOrDefault(
                        c => string.Equals(c.Key, x.CategoryId.ToString(), StringComparison.InvariantCultureIgnoreCase))
            });

            var pagedList = new StaticPagedList<MessageViewModel>(messages, pageIndex, pageSize, messageCount);
            var vm = new ListViewModel()
            {
                Messages = pagedList,
                CategoryId = catid
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

        private int GetMessageCount(Guid? catid)
        {
            return catid.HasValue
                ? _messageCounter.CountMessages(catid.Value)
                : _messageCounter.CountAllMessages();
        }
    }
}
