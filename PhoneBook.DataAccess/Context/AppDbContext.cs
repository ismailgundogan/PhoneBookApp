using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Entity;

namespace PhoneBook.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Personel> Personels { get; set; }
    }
}
