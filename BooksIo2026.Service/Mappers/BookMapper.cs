using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Book;

namespace BooksIo2026.Service.Mappers
{
    public static class BookMapper
    {
        public static Book toEntity(BookCreateDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                PublisherId = bookDto.PublisherId,
                PublishedDate = bookDto.PublishedDate,
                Price = bookDto.Price,
                Stock = bookDto.Stock,
                IsActive = true

            };

        }

        public static BookDetailDto ToBookDetailDto(Book book)
        {
            return new BookDetailDto
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                PublisherName = book.Publisher.Name,
                PublishedDate = book.PublishedDate,
                Price = book.Price,
                Stock = book.Stock,
                IsActive = book.IsActive
            };
        }
        public static BookUpdateDto ToBookUpdateDto(Book book)
        {
            return new BookUpdateDto
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                PublishedDate = book.PublishedDate,
                Price = book.Price,
                Stock = book.Stock,
                IsActive = book.IsActive
            };
        }

        public static Book toEntity(BookUpdateDto bookDto)
        {
            return new Book
            {
                BookId = bookDto.BookId,
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                PublisherId = bookDto.PublisherId,
                PublishedDate = bookDto.PublishedDate,
                Price = bookDto.Price,
                Stock = bookDto.Stock,
                IsActive = bookDto.IsActive
            };
        }

        public static BookListDto ToBookListDto(Book b)
        {
            return new BookListDto
            {
                Title = b.Title,
                AuthorName = $"{b.Author.FirstName} {b.Author.LastName}",
                PublisherName = b.Publisher.Name,
                Price = b.Price,
                Stock = b.Stock,

            };
        }
    }
}
