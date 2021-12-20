using Models;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Logger> Logs { get; set; }
        public DataContext() : base("AirFieldLogs")
        {
            _ = SqlProviderServices.Instance;
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }
    }
}
