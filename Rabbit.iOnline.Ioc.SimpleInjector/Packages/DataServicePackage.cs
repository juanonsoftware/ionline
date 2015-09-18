using Rabbit.Configuration;
using Rabbit.IWasThere.Services;
using Rabbit.IWasThere.Services.CacheAwareImpl;
using Rabbit.IWasThere.Services.DirectImpl;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Rabbit.iOnline.Ioc.SimpleInjector.Packages
{
    public class DataServicePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IConfiguration, EnvironmentAwareAppSettingsConfiguration>();
            container.RegisterSingleton(() =>
            {
                var config = container.GetInstance<IConfiguration>();
                var useRedis = config.Get("UseRedis");

                IDataServiceFactory factory;

                if (bool.Parse(useRedis))
                {
                    factory = new RedisDataServiceFactory(config);
                }
                else
                {
                    factory = new DirectDataServiceFactory();
                }

                return factory;
            });

            container.RegisterPerWebRequest(() => container.GetInstance<IDataServiceFactory>().Create());

        }
    }
}