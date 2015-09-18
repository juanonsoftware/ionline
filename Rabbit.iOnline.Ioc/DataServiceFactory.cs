using Rabbit.IWasThere.Services;
using Rabbit.IWasThere.Services.CacheAwareImpl;
using Rabbit.IWasThere.Services.DirectImpl;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Rabbit.iOnline.Ioc
{
    public class DataServiceFactory
    {
        public static IDataService Create()
        {
            var useRedisCfg = ConfigurationManager.AppSettings["UseRedis"];

            if (bool.Parse(useRedisCfg))
            {
                var endPoint = ConfigurationManager.AppSettings["RedisEndPoint"];
                var password = ConfigurationManager.AppSettings["RedisPassword"];
                return Create(true, new Dictionary<string, string>()
                {
                    {"endPoint", endPoint},
                    {"password", password}
                });
            }

            return Create(false);
        }

        public static IDataService Create(bool useRedis, IDictionary<string, string> options = null)
        {
            if (useRedis)
            {
                if (options == null)
                {
                    throw new ArgumentNullException("options");
                }

                return new RedisDataService(options);
            }

            return new DirectDataService();
        }
    }
}