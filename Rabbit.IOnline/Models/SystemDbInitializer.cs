using System.Data.Entity;
using Rabbit.IWasThere.Data.EF;

namespace Rabbit.IOnline.Models
{
    public class SystemDbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {        
    }
}