using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IWasThere.Data.Dapper;
using Rabbit.IWasThere.Data.EF;
using System.Configuration;

namespace Rabbit.IOnline.App_Start.Autofac
{
    public class EfDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AppDbContext()).AsImplementedInterfaces().InstancePerHttpRequest();

            builder.Register(c =>
            {
                var connectionString = ConfigurationManager.ConnectionStrings["IOnlineDb"].ConnectionString;
                return new DapperMessageCounter(connectionString);
            }).AsImplementedInterfaces().InstancePerHttpRequest();

            builder.Register(c => new EfMessageRepository(c.Resolve<IDbContext>()))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}