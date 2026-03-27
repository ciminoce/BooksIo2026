using BooksIo2026.Entities;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Services;

namespace BooksIo2026.Consola
{
    internal class Program
    {
        static IAuthorService _service = new AuthorService();
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Library Manager");
                Console.WriteLine("1. Authors");
                Console.WriteLine("2. Books");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option:");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AuthorsMenu();
                        break;
                    case "2":
                        // BooksMenu();
                        break;
                    case "0":
                        return;
                    default:
                        break;
                }
            } while (true);
        }

        private static void AuthorsMenu()
        {
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
                        ListAuthors();
                        break;
                    case "2":
                        AddAuthor();
                        break;
                    case "3":
                        DeleteAuthor();
                        break;
                    case "4":
                        UpdateAuthor();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        break;
                }


            } while (true);

        }

        private static void UpdateAuthor()
        {
            Console.Clear();
            Console.WriteLine("Update an Author");
            ShowAuthors();
            Console.Write("Select an ID to update:");

            var authorId = int.Parse(Console.ReadLine()!);

            var authorToUpdate = _service.GetById(authorId);
            if (authorToUpdate != null)
            {
                Console.WriteLine($"Author to Update: {authorToUpdate}");

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

                    var result = _service.Update(authorToUpdate);
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

        private static void DeleteAuthor()
        {
            Console.Clear();
            Console.WriteLine("Delete an Author");
            Console.WriteLine("List of Available Authors");
            ShowAuthors();
            Console.Write("Select an ID to delete:");
            var authorId = int.Parse(Console.ReadLine()!);

            var authorToDelete = _service.GetById(authorId);
            if (authorToDelete != null)
            {
                Console.Write($"Are you sure to delete {authorToDelete} (y/n)?");
                var response = Console.ReadLine();
                if (response!.ToLower() == "y")
                {
                    var result = _service.Delete(authorToDelete.AuthorId);
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

        private static void AddAuthor()
        {
            Console.Clear();
            Console.WriteLine("Add a New Author");
            Console.Write("First Name:");
            var firstName = Console.ReadLine();
            Console.Write("Last Name:");
            var lastName = Console.ReadLine();
            var author = new Author
            {
                FirstName = firstName!,
                LastName = lastName!
            };
            var result = _service.Add(author);
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
            Console.ReadLine();

            return;

        }

        private static void ListAuthors()
        {
            Console.Clear();
            Console.WriteLine("List of Authors");
            ShowAuthors();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static void ShowAuthors()
        {
            var authors = _service.GetAll();
            foreach (var author in authors)
            {
                Console.WriteLine($"ID:{author.AuthorId} Author:{author}");
            }

        }
    }
}
