using MovieCatalog.Api.DTOs;
using MovieCatalog.Api.Models;

namespace MovieCatalog.Api.Mapping;

public static class PersonMapper
{
    public static PersonDto ToDto(this Person person)
    {
        return new PersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Surname = person.Surname,
            BirthYear = person.BirthYear
        };
    }

    public static Person ToEntity(this CreatePersonDto dto)
    {
        return new Person
        {
            Name = dto.Name,
            Surname = dto.Surname,
            BirthYear = dto.BirthYear
        };
    }
}