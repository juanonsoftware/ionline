using Rabbit.Foundation.Data;
using Rabbit.SerializationMaster;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Rabbit.IWasThere.Services
{
    public class RedisDataService : IDataService
    {
        private ConnectionMultiplexer _connection;
        private readonly IDataService _directService;

        public RedisDataService(string endPoint, string password)
        {
            InitializeConfiguration(endPoint, password);
            _directService = new DirectDataService();
        }

        private void InitializeConfiguration(string endPoint, string password)
        {
            var datas = endPoint.Split(':');

            var host = datas[0];
            var port = int.Parse(datas[1]);

            var cfg = new ConfigurationOptions()
            {
                Ssl = false,
                Password = password,
            };
            cfg.EndPoints.Add(new DnsEndPoint(host, port));

            _connection = ConnectionMultiplexer.Connect(cfg);
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
                return dataOnCache.ToString().Deserialize<List<DataItem>>();
            }
        }
    }
}