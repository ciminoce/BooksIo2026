using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Publisher;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Mappers;
using FluentValidation;

namespace BooksIo2026.Service.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<Publisher> _validator;

        public PublisherService(IUnitOfWork unitOfWork,
            IValidator<Publisher> validator)
        {
            _uow = unitOfWork;
            _validator = validator;
        }

        public (bool Success, List<string> Errors) Add(PublisherCreateDto publisherDto)
        {
            var publisher = PublisherMapper.ToEntity(publisherDto);
            var result = _validator.Validate(publisher);
            if (!result.IsValid)
            {

                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }
            if (!_uow.Publishers.Exist(publisher.Name, publisher.PublisherId))
            {
                try
                {
                    _uow.Publishers.Add(publisher);
                    _uow.Save();
                    return (true, new List<string>());
                }
                catch (Exception)
                {

                    return (false, new List<string>() { "Database error" });
                }

            }
            else
            {
                return (false, new List<string>() { "Publisher already exist!!!" });

            }
        }

        public (bool Success, List<string> Errors) Delete(int publisherId)
        {
            try
            {
                _uow.Publishers.Delete(publisherId);
                _uow.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {

                return (false, new List<string>() { "Database error" });
            }

        }

        public List<PublisherListDto> GetAll()
        {
            return _uow.Publishers.GetAll()
                .Select(p => PublisherMapper.ToPublisherListDto(p))
                .ToList();
        }

        public PublisherDetailsDto? GetById(int id)
        {
            var publisher = _uow.Publishers.GetById(id);
            if (publisher == null)
            {
                return null;
            }
            return PublisherMapper.ToPublisherDetailsDto(publisher);
        }

        public PublisherUpdateDto? GetForUpdate(int id)
        {
            var publisher = _uow.Publishers.GetById(id);
            if (publisher == null)
            {
                return null;
            }
            return PublisherMapper.ToPublisherUpdateDto(publisher);
        }

        public (bool Success, List<string> Errors) Update(PublisherUpdateDto publisherDto)
        {
            var publisher = _uow.Publishers.GetById(publisherDto.PublisherId);
            if (publisher == null)
            {
                return (false, new List<string>() { "Publisher not found" });

            }
            publisher.Name = publisherDto.Name;
            publisher.Country = publisherDto.Country;
            publisher.FoundedDate = publisherDto.FoundedDate;
            publisher.Email = publisherDto.Email;
            publisher.IsActive = publisherDto.IsActive;
            var result = _validator.Validate(publisher);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }
            if (!_uow.Publishers.Exist(publisher.Name, publisher.PublisherId))
            {
                try
                {
                    _uow.Publishers.Update(publisher);
                    _uow.Save();
                    return (true, new List<string>());
                }
                catch (Exception)
                {
                    return (false, new List<string>() { "Database error" });
                }
            }
            else
            {
                return (false, new List<string>() { "Publisher already exist!!!" });
            }
        }
    }
}