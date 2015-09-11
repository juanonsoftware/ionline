using Rabbit.IWasThere.Data;
using System.Web.Mvc;

namespace Rabbit.IOnline.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMessageCounter _messageCounter;

        public AdminController(IMessageCounter messageCounter)
        {
            _messageCounter = messageCounter;
        }

        public ActionResult Index()
        {
            var totalMessages = _messageCounter.CountAllMessages();
            return new ContentResult()
            {
                Content = "Total messages: " + totalMessages
            };
        }
    }
}
