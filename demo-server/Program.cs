using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddSingleton<Repository>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

app.UseCors();
app.MapGraphQL();

app.Run();

public class Query
{
    public IEnumerable<Book> GetBooks([Service] Repository repository) => repository.GetBooks();
    
    public IEnumerable<Author> GetAuthors([Service] Repository repository) => repository.GetAuthors();

    public Book? GetBook(int id, [Service] Repository repository) => repository.GetBook(id);
}

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Year { get; set; }
    public required string Genre { get; set; }
    public int AuthorId { get; set; }

    public Author? GetAuthor([Service] Repository repository) => repository.GetAuthor(AuthorId);
}

public class Author
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public IEnumerable<Book> GetBooks([Service] Repository repository) => repository.GetBooksByAuthor(Id);
}

public class Repository
{
    private readonly List<Author> _authors;
    private readonly List<Book> _books;

    public Repository()
    {
        _authors = new List<Author>
        {
            new Author { Id = 1, Name = "J.R.R. Tolkien" },
            new Author { Id = 2, Name = "Michael Crichton" },
            new Author { Id = 3, Name = "Isaac Asimov" }
        };

        _books = new List<Book>
        {
            new Book { Id = 1, Title = "The Hobbit", Year = 1937, Genre = "Fantasy", AuthorId = 1 },
            new Book { Id = 2, Title = "The Fellowship of the Ring", Year = 1954, Genre = "Fantasy", AuthorId = 1 },
            new Book { Id = 3, Title = "Jurassic Park", Year = 1990, Genre = "Sci-Fi", AuthorId = 2 },
            new Book { Id = 4, Title = "The Lost World", Year = 1995, Genre = "Sci-Fi", AuthorId = 2 },
            new Book { Id = 5, Title = "Foundation", Year = 1951, Genre = "Sci-Fi", AuthorId = 3 }
        };
    }

    public IEnumerable<Author> GetAuthors() => _authors;
    public Author? GetAuthor(int id) => _authors.FirstOrDefault(a => a.Id == id);

    public IEnumerable<Book> GetBooks() => _books;
    public Book? GetBook(int id) => _books.FirstOrDefault(b => b.Id == id);
    public IEnumerable<Book> GetBooksByAuthor(int authorId) => _books.Where(b => b.AuthorId == authorId);
}
