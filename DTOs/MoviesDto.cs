using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Range(1888, 2026, ErrorMessage = "Release year must be between 1888 and 2026")]
    public int ReleaseYear { get; set; }

    [Required(ErrorMessage = "Director is required")]
    public int DirectorId { get; set; }
    public List<int> GenreIds { get; set; } = new();
    public List<int> ActorIds { get; set; } = new();
}

public class UpdateMovieDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Range(1888, 2026, ErrorMessage = "Release year must be between 1888 and 2026")]
    public int ReleaseYear { get; set; }

    [Required(ErrorMessage = "Director is required")]
    public int DirectorId { get; set; }
    public List<int> GenreIds { get; set; } = new();
    public List<int> ActorIds { get; set; } = new();
}