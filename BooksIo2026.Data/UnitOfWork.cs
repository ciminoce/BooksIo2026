using BooksIo2026.Data.Interfaces;

namespace BooksIo2026.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BooksDbContext _context;
        public UnitOfWork(BooksDbContext context, IAuthorRepository authors,
            IPublisherRepository publishers, IBookRepository books)
        {
            _context = context;
            Authors = authors;
            Publishers=publishers;
            Books= books;
        }

        public IAuthorRepository Authors { get; }

        public IPublisherRepository Publishers { get; }

        public IBookRepository Books { get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
