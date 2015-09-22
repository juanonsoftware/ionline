using Rabbit.Cache;
using Rabbit.Cache.Redis;
using Rabbit.Foundation.Data;
using Rabbit.IWasThere.Services.DirectImpl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IWasThere.Services.CacheAwareImpl
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

        public IEnumerable<DataItem> GetRemoteItems(string fileUri)
        {
            return _redisCache.GetOrExecute(fileUri, () =>
            {
                var categories = _directService.GetRemoteItems(fileUri).ToList();
                _redisCache.Set(fileUri, categories, TimeSpan.FromHours(1));
                return categories;
            });
        }

        public string GetRemoteContent(string fileUri)
        {
            return _redisCache.GetOrExecute(fileUri, () =>
            {
                var credits = _directService.GetRemoteContent(fileUri);
                _redisCache.Set(fileUri, credits, TimeSpan.FromHours(1));
                return credits;
            });
        }
    }
}