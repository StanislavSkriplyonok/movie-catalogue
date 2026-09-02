using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Api.Data;
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
    public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
    {
        return await _context.People.ToListAsync();
    }

    // GET: api/people/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
        var person = await _context.People.FindAsync();
        if (person == null) return NotFound();
        return person;
    }

    // POST: api/people
    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
    }

    // PUT: api/people/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, Person person)
    {
        if (id != person.Id) return BadRequest();

        _context.Entry(person).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/people/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return NotFound();

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}