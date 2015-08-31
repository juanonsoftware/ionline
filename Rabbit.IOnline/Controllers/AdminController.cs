using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
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

        public ActionResult Index()
        {
            var totalMessages = _messageRepository.Count();
            return new ContentResult()
            {
                Content = "Total messages: " + totalMessages
            };
        }
    }
}
