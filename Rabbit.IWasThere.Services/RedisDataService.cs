using Rabbit.Foundation.Data;
using Rabbit.Integrations.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IWasThere.Services
{
    public class RedisDataService : IDataService
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDataService _directService;

        public RedisDataService(string endPoint, string password)
        {
            _connection = ConnectionMultiplexerManager.GetCurrent(endPoint, password);
            _directService = new DirectDataService();
        }

        public IEnumerable<DataItem> GetCategories(string dataFileUrl)
        {
            var cache = _connection.GetDatabase();
            var dataInCache = cache.Get<List<DataItem>>(dataFileUrl);

            if (dataInCache == null)
            {
                var categories = _directService.GetCategories(dataFileUrl).ToList();
                cache.Set(dataFileUrl, categories, TimeSpan.FromHours(1));

                return categories;
            }
            else
            {
                return dataInCache;
            }
        }
    }
}