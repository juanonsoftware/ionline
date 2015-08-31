using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IOnline.Services;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Rabbit.IOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IDataService _dataService;

        public HomeController()
        {
            _messageRepository = new EfMessageRepository();
            _dataService = new DataService();
        }

        public ActionResult Index()
        {
            var categories =
                _dataService.GetCategories(ConfigurationManager.AppSettings["CategoryDataFilePath"]).ToSelectListItems();

            var vm = new IndexViewModel()
            {
                MessageCount = _messageRepository.Count(),
                EditMessageViewModel = new EditMessageViewModel()
                {
                    Categories = categories.ToList()
                }
            };

            return View(vm);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
