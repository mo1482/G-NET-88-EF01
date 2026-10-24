# ReadMore Books — EF Core Console Application

A simple bookstore database using C#, Entity Framework Core, and SQL Server. Entity mapping uses EF Core conventions only: there are no data annotations, no `OnModelCreating`, and no Fluent API relationship configuration.

## Requirements

- .NET 10 SDK
- SQL Server LocalDB, or a SQL Server instance with a connection string you can use

## Run

```bash
dotnet restore
dotnet run
```

The project references `Microsoft.EntityFrameworkCore.SqlServer` version `10.0.0`, aligned with the .NET 10 target framework. For a production project, use a current supported servicing version that matches your target framework.

The default connection string in `Program.cs` is:

```text
Server=(localdb)\MSSQLLocalDB;Database=ReadMoreBooksDb;Trusted_Connection=True;TrustServerCertificate=True;
```

If you use a different SQL Server instance, change the `Server=...` part (for example, `Server=.;` or `Server=.\SQLEXPRESS;`).

## Entities and relationships

- `Book`: title, ISBN, price, page count, publication year, stock status, category foreign key, and authors navigation.
- `Author`: first name, last name, email, biography, date of birth, and books navigation.
- `Category`: name, description, active status, and books navigation.
- One category has many books; each book has one category.
- Books and authors have a many-to-many relationship. EF Core creates the join table by convention.

The program calls `Database.EnsureCreated()` to create the database/schema on a fresh database, inserts illustrative data only when there are no books, then queries books with their categories and authors to demonstrate the relationships.

`EnsureCreated()` is suitable for a small demonstration. Do not combine it with EF Core migrations for the same database; use one schema-management approach or the other.

## Verify in SQL Server Management Studio

Connect to the same SQL Server instance and run:

```sql
USE ReadMoreBooksDb;

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;

SELECT * FROM Books;
SELECT * FROM Authors;
SELECT * FROM Categories;
```

The table list will also contain EF Core's convention-generated join table for the Book–Author many-to-many relationship.
