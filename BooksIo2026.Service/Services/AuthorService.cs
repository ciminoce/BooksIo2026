using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Data.Repositories;
using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Author;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Mappers;
using BooksIo2026.Service.Validators;
using FluentValidation;

namespace BooksIo2026.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IValidator<Author> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public AuthorService(IAuthorRepository repository,
            IValidator<Author> validator,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _validator = validator;
            _unitOfWork= unitOfWork;
        }

        public (bool Success, List<string> Errors) Add(AuthorCreateDto authorDto)
        {
            var author = AuthorMapper.toEntity(authorDto);

            var result = _validator.Validate(author);
            if (!result.IsValid)
            {

                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }
            if (!_repository.Exist(author.FirstName, author.LastName))
            {
                try
                {
                    _repository.Add(author);
                    _unitOfWork.Save();
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

        public (bool Success, List<string> Errors) Delete(int id)
        {

            try
            {
                _repository.Delete(id);
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {

                return (false, new List<string>() { "Database error" });
            }
        }

        public List<AuthorListDto> GetAll()
        {
            return _repository.GetAll()
                .Select(a => AuthorMapper
                .ToAuthorListDto(a))
                .ToList();
        }

        public AuthorDetailsDto? GetById(int id)
        {
            var author = _repository.GetById(id);
            if (author == null) return null;
            return  AuthorMapper.toAuthorDetailsDto(author);
        }

        public AuthorUpdateDto? GetForUpdate(int id)
        {
            var author = _repository.GetById(id);
            if (author == null) return null;
            return AuthorMapper.ToAuthorUpdateDto(author);
        }

        public (bool Success, List<string> Errors) Update(AuthorUpdateDto authorDto)
        {
            //var author = AuthorMapper.toEntity(authorDto);
            Author? author=_repository.GetById(authorDto.AuthorId);
            if (author == null)
            {
                return (false, new List<string>() { "Author Not Found!!!" });

            }
           
            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;

            var result = _validator.Validate(author);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);

            }
            if (!_repository.Exist(author.FirstName, author.LastName, author.AuthorId))
            {
                try
                {
                    //OJO VER OTRA COSA JODER!!!
                    //_repository.Update(author);
                    _unitOfWork.Save();
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
