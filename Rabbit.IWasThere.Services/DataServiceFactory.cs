using System;
using System.Collections.Generic;
using System.Configuration;

namespace Rabbit.IWasThere.Services
{
    public class DataServiceFactory
    {
        public static IDataService Create()
        {
            var useRedisCfg = ConfigurationManager.AppSettings["UseRedis"];

            if (bool.Parse(useRedisCfg))
            {
                var endPoint = ConfigurationManager.AppSettings["EndPoint"];
                var password = ConfigurationManager.AppSettings["Password"];
                return Create(true, new Dictionary<string, string>()
                {
                    {"EndPoint", endPoint},
                    {"Password", password}
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

                var endPoint = options["EndPoint"];
                var password = options["Password"];

                return new RedisDataService(endPoint, password);
            }
            return new DirectDataService();
        }
    }
}