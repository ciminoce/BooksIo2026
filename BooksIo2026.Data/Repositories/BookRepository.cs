using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksIo2026.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksDbContext _context;

        public BookRepository(BooksDbContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return;
            _context.Books.Remove(book);
        }

        public bool Exist(string title, int? bookId = null)
        {
            Book? book;
            if (bookId == null)
            {
                book = _context.Books.FirstOrDefault(b => b.Title == title);
            }
            else
            {
                book = _context.Books.FirstOrDefault(b => b.Title == title && b.BookId != bookId);
            }
            return book != null;
        }

        public List<Book> GetAll()
        {
            return _context.Books
                .Include(b=> b.Author)
                .Include(b=> b.Publisher)
                .AsNoTracking().ToList();

        }

        public Book? GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }
    }
}
