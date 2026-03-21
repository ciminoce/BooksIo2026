using BooksIo2026.Data;
using BooksIo2026.Entities;

namespace BooksIo2026.Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Library Manager");
                Console.WriteLine("1. Authors");
                Console.WriteLine("2. Books");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option:");
                var option= Console.ReadLine();
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
                       // DeleteAuthor();
                        break;
                    case "4":
                       // UpdateAuthor();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        break;
                }


            } while (true);

        }

        private static void AddAuthor()
        {
            Console.Clear();
            Console.WriteLine("Add a New Author");
            Console.Write("First Name:");
            var firstName= Console.ReadLine();
            Console.Write("Last Name:");
            var lastName=Console.ReadLine();
            if(string.IsNullOrEmpty(firstName)|| string.IsNullOrEmpty(lastName))
            {
                Console.WriteLine("FirstName or LastName are required");
            }
            else
            {
                var author = new Author
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                using (var context = new BooksDbContext())
                {
                    context.Authors.Add(author);
                    context.SaveChanges();
                }
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
            using (var context=new BooksDbContext())
            {
                var authors = context.Authors.ToList();
                foreach (var author in authors)
                {
                    Console.WriteLine($"ID:{author.AuthorId} Author:{author}");
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }
        }
    }
}
