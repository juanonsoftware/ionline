﻿using Rabbit.Cache;
using Rabbit.Cache.Redis;
using Rabbit.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IWasThere.Services
{
    public class RedisDataService : IDataService
    {
        private readonly IDataService _directService;
        private readonly ICache _redisCache;

        public RedisDataService(IDictionary<string, string> options)
        {
            _directService = new DirectDataService();
            _redisCache = new RedisLabsRedisCacheFactory().Create(options);
        }

        public IEnumerable<DataItem> GetCategories(string dataFileUrl)
        {
            return _redisCache.GetOrExecute(dataFileUrl, () =>
            {
                var categories = _directService.GetCategories(dataFileUrl).ToList();
                _redisCache.Set(dataFileUrl, categories, TimeSpan.FromHours(1));
                return categories;
            });
        }

        public string GetCredits(string creditsFileUrl)
        {
            return _redisCache.GetOrExecute(creditsFileUrl, () =>
            {
                var credits = _directService.GetCredits(creditsFileUrl);
                _redisCache.Set(creditsFileUrl, credits, TimeSpan.FromHours(1));
                return credits;
            });
        }
    }
}