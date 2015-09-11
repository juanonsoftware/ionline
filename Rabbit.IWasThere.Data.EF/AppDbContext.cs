using Rabbit.IWasThere.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Rabbit.IWasThere.Data.EF
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext()
            : base("IOnlineDb")
        {
        }

        public IDbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
