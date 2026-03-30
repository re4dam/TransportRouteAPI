using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportRoute.Core.Data;
using TransportRoute.Core.Models;
using TransportRoute.Security.Interfaces;
using TransportRouteApi.DTOs; // Importing your new DTO namespace

namespace TransportRoute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // THE SHIELD: Locks down the entire controller to Admins only
    [Authorize(Roles = "Admin")] 
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<PaginatedResponseDto<UserResponseDto>>> GetUsers(
            [FromQuery] string? keyword, 
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 50)
        {
            // Prevent abuse by capping the max rows returned
            if (pageSize > 200) pageSize = 200;

            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(u => u.Username.Contains(keyword) || u.Role.Contains(keyword));
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserResponseDto 
                {
                    Id = u.Id,             
                    Username = u.Username, 
                    Role = u.Role          
                })
                .ToListAsync();

            return Ok(new PaginatedResponseDto<UserResponseDto>
            {
                Items = users,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }

        // POST: api/Users
        [HttpPost]
        [ValidateAntiForgeryToken] // <-- Add this to enforce the shield
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] CreateUserDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username)) //[cite: 4]
            {
                return BadRequest("Username is already taken.");
            }

            var newUser = new User
            {
                Username = request.Username, //[cite: 4]
                PasswordHash = _passwordHasher.Hash(request.Password), //[cite: 4]
                // Fallback in case the request payload didn't explicitly use the DTO default
                Role = string.IsNullOrWhiteSpace(request.Role) ? "User" : request.Role //[cite: 4]
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Return the created resource using the response DTO
            var responseDto = new UserResponseDto
            {
                Id = newUser.Id,             //[cite: 5]
                Username = newUser.Username, //[cite: 5]
                Role = newUser.Role          //[cite: 5]
            };

            return CreatedAtAction(nameof(GetUsers), new { id = newUser.Id }, responseDto);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken] // <-- Add this to enforce the shield
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            if (user.Username != request.Username && await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest("Username is already taken.");
            }

            user.Username = request.Username;
            user.Role = request.Role;

            // Only update the password if the Admin actually provided a new one
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.PasswordHash = _passwordHasher.Hash(request.Password);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken] // <-- Add this to enforce the shield
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // Custom DTO for updates since updating allows an optional password change
    public class UpdateUserDto
    {
        public required string Username { get; set; }
        public required string Role { get; set; }
        public string? Password { get; set; } 
    }
}