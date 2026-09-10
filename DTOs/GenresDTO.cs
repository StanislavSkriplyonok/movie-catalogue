using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Api.DTOs;

public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateGenreDto
{
    [Required(ErrorMessage = "Genre name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Genre name must be between 2 and 50 characters")]
    public string Name { get; set; } = string.Empty;
}

public class UpdateGenreDto
{
    [Required(ErrorMessage = "Genre name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Genre name must be between 2 and 50 characters")]
    public string Name { get; set; } = string.Empty;
}
