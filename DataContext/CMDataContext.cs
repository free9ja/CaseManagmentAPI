using CaseManagmentAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            // optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }


        public DbSet<CMUser>? CMUser { get; set; }
    }
}
