using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PhoneBook.DataAccess.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            // Connection string (geliştirme için)

            //•	--startup - project parametresine gerek kalmaz
            //•	DataAccess klasöründeyken direkt dotnet ef migrations add çalışır

            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=PhoneBookDb;Username=postgres;Password=saw");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}