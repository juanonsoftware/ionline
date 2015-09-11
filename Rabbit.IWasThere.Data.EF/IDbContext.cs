using System.Data.Entity;
using Rabbit.IWasThere.Domain;

namespace Rabbit.IWasThere.Data.EF
{
    public interface IDbContext
    {
        IDbSet<Message> Messages { get; set; }
    }
}