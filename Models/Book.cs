namespace ReadMoreBooks.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int NumberOfPages { get; set; }
    public int PublishedYear { get; set; }
    public bool IsInStock { get; set; }

    // EF Core convention recognizes CategoryId as a foreign key for Category.
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Many-to-many relationship with Author, discovered by conventions.
    public ICollection<Author> Authors { get; set; } = new List<Author>();
}
