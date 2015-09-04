using Rabbit.Cache;
using Rabbit.Cache.Redis;
using Rabbit.Foundation.Data;
using Rabbit.Integrations.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IWasThere.Services
{
    public class RedisDataService : IDataService
    {
        private readonly IDataService _directService;
        private readonly ICache _redisCache;

        public RedisDataService(string endPoint, string password)
        {
            var connection = ConnectionMultiplexerManager.GetCurrent(endPoint, password);

            _directService = new DirectDataService();
            _redisCache = new RedisCache(connection.GetDatabase());
        }

        public IEnumerable<DataItem> GetCategories(string dataFileUrl)
        {
            var dataInCache = _redisCache.Get<List<DataItem>>(dataFileUrl);

            if (dataInCache != null)
            {
                return dataInCache;
            }

            var categories = _directService.GetCategories(dataFileUrl).ToList();
            _redisCache.Set(dataFileUrl, categories, TimeSpan.FromHours(1));
            return categories;
        }

        public string GetCredits(string creditsFileUrl)
        {
            var dataInCache = _redisCache.Get<string>(creditsFileUrl);

            if (!string.IsNullOrWhiteSpace(dataInCache))
            {
                return dataInCache;
            }

            var credits = _directService.GetCredits(creditsFileUrl);
            _redisCache.Set(creditsFileUrl, credits, TimeSpan.FromHours(1));
            return credits;
        }
    }
}