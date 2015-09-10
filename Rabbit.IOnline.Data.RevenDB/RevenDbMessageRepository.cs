using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Domain;
using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;

namespace Rabbit.IOnline.Data.RevenDB
{
    public class RevenDbMessageRepository : IMessageRepository
    {
        private readonly IDocumentStore _documentStore;

        public RevenDbMessageRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IEnumerable<Message> GetMessages(int pageIndex, int pageSize)
        {
            var offset = pageIndex * pageSize;

            using (var session = _documentStore.OpenSession())
            {
                return session.Query<Message>()
                    .OrderByDescending(m => m.CreatedAt)
                    .Skip(offset)
                    .Take(pageSize);
            }
        }

        public IEnumerable<Message> GetMessages(Guid categoryId, int pageIndex, int pageSize)
        {
            var offset = pageIndex * pageSize;

            using (var session = _documentStore.OpenSession())
            {
                return session.Query<Message>()
                    .Where(m => m.CategoryId == categoryId)
                    .OrderByDescending(m => m.CreatedAt)
                    .Skip(offset)
                    .Take(pageSize);
            }
        }

        public Message GetById(Guid id)
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Load<Message>(id);
            }
        }

        public void Save(Message message)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(message);
                session.SaveChanges();
            }
        }
    }
}