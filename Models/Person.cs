namespace MovieCatalog.Api.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int BirthYear { get; set; }

    // Movies this person directed
    public List<Movie> DirectedMovies { get; set; } = new();

    // Movies this person acted in (many-to-many via MovieActor)
    public List<Movie> ActedMovies { get; set; } = new();
}