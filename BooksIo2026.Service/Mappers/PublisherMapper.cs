using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Publisher;

namespace BooksIo2026.Service.Mappers
{
    public static class PublisherMapper
    {
        public static PublisherListDto ToPublisherListDto(Publisher publisher)
        {
            return new PublisherListDto
            {
                PublisherId = publisher.PublisherId,
                Name = publisher.Name,
                Country = publisher.Country
            };
        }
        public static Publisher ToEntity(PublisherCreateDto dto)
        {
            return new Publisher
            {
                Name = dto.Name,
                Country = dto.Country,
                FoundedDate = dto.FoundedDate,
                Email = dto.Email,
                IsActive = true// New publishers are active by default
            };
        }
        public static PublisherDetailsDto ToPublisherDetailsDto(Publisher publisher)
        {
            return new PublisherDetailsDto
            {
                PublisherId = publisher.PublisherId,
                Name = publisher.Name,
                Country = publisher.Country,
                FoundedDate = publisher.FoundedDate,
                Email = publisher.Email,
            };
        }
        public static PublisherUpdateDto ToPublisherUpdateDto(Publisher publisher)
        {
            return new PublisherUpdateDto
            {
                PublisherId = publisher.PublisherId,
                Name = publisher.Name,
                Country = publisher.Country,
                FoundedDate = publisher.FoundedDate,
                Email = publisher.Email,
                IsActive = publisher.IsActive
            };
        }
    }
}
