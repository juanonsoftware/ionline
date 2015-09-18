using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.Configuration;

namespace Rabbit.IOnline.App_Start.Autofac
{
    public class DataServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentAwareAppSettingsConfiguration>().AsImplementedInterfaces().SingleInstance();

            builder.Register(c => DataServiceFactory.Create()).InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}