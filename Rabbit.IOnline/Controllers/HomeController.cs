using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
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

        public ActionResult About()
        {
            return View();
        }
    }
}
