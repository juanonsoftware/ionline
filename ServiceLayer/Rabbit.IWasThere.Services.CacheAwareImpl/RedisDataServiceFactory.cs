using Rabbit.Configuration;
using System.Collections.Generic;

namespace Rabbit.IWasThere.Services.CacheAwareImpl
{
    public class RedisDataServiceFactory : IDataServiceFactory
    {
        private readonly IConfiguration _configuration;

        public RedisDataServiceFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDataService Create()
        {
            var endPoint = _configuration.Get("RedisEndPoint");
            var password = _configuration.Get("RedisPassword");

            return new RedisDataService(new Dictionary<string, string>()
            {
                {"endPoint", endPoint},
                {"password", password}
            });
        }
    }
}