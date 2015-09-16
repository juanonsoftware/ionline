using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using Rabbit.IWasThere.Data.DocumentDB;
using System.Configuration;

namespace Rabbit.IOnline.App_Start.Autofac
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
                c => new DocumentDbMessageRepository(documentDbUri, documentDbAppKey))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            builder.Register(
                c => new DocumentDbMessageCounter(documentDbAppKey, documentDbUri))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}