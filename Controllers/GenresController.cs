using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Api.Data;
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

    //Get list of genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        return await _context.Genres.ToListAsync();
    }

    //Get genre by id or message "Not Found" if null
    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if(genre == null) return NotFound();
        return genre;
    }

    //Create genre
    [HttpPost]
    public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
    }

    //Update genre information by id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(int id, Genre genre)
    {
        if(id != genre.Id) return BadRequest();

        _context.Entry(genre).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //Delete genre by id
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