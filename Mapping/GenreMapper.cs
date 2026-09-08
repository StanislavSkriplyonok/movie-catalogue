using MovieCatalog.Api.DTOs;
using MovieCatalog.Api.Models;

namespace MovieCatalog.Api.Mapping;

public static class GenreMapper
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }
    public static Genre ToEntity(this CreateGenreDto dto)
    {
        return new Genre
        {
            Name = dto.Name
        };
    }
}