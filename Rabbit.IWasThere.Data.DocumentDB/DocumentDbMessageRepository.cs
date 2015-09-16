using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Rabbit.IWasThere.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rabbit.IWasThere.Data.DocumentDB
{
    public class DocumentDbMessageRepository : DocumentDbRepositoryBase, IMessageRepository
    {
        public DocumentDbMessageRepository(string endPoint, string authKey)
            : base(endPoint, authKey, Constants.DatabaseId, Constants.MessagesCollectionId)
        {
        }

        public IEnumerable<Message> GetMessages(int pageIndex, int pageSize)
        {
            var offset = pageIndex * pageSize;

            return Client.CreateDocumentQuery<ResourceEntity<Message>>(Documents.DocumentsLink)
                .Where(x => x.Object.CategoryId != Guid.Empty)
                .OrderByDescending(x => x.Object.CreatedAt)
                .Select(x => x.Object)
                .AsEnumerable()
                .Skip(offset)
                .Take(pageSize);
        }

        public IEnumerable<Message> GetMessages(Guid categoryId, int pageIndex, int pageSize)
        {
            var options = new FeedOptions()
            {
                MaxItemCount = pageSize
            };

            return Client.CreateDocumentQuery<ResourceEntity<Message>>(Documents.DocumentsLink, options)
                .Where(x => x.Object.CategoryId == categoryId)
                .OrderByDescending(x => x.Object.CreatedAt)
                .Select(x => x.Object)
                .AsEnumerable();
        }

        public Message GetById(Guid id)
        {
            return
                Client.CreateDocumentQuery<ResourceEntity<Message>>(Documents.DocumentsLink)
                    .Where(x => x.Object.Id == id)
                    .Select(x => x.Object)
                    .AsEnumerable()
                    .FirstOrDefault();
        }

        public void Save(Message message)
        {
            Client.CreateDocumentAsync(Documents.DocumentsLink, new ResourceEntity<Message>
            {
                Object = message
            }).Wait();
        }
    }
}
