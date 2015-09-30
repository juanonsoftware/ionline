using Rabbit.IOC;
using Rabbit.IOnline.Data.RevenDB;
using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Data;
using Raven.Client;
using SimpleInjector;
using SimpleInjector.Packaging;
using System.Configuration;

namespace Rabbit.iOnline.Ioc.SimpleInjector.Packages
{
    public class RavenDbDataPackage : ModuleBase, IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IDocumentStore>(() =>
            {
                var url = ConfigurationManager.AppSettings["RavenDbUrl"];
                var apiKey = ConfigurationManager.AppSettings["RavenDbApiKey"];
                return DocumentStoreManager.GetCurrent(url, apiKey);
            });

            container.RegisterPerWebRequest<IMessageCounter, RavenDbMessageCounter>();
            container.RegisterPerWebRequest<IMessageRepository, RevenDbMessageRepository>();
        }

        public override bool IsSatisfied(object condition)
        {
            return GlobalConstants.RavenDb.Equals(condition);
        }
    }
}