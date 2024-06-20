namespace BookApi.Domain.Features.Books;

using BookApi.Domain.Bases;

public class Book : Entity<Book>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string BookCoverUrl { get; set; }
}
