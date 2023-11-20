using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;

namespace BackEnd.Controllers;
[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase {

    private readonly BackendDbContext _context;

    public UserController(BackendDbContext context) {
        _context = context;
    }

    // GET: api/v1/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
        return await _context.Users.ToListAsync();
    }

    // GET: api/v1/users/1
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id) {
        var user = await _context.Users.FindAsync(id);

        if (user == null) {
            return NotFound();
        }

        return user;
    }

    // POST: api/v1/users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user) {

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    // PATCH: api/v1/users/1
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUser(User user) {

        var existingUser = await _context.Users.FindAsync(user.Id);

        if (existingUser == null) {
            return NotFound();
        }

        existingUser.name = user.name;
        existingUser.surname = user.surname;
        existingUser.telephone = user.telephone;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/v1/users/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) {

        var user = await _context.Users.FindAsync(id);
        if (user == null) {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}