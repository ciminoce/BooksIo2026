using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksIo2026.Data.Configurations
{
    public class PublisherEntityTypeConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasIndex(p => new { p.Name }).IsUnique().HasDatabaseName("XI_Publisher_Name");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Country)
               .IsRequired()
               .HasMaxLength(50)
               .HasDefaultValue("Unknown"); // Valor por defecto a nivel DB

            builder.Property(p => p.FoundedDate)
                   .HasColumnType("date"); // Solo guarda la fecha, sin la hora (SQL Server)

            builder.Property(p => p.Email)
                   .HasMaxLength(150)
                   .IsUnicode(false); // Los emails suelen ser caracteres estándar ASCII

            builder.Property(p => p.IsActive)
                   .HasDefaultValue(true); // Nuevos registros activos por defecto
        }
    }
}
