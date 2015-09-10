using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rabbit.IWasThere.Data.DocumentDB
{
    public class DocumentDbEntity<T> : Document where T : class
    {
        [JsonProperty("Id")]
        public override string Id { get; set; }

        public T Object { get; set; }

        [JsonProperty("ObjectStr")]
        public string ObjectStr { get; set; }
    }

    public class DocumentDbMessageCounter : DocumentDbRepositoryBase, IMessageCounter
    {
        public DocumentDbMessageCounter(string endPoint, string authKey)
            : base(endPoint, authKey, Constants.DatabaseId, Constants.MessagesCollectionId)
        {
        }

        public IDictionary<Guid, int> CountMessages()
        {
            throw new NotImplementedException();
        }

        int IMessageCounter.CountMessages(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public int CountAllMessages()
        {
            return Client.CreateDocumentQuery(Documents.DocumentsLink).AsEnumerable().Count();
        }
    }
}
