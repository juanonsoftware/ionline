using Rabbit.IWasThere.Data.EF;
using System.Data.Entity;

namespace Rabbit.IOnline.Models
{
    public class SystemDbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
    }
}