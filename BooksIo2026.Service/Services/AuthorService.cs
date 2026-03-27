using BooksIo2026.Data.Interfaces;
using BooksIo2026.Data.Repositories;
using BooksIo2026.Entities;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Validators;
using System.ComponentModel;

namespace BooksIo2026.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly AuthorValidator _validator;
        public AuthorService()
        {
            _repository = new AuthorRepository();
            _validator = new AuthorValidator();
        }

        public (bool Success, List<string>Errors) Add(Author author)
        {
            
            var result = _validator.Validate(author);
            if (!result.IsValid)
            {

                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }
            try
            {
                _repository.Add(author);
                return (true, new List<string>());
            }
            catch (Exception)
            {

                return (false, new List<string>() { "Database error" });
            }
        }

        public (bool Success, List<string> Errors) Delete(int id)
        {
            
            try
            {
                _repository.Delete(id);
                return (true, new List<string>());
            }
            catch (Exception)
            {

                return (false,new List<string>() { "Database error" });
            }
        }

        public List<Author> GetAll()
        {
            return _repository.GetAll();
        }

        public Author? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public (bool Success, List<string> Errors) Update(Author author)
        {
            var result=_validator.Validate(author);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);

            }
            try
            {
                _repository.Update(author);
                return (true,new List<string>());
            }
            catch (Exception)
            {

                return (false, new List<string>() { "Database error" });
            }
        }
    }
}
