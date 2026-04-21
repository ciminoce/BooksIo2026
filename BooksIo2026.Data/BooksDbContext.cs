using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksIo2026.Data
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=BooksIo2026; Trusted_Connection=true; TrustServerCertificate=true;")
            //    .EnableSensitiveDataLogging()
            //    .LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder
                .UseSqlServer("Data Source=.; Initial Catalog=BooksIo2026; Trusted_Connection=true; TrustServerCertificate=true;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksDbContext).Assembly);
        }
    }
}
