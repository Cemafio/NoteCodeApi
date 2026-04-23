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

        [HttpPost("uploadProfile/{id}")]
        public async Task<IActionResult> UploadProfileImage(int id, IFormFile file)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return NotFound("User not found");

            if (file == null || file.Length == 0)
                return BadRequest("No file");

            // 🔒 sécurité
            if (!file.ContentType.StartsWith("image/"))
                return BadRequest("Invalid file type");
            
            // 🔥 crée le dossier si inexistant
            if (!Directory.Exists("wwwroot/uploads"))
            {
                Directory.CreateDirectory("wwwroot/uploads");
            }
            
            if (!string.IsNullOrEmpty(user.ImageUrl))
            {
                var oldPath = Path.Combine("wwwroot/uploads", Path.GetFileName(user.ImageUrl));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine("wwwroot/uploads", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 🔥 url accessible
            var imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

            user.ImageUrl = imageUrl;
            db.SaveChanges();

            return Ok(new
            {
                message = "Image uploaded",
                imageUrl = imageUrl
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