using Rabbit.Foundation.Data;
using Rabbit.SerializationMaster;
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
            var dataOnCache = cache.StringGet(dataFileUrl);

            if (dataOnCache.IsNull)
            {
                var values = _directService.GetCategories(dataFileUrl).ToList();
                cache.StringSet(dataFileUrl, values.Serialize(), TimeSpan.FromHours(1));
                return values;
            }
            else
            {
                return ((string)dataOnCache).Deserialize<List<DataItem>>();
            }
        }
    }
}