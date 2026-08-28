namespace MovieCatalog.Api.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Movies in this genre (many-to-many)
    public List<Movie> Movies { get; set; } = new();
}