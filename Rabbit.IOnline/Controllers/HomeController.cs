﻿using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IOnline.Services;
using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.Dapper;
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
            _dataService = new DataService();
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
                CategoryMessageCount =
                    stats.Where(x => x.Key != Constants.GlobalCategory).ToDictionary(x => x.Key, x => x.Value)
            };

            return PartialView(vm);
        }
    }
}
