using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksIo2026.Data
{
    public class BooksDbContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=BooksIo2026; Trusted_Connection=true; TrustServerCertificate=true;");
        }

    }
}
