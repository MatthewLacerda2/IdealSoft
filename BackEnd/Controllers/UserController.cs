using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using Newtonsoft.Json;

namespace BackEnd.Controllers;
[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase {

    private readonly BackendDbContext _context;

    public UserController(BackendDbContext context) {
        _context = context;
    }

    // GET: api/v1/users/1
    [HttpGet("{id}")]
    public IActionResult ReadClient(string id) {

        var user = _context.Users.Find(id);
        if(user==null){
            return NotFound();
        }

        var response = JsonConvert.SerializeObject(user);

        return Ok(response);
    }

    // GET: api/v1/users/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(string? name, string? surname, int offset = 0, int limit = 10) {

        if (limit < 1) {
            return BadRequest("Limit must be greater than 0.");
        }

        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(name)) {
            query = query.Where(u => u.name.Contains(name));
        }

        if (!string.IsNullOrEmpty(surname)) {
            query = query.Where(u => u.surname.Contains(surname));
        }

        var users = await query
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return Ok(JsonConvert.SerializeObject(users));
    }

    // POST: api/v1/users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user) {

        var existingUser = await _context.Users.FindAsync(user.Id);

        if (existingUser != null) {
            return BadRequest("Id already taken!");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var jsonUser = JsonConvert.SerializeObject(user);

        return CreatedAtAction(nameof(PostUser), jsonUser);
    }

    // PATCH: api/v1/users/1
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUser(User user) {

        var existingUser = await _context.Users.FindAsync(user.Id);

        if (existingUser == null) {
            return BadRequest("User Id not found!");
        }

        existingUser.name = user.name;
        existingUser.surname = user.surname;
        existingUser.telephone = user.telephone;

        await _context.SaveChangesAsync();

        var response = JsonConvert.SerializeObject(existingUser);

        return Ok(response);
    }

    // DELETE: api/v1/users/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) {

        var user = await _context.Users.FindAsync(id);
        if (user == null) {
            return BadRequest("User Id not found!");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}