namespace ReadMoreBooks.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    // Inverse navigation property for the many-to-many Book relationship.
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
