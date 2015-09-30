using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IOC;
using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Data.Dapper;
using Rabbit.IWasThere.Data.EF;
using System.Configuration;

namespace Rabbit.iOnline.Ioc.Autofac.Modules
{
    public class EfDataModule : Module, IModule
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

        public int Index
        {
            get { return int.MaxValue; }
        }

        public bool IsSatisfied(object condition)
        {
            return GlobalConstants.SqlServer.Equals(condition);
        }
    }
}