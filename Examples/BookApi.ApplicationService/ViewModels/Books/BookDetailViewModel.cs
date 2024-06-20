namespace BookApi.ApplicationService.ViewModels.Books;
public record BookDetailViewModel
{
    public BookDetailViewModel(Guid id, string name, string author)
    {
        Id = id;
        Name = name;
        Author = author;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
}
