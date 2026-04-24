using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Author;
using BooksIo2026.Service.DTOs.Publisher;

namespace BooksIo2026.Service.Interfaces
{
    public interface IPublisherService
    {
        
        List<PublisherListDto> GetAll();
        PublisherDetailsDto? GetById(int id);
        PublisherUpdateDto? GetForUpdate(int id);
        Result Add(PublisherCreateDto publisher);
        Result Update(PublisherUpdateDto publisherDto);
        Result Delete(int publisherId);
        //void Delete(int id);
        //void Update(PublisherUpdateDto publisher);
        //void Add(PublisherAddDto publisher);
    }
}
