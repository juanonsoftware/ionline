using Rabbit.IOC;
using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.Dapper;
using Rabbit.IWasThere.Data.EF;
using SimpleInjector;
using SimpleInjector.Packaging;
using System.Configuration;

namespace Rabbit.iOnline.Ioc.SimpleInjector.Packages
{
    public class EfDataPackage : ModuleBase, IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterPerWebRequest<IDbContext>(() => new AppDbContext());

            container.RegisterPerWebRequest<IMessageCounter>(() =>
            {
                var connectionString = ConfigurationManager.ConnectionStrings["IOnlineDb"].ConnectionString;
                return new DapperMessageCounter(connectionString);
            });

            container.RegisterPerWebRequest<IMessageRepository>(() =>
            {
                var dbContext = container.GetInstance<IDbContext>();
                return new EfMessageRepository(dbContext);
            });
        }

        public override bool IsSatisfied(object condition)
        {
            return GlobalConstants.SqlServer.Equals(condition);
        }
    }
}