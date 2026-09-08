using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Api.Data;
using MovieCatalog.Api.DTOs;
using MovieCatalog.Api.Mapping;
using MovieCatalog.Api.Models;

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

    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        var movies = await _context.Movies
            .Include(m => m.Director)
            .Include(m => m.Genres)
            .Include(m => m.Actors)
            .OrderBy(g => g.Id)
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