using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Api.Data;
using MovieCatalog.Api.DTOs;
using MovieCatalog.Api.Mapping;
using MovieCatalog.Api.Models;

namespace MovieCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly AppDbContext _context;

    public PeopleController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/people
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPeople()
    {
        var people = await _context.People
            .OrderBy(g => g.Id)
            .ToListAsync();
        return people.Select(p => p.ToDto()).ToList();
    }

    // GET: api/people/1
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPerson(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return NotFound();
        return person.ToDto();
    }

    // POST: api/people
    [HttpPost]
    public async Task<ActionResult<PersonDto>> CreatePerson(CreatePersonDto dto)
    {
        var person = dto.ToEntity();

        _context.People.Add(person);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person.ToDto());
    }

    // PUT: api/people/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, UpdatePersonDto dto)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return NotFound();

        person.Name = dto.Name;
        person.Surname = dto.Surname;
        person.BirthYear = dto.BirthYear;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/people/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return NotFound();

        var isDirector = await _context.Movies.AnyAsync(m => m.DirectorId == id);
        if (isDirector)
        {
            return Conflict(new
            {
                status = 409,
                message = "Cannot delete this person because they are a director of one or more movies."
            });
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}