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

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Invalid data");

            var userExist = _context.Users
                .FirstOrDefault(u => u.Username == dto.Username || u.Email == dto.Email);

            if (userExist != null)
            {
                return BadRequest(new
                {
                    message = "Username or Email already exists"
                });
            }

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

            return Ok(new
            {
                message = "User created successfully",
                userId = user.Id,
            });
        }

        [HttpPost("sign_in")]
        public IActionResult SignIn([FromBody] LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == dto.UserName);

            if (user == null)
                return Unauthorized(new{ message = "Incorrect name !" });

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                return Unauthorized(new{ message = "Incorrect password !" });

            if (dto.Is_activate == false || dto.Is_activate == true)
            {
                user.Is_active = dto.Is_activate;
            }
            _context.SaveChanges();
            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                username = user.Username,
                userEmail = user.Email,
                userProfil = user.ImageUrl,
            });
        }
    }
}