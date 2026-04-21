using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksIo2026.Data.Configurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // 🔑 Primary Key
            builder.HasKey(b => b.BookId);

            // 📌 Propiedades
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(b => b.PublishedDate)
                .IsRequired();

            builder.Property(b => b.IsActive)
                .IsRequired();

            // 🔗 Relación con Author (muchos libros → un autor)
            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Relación con Publisher (muchos libros → un publisher)
            builder.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            // 💥 Índice compuesto
            builder.HasIndex(b => new { b.Title, b.AuthorId })
                .IsUnique()
                .HasDatabaseName("IX_Books_Title_AuthorId");
        }

    }
}
