using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using Rabbit.Configuration;
using Rabbit.IWasThere.Data.DocumentDB;
using System.Configuration;

namespace Rabbit.iOnline.Ioc.Autofac.Modules
{
    public class AzureDocumentDbDataModule : Module
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AzureDocumentDbDataModule));

        protected override void Load(ContainerBuilder builder)
        {
            var documentDbAppKey = ConfigurationManager.AppSettings["DocumentDbAppKey"];
            var documentDbUri = ConfigurationManager.AppSettings["DocumentDbUri"];

            Logger.InfoFormat("DocumentDbAppKey: {0}, DocumentDbUri: {1}", documentDbAppKey, documentDbUri);

            builder.Register(
                c => new DocumentDbMessageRepository(c.Resolve<IConfiguration>()))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            builder.Register(
                c => new DocumentDbMessageCounter(documentDbUri, documentDbAppKey))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}