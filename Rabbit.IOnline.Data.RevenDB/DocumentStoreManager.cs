using Raven.Client;
using Raven.Client.Document;

namespace Rabbit.IOnline.Data.RevenDB
{
    public class DocumentStoreManager
    {
        public static IDocumentStore GetCurrent(string url, string apiKey)
        {
            return new DocumentStore()
            {
                Url = url,
                ApiKey = apiKey
            }.Initialize();
        }
    }
}