using Microsoft.Azure.Documents;

namespace Rabbit.IWasThere.Data.DocumentDB
{
    public class ResourceEntity<T> : Resource where T : class
    {
        public T Object { get; set; }
    }
}