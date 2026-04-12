using Microsoft.AspNetCore.Mvc;
using NoteCodeApi.Data;
using NoteCodeApi.Models;
using BCrypt.Net;
using NoteCodeApi.Dto;

namespace NoteCodeApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly NoteCodeDb _context;
        public AuthController(NoteCodeDb context)
        {
            _context = context;
        }

        [HttpPost("sing_up")]
        public IActionResult Register(RegisterDto dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new Users
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Is_active = dto.Is_activate
            };


            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User created successfully" });
        }

        [HttpPost("sing_in")]
        public IActionResult SingIn(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == dto.UserName);

            if (user == null)
                return Unauthorized();

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                return Unauthorized();
            
            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                username = user.Username
            });
        }
    }
}