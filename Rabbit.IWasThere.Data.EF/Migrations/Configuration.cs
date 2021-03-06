using Rabbit.IWasThere.Domain;

namespace Rabbit.IWasThere.Data.EF.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = typeof(AppDbContext).FullName;
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
                    Id = Guid.Parse("3C7EB980-545E-4C62-B54A-B1A732FC669C"),
                    Body = "System has been initialized",
                    CreatedAt = DateTime.Now,
                },
            };
        }
    }
}
