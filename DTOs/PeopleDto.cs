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
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int BirthYear { get; set; }
}

public class UpdatePersonDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int BirthYear { get; set; }
}
