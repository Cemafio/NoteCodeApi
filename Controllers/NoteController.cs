using Microsoft.AspNetCore.Mvc;
using NoteCodeApi.Data;
using NoteCodeApi.Dto;
using NoteCodeApi.Models;

namespace NoteCodeApi.Controllers
{
    [ApiController]
    [Route("api/Note")]

    public class NoteController:ControllerBase
    {
        private readonly NoteCodeDb db;
        public NoteController(NoteCodeDb Db)
        {
            db = Db;
        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto dto)
        {
            var note = new NoteUser
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = dto.UserId,
                Code = dto.Code
            };
            db.NoteUsers.Add(note);
            db.SaveChanges();

            return Ok(new
            {
                message = "Note créée",
                data = note
            });
        }

        [HttpPatch("Update/{id}")]
        public IActionResult Update(int id, [FromBody] UpdateNoteDto dto)
        {
            var note = db.NoteUsers.Find(id);

            if (note == null)
                return NotFound(new {message = "Note introuvable"});

            if (!string.IsNullOrEmpty(dto.Title))
                note.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Content))
                note.Content = dto.Content;

            if (!string.IsNullOrEmpty(dto.Code))
                note.Code = dto.Code;

            db.SaveChanges();

            return Ok(new
            {
                message = "Note mise à jour",
                data = note
            });
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var note = db.NoteUsers.Find(id);

            if (note == null)
                return NotFound("Note introuvable");

            db.NoteUsers.Remove(note);
            db.SaveChanges();

            return Ok(new
            {
                message = "Note supprimée"
            });
        }

        [HttpGet("ListAll")]
        public IActionResult ListAllNote()
        {
            var noteUsers = db.NoteUsers.ToList();

            return Ok(
                new{
                message = noteUsers.Any()? "Get noteUsers, success !!!": "Aucune note",
                list = noteUsers
            }
            );
        }

        [HttpGet("ListForUser/{userId}")]
        public IActionResult ListForOneUser(int userId)
        {
            var list = db.NoteUsers
                .Where(n => n.UserId == userId)
                .ToList();

            return Ok(list);
        }

        [HttpGet("oneNoteUser/{noteId}")]
        public IActionResult OneNoteUser(int noteId)
        {
            var list = db.NoteUsers
                .Where(n => n.Id == noteId)
                .ToList();

            return Ok(list);
        }
    }
}