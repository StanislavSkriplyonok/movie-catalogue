using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Api.Data;
using MovieCatalog.Api.DTOs;
using MovieCatalog.Api.Mapping;
using MovieCatalog.Api.Models;

namespace MovieCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly AppDbContext _context;

    public GenresController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
    {
        var genres = await _context.Genres
            .OrderBy(g => g.Id)
            .ToListAsync();
        return genres.Select(g => g.ToDto()).ToList();
    }

    // GET: api/genres/1
    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return NotFound();
        return genre.ToDto();
    }

    // POST: api/genres
    [HttpPost]
    public async Task<ActionResult<GenreDto>> CreateGenre(CreateGenreDto dto)
    {
        var genre = dto.ToEntity();

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre.ToDto());
    }

    // PUT: api/genres/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(int id, UpdateGenreDto dto)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return NotFound();

        genre.Name = dto.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/genres/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if(genre == null) return NotFound();

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}