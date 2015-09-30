using log4net;
using Rabbit.IOC;
using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.DocumentDB;
using SimpleInjector;
using SimpleInjector.Packaging;
using System.Configuration;

namespace Rabbit.iOnline.Ioc.SimpleInjector.Packages
{
    public class AzureDocumentDbPackage : ModuleBase, IPackage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AzureDocumentDbPackage));

        public void RegisterServices(Container container)
        {
            var documentDbAppKey = ConfigurationManager.AppSettings["DocumentDbAppKey"];
            var documentDbUri = ConfigurationManager.AppSettings["DocumentDbUri"];

            Logger.InfoFormat("DocumentDbAppKey: {0}, DocumentDbUri: {1}", documentDbAppKey, documentDbUri);

            container.RegisterPerWebRequest<IMessageRepository, DocumentDbMessageRepository>();
            container.RegisterPerWebRequest<IMessageCounter>(() => new DocumentDbMessageCounter(documentDbUri, documentDbAppKey));
        }

        public override bool IsSatisfied(object condition)
        {
            return GlobalConstants.DocumentDb.Equals(condition);
        }
    }
}