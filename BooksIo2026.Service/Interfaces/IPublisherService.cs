using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Author;
using BooksIo2026.Service.DTOs.Publisher;

namespace BooksIo2026.Service.Interfaces
{
    public interface IPublisherService
    {
        
        Result<List<PublisherListDto>> GetAll();
        Result<PublisherListDto> GetById(int id);
        Result<PublisherUpdateDto> GetForUpdate(int id);
        Result<PublisherDetailsDto> GetPublisherDetails(int id);
        Result Add(PublisherCreateDto publisher);
        Result Update(PublisherUpdateDto publisherDto);
        Result Delete(int publisherId);
    }
}
