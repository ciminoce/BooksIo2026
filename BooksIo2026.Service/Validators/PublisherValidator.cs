using BooksIo2026.Entities;
using FluentValidation;

namespace BooksIo2026.Service.Validators
{
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator() {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Publisher name is required.")
                .MaximumLength(50).WithMessage("Publisher name must not exceed 50 characters.");
            // Regla para el País: Obligatorio
            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");

            // Regla para la Fecha de Fundación: No puede ser en el futuro
            RuleFor(p => p.FoundedDate)
                .NotEmpty().WithMessage("Founded date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Founded date cannot be in the future.");

            // Regla para el Email: Opcional, pero si existe debe ser válido
            RuleFor(p => p.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(150).WithMessage("Email must not exceed 150 characters.")
                .When(p => !string.IsNullOrEmpty(p.Email));

            // Rule for IsActive
            RuleFor(p => p.IsActive)
                .NotNull().WithMessage("Activity status must be defined.");
        }
    }
}
