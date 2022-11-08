using CaseManagmentAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CaseManagmentAPI.DataContext
{
    public class CMDataContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            // optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }

        public DbSet<CMUser>? CMUser { get; set; }
        public DbSet<CMCase>? CMCase { get; set; }

        public DbSet<CMCaseType>? CMCaseType { get; set; }

        public DbSet<CMCustomer>? CMCustomer { get; set; }

        public DbSet<CMCustomerCare>? CMCustomerCare { get; set; }
    }

   /* public class CMDbContextFactory : IDesignTimeDbContextFactory<CMDataContext>
    {
        public CMDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CMDataContext>();
            optionsBuilder.UseSqlServer("your connection string");

            return new CMDataContext(optionsBuilder.Options);
        }
    } */

}
