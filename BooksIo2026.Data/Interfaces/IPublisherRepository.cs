using BooksIo2026.Entities;

namespace BooksIo2026.Data.Interfaces
{
    public interface IPublisherRepository
    {
        List<Publisher> GetAll();
        IQueryable<Publisher> Query();
        Publisher? GetById(int id);
        void Delete(int id);
        void Update(Publisher publisher);
        void Add(Publisher publisher);
        bool ExistSameName(string name, int? publisherId = null);
        bool HasBooks(int id);
    }
}
