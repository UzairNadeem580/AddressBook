// OwnersController.cs
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OwnersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
    {
        return await _context.Owners.ToListAsync();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<Owner>> GetOwner(string userId)
    {
        var owner = await _context.Owners
            .Where(o => o.UserId == userId)
            .FirstOrDefaultAsync();

        if (owner == null)
        {
            return NotFound();
        }

        return owner;
    }

    [HttpPost]
    public async Task<ActionResult<Owner>> CreateOwner(Owner owner)
    {
        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOwner), new { userId = owner.UserId }, owner);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateOwner(string userId, Owner owner)
    {
        if (userId != owner.UserId)
        {
            return BadRequest();
        }

        _context.Entry(owner).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OwnerExists(userId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteOwner(string userId)
    {
        var owner = await _context.Owners
            .Where(o => o.UserId == userId)
            .FirstOrDefaultAsync();

        if (owner == null)
        {
            return NotFound();
        }

        _context.Owners.Remove(owner);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OwnerExists(string userId)
    {
        return _context.Owners.Any(o => o.UserId == userId);


    }
}