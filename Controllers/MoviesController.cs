using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Api.Data;
using MovieCatalog.Api.DTOs;
using MovieCatalog.Api.Mapping;
using MovieCatalog.Api.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/movies?genre=Sci-Fi&year=2010&sortBy=title&page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] MovieQueryParams query)
    {
        var moviesQuery = _context.Movies
        .Include(m => m.Director)
        .Include(m => m.Genres)
        .Include(m => m.Actors)
        .AsQueryable();
        
        //Filtering
        if (!string.IsNullOrWhiteSpace(query.Genre))
        {
            moviesQuery = moviesQuery.Where(m => m.Genres.Any(g => g.Name == query.Genre));
        }

        if (query.Year.HasValue)
        {
            moviesQuery = moviesQuery.Where(m => m.ReleaseYear == query.Year.Value);
        }

        // Sorting
        moviesQuery = query.SortBy?.ToLower() switch
        {
            "year" => query.Descending
                ? moviesQuery.OrderByDescending(m => m.ReleaseYear)
                : moviesQuery.OrderBy(m => m.ReleaseYear),
            _ => query.Descending
                ? moviesQuery.OrderByDescending(m => m.Title)
                : moviesQuery.OrderBy(m => m.Title)
        };

        // Pagination
        var pageSize = query.PageSize is > 0 and <= 100 ? query.PageSize : 10;
        var page = query.Page > 0 ? query.Page : 1;

        var movies = await moviesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return movies.Select(m => m.ToDto()).ToList();
    }

    // GET: api/movies/1
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Director)
            .Include(m => m.Genres)
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();
        return movie.ToDto();
    }

    // POST: api/movies
    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieDto dto)
    {
        var movie = new Movie
        {
            Title = dto.Title,
            ReleaseYear = dto.ReleaseYear,
            DirectorId = dto.DirectorId
        };

        movie.Genres = await _context.Genres
            .Where(g => dto.GenreIds.Contains(g.Id))
            .ToListAsync();

        movie.Actors = await _context.People
            .Where(p => dto.ActorIds.Contains(p.Id))
            .ToListAsync();

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        await _context.Entry(movie).Reference(m => m.Director).LoadAsync();

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie.ToDto());
    }

    // PUT: api/movie/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, UpdateMovieDto dto)
    {
        var movie = await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        movie.Title = dto.Title;
        movie.ReleaseYear = dto.ReleaseYear;
        movie.DirectorId = dto.DirectorId;

        movie.Genres = await _context.Genres
            .Where(g => dto.GenreIds.Contains(g.Id))
            .ToListAsync();

        movie.Actors = await _context.People
            .Where(p => dto.ActorIds.Contains(p.Id))
            .ToListAsync();

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/movies/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}