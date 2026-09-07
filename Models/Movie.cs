namespace MovieCatalog.Api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear {  get; set; }

    // Foreign key to the director
    public int DirectorId { get; set; }
    [ValidateNever]
    public Person Director { get; set; } = null;

    // Many-to-many with Genre
    [ValidateNever]
    public List<Genre> Genres { get; set; } = new();

    // Many-to-many with Person (actors)
    [ValidateNever]
    public List<Person> Actors { get; set; } = new();
}