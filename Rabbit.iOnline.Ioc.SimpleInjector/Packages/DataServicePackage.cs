using Rabbit.Configuration;
using Rabbit.IOC;
using Rabbit.IWasThere.Services;
using Rabbit.IWasThere.Services.CacheAwareImpl;
using Rabbit.IWasThere.Services.DirectImpl;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Rabbit.iOnline.Ioc.SimpleInjector.Packages
{
    public class DataServicePackage : ModuleBase, IPackage
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

            container.RegisterSingleton(() => container.GetInstance<IDataServiceFactory>().Create());
            container.RegisterSingleton<IAppSettings, AppSettings>();
        }

        public override int Index
        {
            get { return 0; }
        }
    }
}
