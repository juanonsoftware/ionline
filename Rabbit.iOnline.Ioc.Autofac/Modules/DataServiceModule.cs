using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.Configuration;
using Rabbit.IWasThere.Services;
using Rabbit.IWasThere.Services.CacheAwareImpl;
using Rabbit.IWasThere.Services.DirectImpl;

namespace Rabbit.iOnline.Ioc.Autofac.Modules
{
    public class DataServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentAwareAppSettingsConfiguration>().AsImplementedInterfaces().SingleInstance();

            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
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
            }).AsImplementedInterfaces().SingleInstance();

            builder.Register(c => c.Resolve<IDataServiceFactory>().Create()).InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}