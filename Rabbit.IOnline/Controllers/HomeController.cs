using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.Dapper;
using Rabbit.IWasThere.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Rabbit.IOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IMessageCounter _messageCounter;

        public HomeController()
        {
            _dataService = DataServiceFactory.Create();
            _messageCounter = new DapperMessageCounter(ConfigurationManager.ConnectionStrings["IOnlineDb"].ConnectionString);
        }

        public ActionResult Index()
        {
            var categories =
                _dataService.GetCategories(ConfigurationManager.AppSettings["CategoryDataFilePath"]).ToSelectListItems();

            var vm = new IndexViewModel()
            {
                EditMessageViewModel = new EditMessageViewModel()
                {
                    Categories = categories.ToList()
                },
            };

            return View(vm);
        }

        public ActionResult About()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SiteCredits()
        {
            var stats = _messageCounter.CountMessages();

            var vm = new SiteCreditsViewModel()
            {
                MessageCount = stats.First(x => x.Key == Constants.GlobalCategory).Value,
                Categories = new List<CategoryStatViewModel>()
            };

            var categories = _dataService.GetCategories(ConfigurationManager.AppSettings["CategoryDataFilePath"]);

            foreach (var category in categories)
            {
                var categoryStatViewModel = new CategoryStatViewModel()
                {
                    Category = category,
                };

                var categoryStat = stats.SingleOrDefault(x => string.Equals(x.Key.ToString(), category.Key, StringComparison.InvariantCultureIgnoreCase));
                if (!Equals(categoryStat, default(KeyValuePair<Guid, int>)))
                {
                    categoryStatViewModel.MessageCount = categoryStat.Value;
                }

                vm.Categories.Add(categoryStatViewModel);
            }

            return PartialView(vm);
        }
    }
}
