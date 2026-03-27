using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksIo2026.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public void Add(Author author)
        {
            using (var context = new BooksDbContext())
            {
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new BooksDbContext())
            {
                var author = GetById(id);
                if (author == null) return;
                context.Authors.Remove(author);
                context.SaveChanges();
            }
        }

        public List<Author> GetAll()
        {
            using (var context = new BooksDbContext())
            {
                var authors = context.Authors.AsNoTracking().ToList();
                return authors;
            }

        }

        public Author? GetById(int id)
        {
            using (var context = new BooksDbContext())
            {
                return context.Authors.Find(id);
            }
        }

        public void Update(Author author)
        {
            using (var context = new BooksDbContext())
            {
                context.Authors.Update(author);
                context.SaveChanges();
            }
        }
    }
}
