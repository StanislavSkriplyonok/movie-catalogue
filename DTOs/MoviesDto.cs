namespace MovieCatalog.Api.DTOs;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string DirectorName { get; set; } = string.Empty;
    public List<string> Genres { get; set; } = new();
    public List<string> Actors { get; set; } = new();
}

public class CreateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DirectorId { get; set; }
    public List<int> GenreIds { get; set; } = new();
    public List<int> ActorIds { get; set; } = new();
}

public class UpdateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DirectorId { get; set; }
    public List<int> GenreIds { get; set; } = new();
    public List<int> ActorIds { get; set; } = new();
}