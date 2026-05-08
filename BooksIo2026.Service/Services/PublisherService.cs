using BooksIo2026.Data;
using BooksIo2026.Entities;
using BooksIo2026.Service.Common;
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

        public Result Add(PublisherCreateDto publisherDto)
        {
            var publisher = PublisherMapper.ToEntity(publisherDto);
            var result = _validator.Validate(publisher);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_uow.Publishers.ExistSameName(publisher.Name, publisher.PublisherId))
            {
                return Result.Failure("Publisher already exist!!!");
            }
            try
            {
                _uow.Publishers.Add(publisher);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }

        }

        public Result Delete(int publisherId)
        {
            var publisher = _uow.Publishers.GetById(publisherId);
            if (publisher==null)
            {
                return Result.Failure("Publisher Not Found");
            }
            if (_uow.Publishers.HasBooks(publisherId))
            {
                return Result.Failure("Publisher with associated books");
            }
            try
            {
                _uow.Publishers.Delete(publisherId);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }

        }

        public Result<List<PublisherListDto>> GetAll()
        {
            var publishers= _uow.Publishers.GetAll()
                .Select(p => PublisherMapper.ToPublisherListDto(p))
                .ToList();
            return Result<List<PublisherListDto>>.Success(publishers);
        }

        public Result<PublisherListDto> GetById(int id)
        {
            var publisher = _uow.Publishers.GetById(id);
            if (publisher == null)
            {
                return Result<PublisherListDto>.Failure("Publisher not found");
            }
            return Result<PublisherListDto>.Success(PublisherMapper.ToPublisherListDto(publisher));
        }

        public Result<PublisherUpdateDto> GetForUpdate(int id)
        {
            var publisher = _uow.Publishers.GetById(id);
            if (publisher == null)
            {
                return Result<PublisherUpdateDto>.Failure("Publisher not found");
            }
            return Result<PublisherUpdateDto>.Success(PublisherMapper.ToPublisherUpdateDto(publisher));
        }

        public Result Update(PublisherUpdateDto publisherDto)
        {
            var publisherToValidate = PublisherMapper.ToEntity(publisherDto);
            var result = _validator.Validate(publisherToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());

            }

            var publisher = _uow.Publishers.GetById(publisherDto.PublisherId);
            if (publisher == null)
            {
                return Result.Failure("Publisher not found");

            }
            publisher.Name = publisherDto.Name;
            publisher.Country = publisherDto.Country;
            publisher.FoundedDate = publisherDto.FoundedDate;
            publisher.Email = publisherDto.Email;
            publisher.IsActive = publisherDto.IsActive;
            if (!_uow.Publishers.ExistSameName(publisher.Name, publisher.PublisherId))
            {
                return Result.Failure("Publisher already exist!!!");
            }
            try
            {
                _uow.Publishers.Update(publisher);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
