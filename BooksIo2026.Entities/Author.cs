namespace BooksIo2026.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
