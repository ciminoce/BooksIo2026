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
        private readonly IPublisherRepository _publisherRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Publisher> _validator;

        public PublisherService(IPublisherRepository publisherRepository, IUnitOfWork unitOfWork,
            IValidator<Publisher> validator)
        {
            _publisherRepository = publisherRepository;
            _unitOfWork = unitOfWork;
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
            if (!_publisherRepository.Exist(publisher.Name, publisher.PublisherId))
            {
                try
                {
                    _publisherRepository.Add(publisher);
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
                return (false, new List<string>() { "Publisher already exist!!!" });

            }
        }

        public (bool Success, List<string> Errors) Delete(int publisherId)
        {
            try
            {
                _publisherRepository.Delete(publisherId);
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {

                return (false, new List<string>() { "Database error" });
            }

        }

        public List<PublisherListDto> GetAll()
        {
            return _publisherRepository.GetAll()
                .Select(p => PublisherMapper.ToPublisherListDto(p))
                .ToList();
        }

        public PublisherDetailsDto? GetById(int id)
        {
            var publisher = _publisherRepository.GetById(id);
            if (publisher == null)
            {
                return null;
            }
            return PublisherMapper.ToPublisherDetailsDto(publisher);
        }

        public PublisherUpdateDto? GetForUpdate(int id)
        {
            var publisher = _publisherRepository.GetById(id);
            if (publisher == null)
            {
                return null;
            }
            return PublisherMapper.ToPublisherUpdateDto(publisher);
        }

        public (bool Success, List<string> Errors) Update(PublisherUpdateDto publisherDto)
        {
            var publisher = _publisherRepository.GetById(publisherDto.PublisherId);
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
            if (!_publisherRepository.Exist(publisher.Name, publisher.PublisherId))
            {
                try
                {
                    _publisherRepository.Update(publisher);
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
                return (false, new List<string>() { "Publisher already exist!!!" });
            }
        }
    }
}