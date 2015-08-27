using Rabbit.IWasThere.Data.EF;
using Rabbit.IWasThere.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Rabbit.IWasThere.Models
{
    public class SystemDbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {        
    }
}