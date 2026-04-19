using BooksIo2026.Data.Migrations;
using BooksIo2026.IoC;
using BooksIo2026.Service.DTOs.Author;
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
                    case "0":
                        return;
                    default:
                        break;
                }
            } while (true);
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
