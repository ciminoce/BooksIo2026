using BooksIo2026.Entities;
using FluentValidation;

namespace BooksIo2026.Service.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Must(t => !string.IsNullOrWhiteSpace(t))
                .WithMessage("Title cannot be empty or whitespace.")
                .MaximumLength(200)
                .WithMessage("Title cannot exceed 200 characters.");

            RuleFor(b => b.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.")
                .LessThan(1000000)
                .WithMessage("Price is unrealistically high.");

            RuleFor(b => b.PublishedDate)
                .Must(d => d != default)
                .WithMessage("Publish date is required.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Publish date cannot be in the future.");

            RuleFor(b => b.AuthorId)
                .GreaterThan(0)
                .WithMessage("AuthorId must be greater than zero.");

            RuleFor(b => b.PublisherId)
                .GreaterThan(0)
                .WithMessage("PublisherId must be greater than zero.");

            RuleFor(b=> b.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock cannot be negative.");
        }
    }
}
