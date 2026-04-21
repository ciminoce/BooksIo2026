using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Data.Repositories;
using BooksIo2026.Entities;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Services;
using BooksIo2026.Service.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BooksIo2026.IoC
{
    public static class DependencyInyectionContainer
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddDbContext<BooksDbContext>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService,AuthorService>();
            services.AddScoped<IValidator<Author>,AuthorValidator>();

            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IValidator<Publisher>, PublisherValidator>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IValidator<Book>, BookValidator>();

            services.AddScoped<IUnitOfWork,UnitOfWork>();
            return services.BuildServiceProvider();
        }
    }
}
