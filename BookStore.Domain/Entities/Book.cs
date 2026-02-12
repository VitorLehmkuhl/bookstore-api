namespace BookStore.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Year { get; private set; }
    public string? CoverUrl { get; private set; }
    //para o Emtity Framework
    private Book() { }

    public Book(string title, string author, int year, string coverImage = "")
    {
        Id = Guid.NewGuid();
        SetTitle(title);
        SetAuthor(author);
        SetYear(year);
        SetCover(coverImage);
    }
    public void Update(string title, string author, int year, string coverImage = "")
    {
        SetTitle(title);
        SetAuthor(author);
        SetYear(year);
        SetCover(coverImage);
    }

    public void SetCover(string url)
    {
        if (!string.IsNullOrEmpty(url))
            CoverUrl = url;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");
        Title = title;
    }

    private void SetAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty");
        Author = author;
    }

    private void SetYear(int year)
    {
        if (year < 0)
            throw new ArgumentException("Invalid year");
        Year = year;
    }

}