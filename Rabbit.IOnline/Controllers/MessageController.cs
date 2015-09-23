using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Rabbit.Foundation.Text;
using Rabbit.Helper;
using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Domain;
using Rabbit.IWasThere.Services;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using ServiceStack;

namespace Rabbit.IOnline.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageCounter _messageCounter;
        private readonly IAppSettings _appSettings;

        public MessageController(IMessageRepository messageRepository, IMessageCounter messageCounter, IAppSettings appSettings)
        {
            _messageRepository = messageRepository;
            _messageCounter = messageCounter;
            _appSettings = appSettings;
        }

        [HttpPost]
        public ActionResult Save(EditMessageViewModel message)
        {
            if (ModelState.IsValid)
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

            message.Categories = _appSettings.Categories.ToSelectListItems().ToList();

            return View(message);
        }

        public ActionResult Detail(Guid id)
        {
            var message = _messageRepository.GetById(id);

            var vm = new MessageViewModel()
            {
                Body = message.Body,
                CreatedAt = message.CreatedAt,
                CategorySelected = _appSettings.Categories.FirstOrDefault(x => string.Equals(x.Key, message.CategoryId.ToString(), StringComparison.InvariantCultureIgnoreCase))
            };

            return View(vm);
        }

        public ActionResult List(int? p, int? s, Guid? catid, bool? pager)
        {
            var pageIndex = p.HasValue ? p.Value : 1;
            var pageSize = s.HasValue ? s.Value : _appSettings.PageSize;

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
                        _appSettings.Categories.SingleOrDefault(
                            c => x.CategoryId.ToString().Equals(c.Key, StringComparison.InvariantCultureIgnoreCase))
                });

            var pagedList = new StaticPagedList<MessageViewModel>(messages, pageIndex, pageSize, messageCount);
            var vm = new ListViewModel()
            {
                Messages = pagedList,
                CategoryId = catid,
                PagerEnabled = pager.HasValue ? pager.Value : true
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
