namespace BookApi.Api.Dtos;

public class BookDto
{
    public string Author { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime Released { get; set; }
    public string? BookCoverUrl { get; init; }
}
