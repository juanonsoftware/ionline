using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IOnline.Data.RevenDB;
using Raven.Client;
using System.Configuration;

namespace Rabbit.IOnline.App_Start.Autofac
{
    public class RavenDbDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var url = ConfigurationManager.AppSettings["RavenDbUrl"];
                var apiKey = ConfigurationManager.AppSettings["RavenDbApiKey"];
                return DocumentStoreManager.GetCurrent(url, apiKey);
            }).As<IDocumentStore>().SingleInstance();

            builder.RegisterType<RavenDbMessageCounter>().AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterType<RevenDbMessageRepository>().AsImplementedInterfaces().InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}