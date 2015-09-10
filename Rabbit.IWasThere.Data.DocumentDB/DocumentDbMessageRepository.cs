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

            return
                Client.CreateDocumentQuery<Message>(Documents.DocumentsLink)
                    .OrderByDescending(m => m.CreatedAt)
                    .AsEnumerable();
        }

        public IEnumerable<Message> GetMessages(Guid categoryId, int pageIndex, int pageSize)
        {
            var offset = pageIndex * pageSize;

            return
                Client.CreateDocumentQuery<Message>(Documents.DocumentsLink)
                    .Where(m => m.CategoryId == categoryId)
                    .OrderByDescending(m => m.CreatedAt)
                    .AsEnumerable()
                    .Skip(offset)
                    .Take(pageSize);
        }

        public Message GetById(Guid id)
        {
            return
                Client.CreateDocumentQuery<Message>(Documents.DocumentsLink)
                    .Where(m => m.Id == id)
                    .AsEnumerable()
                    .FirstOrDefault();
        }

        public void Save(Message message)
        {
            Client.CreateDocumentAsync(Documents.DocumentsLink, message).Wait();
        }
    }
}
