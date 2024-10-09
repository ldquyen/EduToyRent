using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EduToyRent.DAL.Context
{
    public class EduToyRentDbContextFactory : IDesignTimeDbContextFactory<EduToyRentDbContext>
    {
        public EduToyRentDbContext CreateDbContext(string[] args)
        {
            
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var config = new ConfigurationBuilder()
                .SetBasePath(path + "\\EduToyRent.API") 
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EduToyRentDbContext>();
            var connectionString = config.GetConnectionString("MyDb");

            optionsBuilder.UseSqlServer(connectionString);

            return new EduToyRentDbContext(optionsBuilder.Options);
        }
    }
}
