using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IOC;
using Rabbit.IOnline.Data.RevenDB;
using Rabbit.IWasThere.Common;
using Raven.Client;
using System.Configuration;

namespace Rabbit.iOnline.Ioc.Autofac.Modules
{
    public class RavenDbDataModule : Module, IModule
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

        public int Index
        {
            get { return int.MaxValue; }
        }

        public bool IsSatisfied(object condition)
        {
            return GlobalConstants.RavenDb.Equals(condition);
        }
    }
}