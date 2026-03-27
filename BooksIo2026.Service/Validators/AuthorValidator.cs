using BooksIo2026.Entities;
using FluentValidation;

namespace BooksIo2026.Service.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("First Name is required")
                .MaximumLength(50)
                .MinimumLength(3)
                .WithMessage("First Name must be between 3 and 50 characters");

            RuleFor(a => a.LastName).NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(50)
                .MinimumLength(3)
                .WithMessage("Last Name must be between 3 and 50 characters");

        }
    }
}
