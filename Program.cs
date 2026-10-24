using Microsoft.EntityFrameworkCore;
using ReadMoreBooks.Data;
using ReadMoreBooks.Models;

// Change the server part if your SQL Server instance is not LocalDB.
const string connectionString =
    @"Server=(localdb)\MSSQLLocalDB;Database=ReadMoreBooksDb;Trusted_Connection=True;TrustServerCertificate=True;";

var options = new DbContextOptionsBuilder<BookstoreDbContext>()
    .UseSqlServer(connectionString)
    .Options;

using var context = new BookstoreDbContext(options);

try
{
    // Creates the database and its schema if the database does not already exist.
    bool wasCreated = context.Database.EnsureCreated();
    Console.WriteLine(wasCreated
        ? "Database and schema created successfully."
        : "Database already exists.");

    if (!context.Database.CanConnect())
    {
        Console.WriteLine("Could not connect to the database. Check the connection string and SQL Server.");
        return;
    }

    // Add demo data on the first run only, so repeated runs do not duplicate it.
    if (!context.Books.Any())
    {
        var category = new Category
        {
            Name = "Fiction",
            Description = "Fiction books and novels.",
            IsActive = true
        };

        var author1 = new Author
        {
            FirstName = "Mona",
            LastName = "Hassan",
            Email = "mona.hassan@example.com",
            Biography = "A sample author record for demonstrating the database.",
            DateOfBirth = new DateTime(1990, 5, 15)
        };

        var author2 = new Author
        {
            FirstName = "Omar",
            LastName = "Ali",
            Email = "omar.ali@example.com",
            Biography = "Another sample author record for demonstrating the relationship.",
            DateOfBirth = new DateTime(1988, 9, 20)
        };

        var book = new Book
        {
            Title = "Learning C#",
            ISBN = "9780000000019",
            Price = 250.00m,
            NumberOfPages = 320,
            PublishedYear = 2024,
            IsInStock = true,
            Category = category,
            Authors = new List<Author> { author1, author2 }
        };

        context.Books.Add(book);
        context.SaveChanges();
        Console.WriteLine("Sample data inserted successfully.");
    }

    // Query navigations to verify that the relationships work.
    var books = context.Books
        .Include(book => book.Category)
        .Include(book => book.Authors)
        .ToList();

    Console.WriteLine($"\nConnected to database: {context.Database.GetDbConnection().Database}");
    Console.WriteLine($"Books count: {context.Books.Count()}");
    Console.WriteLine($"Authors count: {context.Authors.Count()}");
    Console.WriteLine($"Categories count: {context.Categories.Count()}");

    Console.WriteLine("\nBooks and relationships:");
    foreach (var item in books)
    {
        var authorNames = string.Join(", ", item.Authors.Select(author => $"{author.FirstName} {author.LastName}"));
        Console.WriteLine($"Title: {item.Title}");
        Console.WriteLine($"ISBN: {item.ISBN}");
        Console.WriteLine($"Category: {item.Category.Name}");
        Console.WriteLine($"Authors: {authorNames}");
        Console.WriteLine($"Price: {item.Price:C}");
        Console.WriteLine($"In stock: {item.IsInStock}");
        Console.WriteLine(new string('-', 35));
    }

    Console.WriteLine("Database connection and sample queries completed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred while creating or accessing the database:");
    Console.WriteLine(ex.Message);
    Console.WriteLine("Check that SQL Server/LocalDB is installed and the connection string is correct.");
}
