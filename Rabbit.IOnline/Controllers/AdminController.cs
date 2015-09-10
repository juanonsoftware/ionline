using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.Dapper;
using System.Configuration;
using System.Web.Mvc;

namespace Rabbit.IOnline.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMessageCounter _messageCounter;

        public AdminController()
        {
            _messageCounter = new DapperMessageCounter(ConfigurationManager.ConnectionStrings["IOnlineDb"].ConnectionString);
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
