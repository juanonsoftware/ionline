﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rabbit.Configuration;
using Rabbit.Helper;
using Rabbit.IOnline.Models.ViewModels;
using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Services;

namespace Rabbit.IOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IMessageCounter _messageCounter;
        private readonly IConfiguration _configuration;
        private readonly IAppSettings _appSettings;

        public HomeController(IMessageCounter messageCounter, IDataService dataService, IConfiguration configuration, IAppSettings appSettings)
        {
            _messageCounter = messageCounter;
            _dataService = dataService;
            _configuration = configuration;
            _appSettings = appSettings;
        }

        public ActionResult Index()
        {
            var categories = _appSettings.Categories.ToSelectListItems();

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
            object credits = _dataService.GetRemoteContent(_configuration.Get(GlobalConstants.CreditsFilePath));
            return View(credits);
        }

        [ChildActionOnly]
        public ActionResult SiteCredits()
        {
            var stats = _messageCounter.CountMessages();

            var vm = new SiteCreditsViewModel()
            {
                MessageCount = stats.First(x => x.Key == GlobalConstants.GlobalCategory).Value,
                Categories = new List<CategoryStatViewModel>()
            };

            foreach (var category in _appSettings.Categories)
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
