using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Api.DTOs;

public class PersonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int BirthYear { get; set; }
}

public class CreatePersonDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(50, MinimumLength = 1)]
    public string Surname { get; set; } = string.Empty;

    [Range(1850, 2026, ErrorMessage = "Birth year must be between 1850 and 2026")]
    public int BirthYear { get; set; }
}

public class UpdatePersonDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(50, MinimumLength = 1)]
    public string Surname { get; set; } = string.Empty;

    [Range(1850, 2026, ErrorMessage = "Birth year must be between 1850 and 2026")]
    public int BirthYear { get; set; }
}
