using Raven.Client;
using Raven.Client.Document;

namespace Rabbit.IOnline.Data.RevenDB
{
    public class DocumentStoreManager
    {
        private static IDocumentStore _documentStore;

        public static IDocumentStore GetCurrent(string url, string apiKey)
        {
            if (_documentStore == null)
            {
                _documentStore = new DocumentStore()
                {
                    Url = url,
                    ApiKey = apiKey
                };
                _documentStore.Initialize();
            }

            return _documentStore;
        }
    }
}