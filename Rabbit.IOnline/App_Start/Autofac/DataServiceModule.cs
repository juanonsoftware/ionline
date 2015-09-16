using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.Configuration;
using Rabbit.IWasThere.Services;

namespace Rabbit.IOnline.App_Start.Autofac
{
    public class DataServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => DataServiceFactory.Create()).InstancePerHttpRequest();
            builder.RegisterType<EnvironmentAwareAppSettingsConfiguration>().AsImplementedInterfaces().SingleInstance();

            base.Load(builder);
        }
    }
}