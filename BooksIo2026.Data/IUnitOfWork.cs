using BooksIo2026.Data.Interfaces;

namespace BooksIo2026.Data
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IPublisherRepository Publishers { get; }
        IBookRepository Books { get; }
        void Save();
    }
}
