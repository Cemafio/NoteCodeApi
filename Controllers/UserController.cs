using Microsoft.AspNetCore.Mvc;
using NoteCodeApi.Data;
using NoteCodeApi.Dto;
using NoteCodeApi.Models;

namespace NoteCodeApi.Controllers
{
    [ApiController]
    [Route("api/User")]

    public class UserController:ControllerBase
    {
        private readonly NoteCodeDb db;
        public UserController(NoteCodeDb Db)
        {
            db = Db;
        }

        [HttpPatch("Update/{id}")]
        public IActionResult Update(int id, [FromBody] RegisterDto dto)
        {
            var user = db.Users.Find(id);

            if (user == null)
                return NotFound("User introuvable");
                
            if (!string.IsNullOrEmpty(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            db.SaveChanges();

            return Ok(new
            {
                message = "User updated",
                data = user
            });
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var user = db.Users.Find(id);

            if (user == null)
                return NotFound("Note introuvable");

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(new
            {
                message = $"User {user.Username} supprimée"
            });
        }

        [HttpGet("ListAll")]
        public IActionResult ListAllNote()
        {
            var Users = db.Users.ToList();

            return Ok(
                new{
                message = Users.Any()? "Get User, success !!!": "User empty",
                list = Users
            }
            );
        }

        [HttpGet("ListForUser/{userId}")]
        public IActionResult listForOneUser(int userId)
        {
            var list = db.Users
                .Where(n => n.Id == userId)
                .ToList();

            return Ok(list);
        }
    }
}