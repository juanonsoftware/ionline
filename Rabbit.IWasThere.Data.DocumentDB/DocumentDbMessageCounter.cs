using Microsoft.Azure.Documents.Linq;
using Rabbit.IWasThere.Common;
using Rabbit.IWasThere.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IWasThere.Data.DocumentDB
{
    public class DocumentDbMessageCounter : DocumentDbRepositoryBase, IMessageCounter
    {
        public DocumentDbMessageCounter(string endPoint, string authKey)
            : base(endPoint, authKey, Constants.DatabaseId, Constants.MessagesCollectionId)
        {
        }

        public IDictionary<Guid, int> CountMessages()
        {
            // Categories counting
            var result = Client.CreateDocumentQuery<ResourceEntity<Message>>(Documents.DocumentsLink)
                .Select(x => new { x.Object.Id, x.Object.CategoryId })
                .AsEnumerable()
                .GroupBy(x => x.CategoryId)
                .ToDictionary(x => x.Key, x => x.Count());

            // Add system counting
            result.Add(GlobalConstants.GlobalCategory, CountAllMessages());

            return result;
        }

        public int CountMessages(Guid categoryId)
        {
            return Client.CreateDocumentQuery<ResourceEntity<Message>>(Documents.DocumentsLink)
                  .Where(x => x.Object.CategoryId == categoryId)
                  .Select(x => x.Object.Id)
                  .AsEnumerable()
                  .Count();
        }

        public int CountAllMessages()
        {
            return Client.CreateDocumentQuery(Documents.DocumentsLink).Select(x => x.Id).AsEnumerable().Count();
        }
    }
}
