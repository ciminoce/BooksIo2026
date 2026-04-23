using BooksIo2026.IoC;
using BooksIo2026.Service.DTOs.Author;
using BooksIo2026.Service.DTOs.Book;
using BooksIo2026.Service.DTOs.Publisher;
using BooksIo2026.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BooksIo2026.Consola
{
    internal class Program
    {
        static IServiceProvider provider = DependencyInyectionContainer.Configure();
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Library Manager");
                Console.WriteLine("1. Authors");
                Console.WriteLine("2. Publishers");
                Console.WriteLine("3. Books");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option:");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AuthorsMenu();
                        break;
                    case "2":
                        PublishersMenu();
                        break;
                    case "3":
                        BooksMenu();
                        break;
                    case "0":
                        return;
                    default:
                        break;
                }
            } while (true);
        }

        private static void BooksMenu()
        {
            using (var scoped = provider.CreateScope())
            {
                var bookService = scoped.ServiceProvider.GetRequiredService<IBookService>();
                var authorService = scoped.ServiceProvider.GetRequiredService<IAuthorService>();
                var publisherService = scoped.ServiceProvider.GetRequiredService<IPublisherService>();
                do
                {
                    Console.Clear();
                    Console.WriteLine("Book's Manager");
                    Console.WriteLine("1. List of Books");
                    Console.WriteLine("2. Add a Book");
                    Console.WriteLine("3. Delete a Book");
                    Console.WriteLine("4. Update a Book");
                    Console.WriteLine("0. Back to Main Menu");
                    Console.Write("Select an option:");
                    var opcion = Console.ReadLine();
                    switch (opcion)
                    {
                        case "1":
                            ListBooks(bookService);
                            break;
                        case "2":
                            AddBook(bookService, publisherService, authorService);
                            break;
                        case "3":
                            DeleteBook(bookService);
                            break;
                        case "4":
                            UpdateBook(bookService, publisherService, authorService);
                            break;
                        case "0":
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            break;
                    }


                } while (true);

            }

        }

        private static void AddBook(IBookService service, IPublisherService publisherService, IAuthorService authorService)
        {
            Console.Clear();
            Console.WriteLine("--- Add New Book ---");

            var dto = new BookCreateDto();

            Console.Write("Title: ");
            dto.Title = Console.ReadLine() ?? "";

            // 🔥 Mostrar Authors
            Console.WriteLine("\nAvailable Authors:");
            ShowAuthors(authorService);

            Console.Write("Select Author ID: ");
            if (!int.TryParse(Console.ReadLine(), out int authorId))
            {
                Console.WriteLine("Invalid Author ID");
                Console.ReadLine();
                return;
            }
            dto.AuthorId = authorId;

            // 🔥 Mostrar Publishers
            Console.WriteLine("\nAvailable Publishers:");
            ShowPublishers(publisherService);

            Console.Write("Select Publisher ID: ");
            if (!int.TryParse(Console.ReadLine(), out int publisherId))
            {
                Console.WriteLine("Invalid Publisher ID");
                Console.ReadLine();
                return;
            }
            dto.PublisherId = publisherId;

            Console.Write("Published Date (yyyy-mm-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                dto.PublishedDate = date;
            }

            Console.Write("Price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                dto.Price = price;
            }

            // 🔥 LLAMADA AL SERVICE
            var result = service.Add(dto);

            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("Book added successfully!!!");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
        private static void ListBooks(IBookService service)
        {
            Console.Clear();
            Console.WriteLine("List of Books");
            ShowBooks(service);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static void ShowBooks(IBookService service)
        {
            var books = service.GetAll();
            foreach (var book in books)
            {
                Console.WriteLine($"ID:{book.BookId,4} Title:{book.Title,-40} Author:{book.AuthorName,-30} Publisher:{book.PublisherName,-10}");
            }

        }
        private static void UpdateBook(
            IBookService service, IPublisherService publisherService,
            IAuthorService authorService)
        {
            Console.Clear();
            Console.WriteLine("Update Book");

            ShowBooks(service);

            Console.Write("Select Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid ID");
                Console.ReadLine();
                return;
            }

            var book = service.GetForUpdate(bookId);

            if (book == null)
            {
                Console.WriteLine("Book not found");
                Console.ReadLine();
                return;
            }

            // 🔹 Title
            Console.Write($"Title ({book.Title}): ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                book.Title = input;

            // 🔥 Author
            Console.WriteLine("\nAuthors:");
            ShowAuthors(authorService);

            Console.Write($"AuthorId ({book.AuthorId}): ");
            input = Console.ReadLine();
            if (int.TryParse(input, out int authorId))
                book.AuthorId = authorId;

            // 🔥 Publisher
            Console.WriteLine("\nPublishers:");
            ShowPublishers(publisherService);

            Console.Write($"PublisherId ({book.PublisherId}): ");
            input = Console.ReadLine();
            if (int.TryParse(input, out int publisherId))
                book.PublisherId = publisherId;

            // 🔹 Fecha
            Console.Write($"Published Date ({book.PublishedDate:yyyy-MM-dd}): ");
            input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime date))
                book.PublishedDate = date;

            // 🔹 Precio
            Console.Write($"Price ({book.Price}): ");
            input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal price))
                book.Price = price;

            // 🔹 Activo
            Console.Write($"Is Active ({book.IsActive}): ");
            input = Console.ReadLine();
            if (bool.TryParse(input, out bool isActive))
                book.IsActive = isActive;

            // 🔥 Llamada al service
            var result = service.Update(book);

            if (!result.Success)
            {
                foreach (var error in result.Errors)
                    Console.WriteLine(error);
            }
            else
            {
                Console.WriteLine("Book updated successfully!");
            }

            Console.ReadLine();
        }
        private static void DeleteBook(IBookService service)
        {
            Console.Clear();
            Console.WriteLine("Delete Book");

            ShowBooks(service);

            Console.Write("Select Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid ID");
                Console.ReadLine();
                return;
            }

            Console.Write("Are you sure? (y/n): ");
            var confirm = Console.ReadLine();

            if (confirm?.ToLower() != "y")
            {
                Console.WriteLine("Cancelled");
                Console.ReadLine();
                return;
            }

            var result = service.Delete(bookId);

            if (!result.Success)
            {
                foreach (var error in result.Errors)
                    Console.WriteLine(error);
            }
            else
            {
                Console.WriteLine("Book deleted successfully!");
            }

            Console.ReadLine();
        }

        private static void PublishersMenu()
        {
            using (var scoped = provider.CreateScope())
            {
                var service = scoped.ServiceProvider.GetRequiredService<IPublisherService>();
                do
                {
                    Console.Clear();
                    Console.WriteLine("Publisher's Manager");
                    Console.WriteLine("1. List of Publishers");
                    Console.WriteLine("2. Add a Publisher");
                    Console.WriteLine("3. Delete a Publisher");
                    Console.WriteLine("4. Update a Publisher");
                    Console.WriteLine("0. Back to Main Menu");
                    Console.Write("Select an option:");
                    var opcion = Console.ReadLine();
                    switch (opcion)
                    {
                        case "1":
                            ListPublishers(service);
                            break;
                        case "2":
                            AddPublisher(service);
                            break;
                        case "3":
                            DeletePublisher(service);
                            break;
                        case "4":
                            UpdatePublisher(service);
                            break;
                        case "0":
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            break;
                    }


                } while (true);

            }
        }

        private static void UpdatePublisher(IPublisherService service)
        {
            Console.Clear();
            Console.WriteLine("Update a Publisher");
            Console.WriteLine("List of Available Publishers");
            ShowPublishers(service);
            Console.Write("Select an ID to update:");
            var publisherId = int.Parse(Console.ReadLine()!);

            var publisherToUpdate = service.GetForUpdate(publisherId);
            if (publisherToUpdate != null)
            {


                Console.Write("Name (current: {0}): ", publisherToUpdate.Name);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    publisherToUpdate.Name = input;
                }

                Console.Write("Country (current: {0}): ", publisherToUpdate.Country);
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    publisherToUpdate.Country = input;
                }

                Console.Write("Founded Date (current: {0}, yyyy-mm-dd): ", publisherToUpdate.FoundedDate.ToString("yyyy-MM-dd"));
                input = Console.ReadLine();
                if (DateTime.TryParse(input, out DateTime date))
                {
                    publisherToUpdate.FoundedDate = date;
                }

                Console.Write("Email (current: {0}): ", publisherToUpdate.Email);
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    publisherToUpdate.Email = input;
                }

                Console.Write("Is Active (current: {0}, true/false): ", publisherToUpdate.IsActive);
                input = Console.ReadLine();
                if (bool.TryParse(input, out bool isActive))
                {
                    publisherToUpdate.IsActive = isActive;
                }

                var result = service.Update(publisherToUpdate);
                if (!result.Success)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error);
                    }
                }
                else
                {
                    Console.WriteLine("Publisher updated successfully!!!");
                }
            }
            else
            {
                Console.WriteLine("Publisher does not exist");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static void DeletePublisher(IPublisherService service)
        {
            Console.Clear();
            Console.WriteLine("Delete a Publisher");
            Console.WriteLine("List of Available Publishers");
            ShowPublishers(service);
            Console.Write("Select an ID to delete:");
            var publisherId = int.Parse(Console.ReadLine()!);

            var publisherToDelete = service.GetById(publisherId);
            if (publisherToDelete != null)
            {
                Console.Write($"Are you sure to delete {publisherToDelete.Name} (y/n)?");
                var response = Console.ReadLine();
                if (response!.ToLower() == "y")
                {
                    var result = service.Delete(publisherToDelete.PublisherId);
                    if (!result.Success)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Publisher successfully deleted!!!");

                    }

                }
                else
                {
                    Console.WriteLine("Cancelled by user!!!");
                }
            }
            else
            {
                Console.WriteLine("Author does not exist");
            }
            Console.WriteLine("Key to continue");
            Console.ReadLine();
        }

        private static void AddPublisher(IPublisherService service)
        {
            Console.Clear();
            Console.WriteLine("--- Add New Publisher ---");

            var dto = new PublisherCreateDto();

            Console.Write("Name: ");
            dto.Name = Console.ReadLine() ?? "";

            Console.Write("Country: ");
            dto.Country = Console.ReadLine() ?? "";

            Console.Write("Founded Date (yyyy-mm-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                dto.FoundedDate = date;
            }

            Console.Write("Email (optional): ");
            dto.Email = Console.ReadLine();
            var result = service.Add(dto);
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("Publisher added succesfully!!!");

            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

        }

        private static void ListPublishers(IPublisherService service)
        {

            Console.Clear();
            Console.WriteLine("List of Publishers");
            ShowPublishers(service);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static void ShowPublishers(IPublisherService service)
        {
            var publishers = service.GetAll();
            foreach (var publisher in publishers)
            {
                Console.WriteLine($"ID:{publisher.PublisherId,4} Publisher:{publisher.Name,-30} Country:{publisher.Country,-30}");
            }

        }
        private static void AuthorsMenu()
        {
            using (var scoped = provider.CreateScope())
            {
                var service = scoped.ServiceProvider.GetRequiredService<IAuthorService>();
                do
                {
                    Console.Clear();
                    Console.WriteLine("Author's Manager");
                    Console.WriteLine("1. List of Authors");
                    Console.WriteLine("2. Add an Author");
                    Console.WriteLine("3. Delete an Author");
                    Console.WriteLine("4. Update an Author");

                    Console.WriteLine("0. Back to Main Menu");
                    Console.Write("Select an option:");
                    var opcion = Console.ReadLine();
                    switch (opcion)
                    {
                        case "1":
                            ListAuthors(service);
                            break;
                        case "2":
                            AddAuthor(service);
                            break;
                        case "3":
                            DeleteAuthor(service);
                            break;
                        case "4":
                            UpdateAuthor(service);
                            break;
                        case "0":
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            break;
                    }


                } while (true);

            }
        }

        private static void UpdateAuthor(IAuthorService service)
        {
            Console.Clear();
            Console.WriteLine("Update an Author");
            ShowAuthors(service);
            Console.Write("Select an ID to update:");

            var authorId = int.Parse(Console.ReadLine()!);

            var authorToUpdate = service.GetForUpdate(authorId);
            if (authorToUpdate != null)
            {
                Console.WriteLine($"Author to Update: {authorToUpdate.FirstName} {authorToUpdate.LastName}");

                Console.Write("New First Name (ENTER to keep the same):");
                var inputFirstName = Console.ReadLine();
                var newFirstName = !string
                    .IsNullOrWhiteSpace(inputFirstName)
                    ? inputFirstName : authorToUpdate.FirstName;

                Console.Write("New Last Name (ENTER to keep the same):");
                var inputLastName = Console.ReadLine();
                var newLastName = !string
                    .IsNullOrWhiteSpace(inputLastName)
                    ? inputLastName : authorToUpdate.LastName;

                Console.Write("Confirm the changes?(y/n):");
                var response = Console.ReadLine();
                if (response!.ToLower() == "y")
                {
                    authorToUpdate.FirstName = newFirstName;
                    authorToUpdate.LastName = newLastName;

                    var result = service.Update(authorToUpdate);
                    if (!result.Success)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Author successfully updated!!!");

                    }
                }
                else
                {
                    Console.WriteLine("Cancelled by user");
                }
            }
            else
            {
                Console.WriteLine("Author does not exist");
            }
            Console.WriteLine("Key to continue");
            Console.ReadLine();
        }

        private static void DeleteAuthor(IAuthorService service)
        {
            Console.Clear();
            Console.WriteLine("Delete an Author");
            Console.WriteLine("List of Available Authors");
            ShowAuthors(service);
            Console.Write("Select an ID to delete:");
            var authorId = int.Parse(Console.ReadLine()!);

            var authorToDelete = service.GetById(authorId);
            if (authorToDelete != null)
            {
                Console.Write($"Are you sure to delete {authorToDelete.FirstName} {authorToDelete.LastName} (y/n)?");
                var response = Console.ReadLine();
                if (response!.ToLower() == "y")
                {
                    var result = service.Delete(authorToDelete.AuthorId);
                    if (!result.Success)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Author successfully deleted!!!");

                    }

                }
                else
                {
                    Console.WriteLine("Cancelled by user!!!");
                }
            }
            else
            {
                Console.WriteLine("Author does not exist");
            }
            Console.WriteLine("Key to continue");
            Console.ReadLine();
        }

        private static void AddAuthor(IAuthorService service)
        {
            Console.Clear();
            Console.WriteLine("Add a New Author");
            Console.Write("First Name:");
            var firstName = Console.ReadLine();
            Console.Write("Last Name:");
            var lastName = Console.ReadLine();
            var authorDto = new AuthorCreateDto
            {
                FirstName = firstName!,
                LastName = lastName!
            };
            var result = service.Add(authorDto);
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("Author added succesfully!!!");

            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();


        }

        private static void ListAuthors(IAuthorService service)
        {
            Console.Clear();
            Console.WriteLine("List of Authors");
            ShowAuthors(service);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static void ShowAuthors(IAuthorService service)
        {
            var authors = service.GetAll();
            foreach (var author in authors)
            {
                Console.WriteLine($"ID:{author.AuthorId,4} Author:{author.FullName,-30}");
            }

        }
    }
}
