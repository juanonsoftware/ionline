using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IWasThere.Services;

namespace Rabbit.IOnline.App_Start.Autofac
{
    public class DataServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => DataServiceFactory.Create()).InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}