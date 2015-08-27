namespace Rabbit.IWasThere.Data.EF.Migrations
{
    using Rabbit.IWasThere.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Rabbit.IWasThere.Data.EF.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Rabbit.IWasThere.Data.EF.AppDbContext";
        }

        protected override void Seed(AppDbContext context)
        {
            foreach (var message in GetMessages())
            {
                context.Messages.AddOrUpdate(message);
            }
        }

        private IEnumerable<Message> GetMessages()
        {
            return new List<Message>()
            {
                new Message()
                {
                    Id=Guid.Parse("3C7EB980-545E-4C62-B54A-B1A732FC669C"),
                    Body = "System has been initialized",
                    CreatedAt = DateTime.Now,
                },
            };
        }
    }
}
