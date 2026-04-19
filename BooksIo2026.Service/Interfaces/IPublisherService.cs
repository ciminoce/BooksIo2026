using BooksIo2026.Service.DTOs.Author;
using BooksIo2026.Service.DTOs.Publisher;

namespace BooksIo2026.Service.Interfaces
{
    public interface IPublisherService
    {
        
        List<PublisherListDto> GetAll();
        PublisherDetailsDto? GetById(int id);
        PublisherUpdateDto? GetForUpdate(int id);
        (bool Success, List<string> Errors) Add(PublisherCreateDto publisher);
        (bool Success, List<string> Errors) Update(PublisherUpdateDto publisherDto);
        (bool Success, List<string> Errors) Delete(int publisherId);
        //void Delete(int id);
        //void Update(PublisherUpdateDto publisher);
        //void Add(PublisherAddDto publisher);
    }
}
