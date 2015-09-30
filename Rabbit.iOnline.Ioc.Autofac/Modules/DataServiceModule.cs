using Autofac;
using Rabbit.Configuration;
using Rabbit.IOC;
using Rabbit.IWasThere.Services;
using Rabbit.IWasThere.Services.CacheAwareImpl;
using Rabbit.IWasThere.Services.DirectImpl;

namespace Rabbit.iOnline.Ioc.Autofac.Modules
{
    public class DataServiceModule : Module, IModule
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

            builder.Register(c => c.Resolve<IDataServiceFactory>().Create()).SingleInstance();

            builder.RegisterType<AppSettings>().AsImplementedInterfaces().SingleInstance();

            base.Load(builder);
        }

        public int Index
        {
            get { return 0; }
        }

        public bool IsSatisfied(object condition)
        {
            return true;
        }
    }
}