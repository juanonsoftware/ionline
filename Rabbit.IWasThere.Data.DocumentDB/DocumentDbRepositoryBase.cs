using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Linq;

namespace Rabbit.IWasThere.Data.DocumentDB
{
    public abstract class DocumentDbRepositoryBase
    {
        private readonly string _endPoint;
        private readonly string _authKey;
        private readonly string _databaseId;
        private readonly string _documentCollectionId;

        protected DocumentDbRepositoryBase(string endPoint, string authKey, string databaseId, string documentCollectionId)
        {

            _endPoint = endPoint;
            _authKey = authKey;
            _databaseId = databaseId;
            _documentCollectionId = documentCollectionId;
        }

        private DocumentClient _client;
        private DocumentCollection _documentCollection;
        private Database _database;

        protected DocumentClient Client
        {
            get
            {
                return _client ?? (_client = new DocumentClient(new Uri(_endPoint), _authKey));
            }
        }

        protected Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = Client.CreateDatabaseQuery().AsEnumerable().FirstOrDefault(db => db.Id == _databaseId);
                }

                if (_database == null)
                {
                    var newDb = new Database()
                    {
                        Id = _databaseId
                    };
                    _database = Client.CreateDatabaseAsync(newDb).Result;
                }

                return _database;
            }
        }

        protected DocumentCollection Documents
        {
            get
            {
                if (_documentCollection == null)
                {
                    _documentCollection = Client.CreateDocumentCollectionQuery(Database.SelfLink)
                        .Where(dc => dc.Id == _documentCollectionId)
                        .AsEnumerable()
                        .FirstOrDefault();
                }

                if (_documentCollection == null)
                {
                    var newDocumentCollection = new DocumentCollection
                    {
                        Id = _documentCollectionId
                    };
                    _documentCollection = Client.CreateDocumentCollectionAsync(Database.SelfLink, newDocumentCollection).Result;
                }

                return _documentCollection;
            }
        }
    }
}