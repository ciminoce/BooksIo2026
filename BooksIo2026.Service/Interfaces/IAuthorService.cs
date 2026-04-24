using BooksIo2026.Entities;
using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Author;

namespace BooksIo2026.Service.Interfaces
{
    public interface IAuthorService
    {
        List<AuthorListDto> GetAll();
        AuthorDetailsDto? GetById(int id);
        AuthorUpdateDto? GetForUpdate(int id);
        //bool Exist(string FirstName, string LastName);
        Result Add(AuthorCreateDto authorDto);
        Result Update(AuthorUpdateDto authorDto);
        Result Delete(int id);
    }
}
