namespace MovieCatalog.Api.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear {  get; set; }

    // Foreign key to the director
    public int DirectorId { get; set; }
    public Person Director { get; set; } = null;

    // Many-to-many with Genre
    public List<Genre> Genres { get; set; } = new();

    // Many-to-many with Person (actors)
    public List<Person> Actors { get; set; } = new();
}