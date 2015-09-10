using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Domain;
using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IOnline.Data.RevenDB
{
    public class RavenDbMessageCounter : IMessageCounter
    {
        private readonly IDocumentStore _documentStore;

        public RavenDbMessageCounter(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IDictionary<Guid, int> CountMessages()
        {
            using (var session = _documentStore.OpenSession())
            {
                var result = session.Query<Message>().AsEnumerable().GroupBy(m => m.CategoryId).ToDictionary(g => g.Key, g => g.Count());
                result.Add(Constants.GlobalCategory, CountAllMessages());

                return result;
            }
        }

        public int CountMessages(Guid categoryId)
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<Message>().Where(m => m.CategoryId == categoryId).AsEnumerable().Count();
            }
        }

        public int CountAllMessages()
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<Message>().AsEnumerable().Count();
            }
        }
    }
}
