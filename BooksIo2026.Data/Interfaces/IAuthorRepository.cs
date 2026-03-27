using BooksIo2026.Entities;

namespace BooksIo2026.Data.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAll();
        Author? GetById(int id);
        void Delete(int id);
        void Update(Author author);
        void Add(Author author);

    }
}
