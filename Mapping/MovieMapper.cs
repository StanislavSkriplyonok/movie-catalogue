using MovieCatalog.Api.DTOs;
using MovieCatalog.Api.Models;

namespace MovieCatalog.Api.Mapping;

public static class MovieMapper
{
    public static MovieDto ToDto(this Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            DirectorName = $"{movie.Director.Name} {movie.Director.Surname}",
            Genres = movie.Genres.Select(g => g.Name).ToList(),
            Actors = movie.Actors.Select(a => $"{a.Name} {a.Surname}").ToList(),
        };
    }
}