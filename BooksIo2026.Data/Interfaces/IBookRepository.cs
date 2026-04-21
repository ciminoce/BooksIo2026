using BooksIo2026.Entities;

namespace BooksIo2026.Data.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book? GetById(int id);
        void Delete(int id);
        void Update(Book book);
        void Add(Book book);
        bool Exist(string title, int? bookId = null);
    }
}
