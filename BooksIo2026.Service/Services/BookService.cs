using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Book;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Mappers;
using FluentValidation;

namespace BooksIo2026.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IValidator<Book> _validator;
        private readonly IUnitOfWork _uow;
        public BookService(
            IValidator<Book> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _uow = unitOfWork;
        }

        public (bool Success, List<string> Errors) Add(BookCreateDto bookDto)
        {
            var book = BookMapper.toEntity(bookDto);

            var result = _validator.Validate(book);
            if (!result.IsValid)
            {

                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }
            if (!_uow.Books.Exist(book.Title, book.BookId))
            {
                try
                {
                    _uow.Books.Add(book);
                    _uow.Save();
                    return (true, new List<string>());
                }
                catch (Exception)
                {

                    return (false, new List<string>() { "Database error" });
                }

            }
            else
            {
                return (false, new List<string>() { "Book already exist!!!" });

            }
        }

        public (bool Success, List<string> Errors) Delete(int id)
        {

            try
            {
                _uow.Books.Delete(id);
                _uow.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {

                return (false, new List<string>() { "Database error" });
            }
        }

        public List<BookListDto> GetAll()
        {
            return _uow.Books.GetAll()
                .Select(b => BookMapper.ToBookListDto(b))
                .ToList();
        }

        public BookDetailDto? GetById(int id)
        {
            var book = _uow.Books.GetById(id);
            if (book == null) return null;
            return BookMapper.ToBookDetailDto(book);
        }

        public BookUpdateDto? GetForUpdate(int id)
        {
            var book = _uow.Books.GetById(id);
            if (book == null) return null;
            return BookMapper.ToBookUpdateDto(book);
        }

        public (bool Success, List<string> Errors) Update(BookUpdateDto bookDto)
        {
            //var book = BookMapper.toEntity(bookDto);
            Book? book = _uow.Books.GetById(bookDto.BookId);
            if (book == null)
            {
                return (false, new List<string>() { "Book Not Found!!!" });

            }

            book.Title = bookDto.Title;
            book.AuthorId = bookDto.AuthorId;
            book.PublisherId = bookDto.PublisherId;
            book.PublishedDate = bookDto.PublishedDate;
            book.Price = bookDto.Price;
            book.Stock = bookDto.Stock;
            book.IsActive = bookDto.IsActive;

            var result = _validator.Validate(book);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);

            }
            if (!_uow.Books.Exist(book.Title, book.BookId))
            {
                try
                {
                    //OJO VER OTRA COSA JODER!!!
                    //_repository.Update(book);
                    _uow.Save();
                    return (true, new List<string>());
                }
                catch (Exception)
                {

                    return (false, new List<string>() { "Database error" });
                }

            }
            else
            {
                return (false, new List<string>() { "Author already exist!!!" });

            }
        }

    }
}
