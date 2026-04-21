using Microsoft.AspNetCore.Mvc;
using NoteCodeApi.Data;
using NoteCodeApi.Dto;
using NoteCodeApi.Models;

namespace NoteCodeApi.Controllers
{
    [ApiController]
    [Route("api/Task")]
    public class TaskUserController : ControllerBase
    {
        private readonly NoteCodeDb _db;
        public TaskUserController(NoteCodeDb Db)
        {
            _db = Db;
        }

        [HttpPost("add")]
        public IActionResult AddTask([FromBody] TaskUserDto dto)
        {
            var task = new TaskUser
            {
                UserId = dto.UserId,
                TaskName = dto.TaskName,
                IS_completed = dto.IS_completed
            };
            _db.TaskUsers.Add(task);
            _db.SaveChanges();

            return Ok(new
            {
                message = "Task created",
                data = task
            });
        }

        [HttpPatch("Update/{id}")]
        public IActionResult Update(int id, [FromBody] TaskUserDto dto)
        {
            var task = _db.TaskUsers.Find(id);

            if (task == null)
                return NotFound(new {message = "Note introuvable"});

            if (!string.IsNullOrEmpty(dto.TaskName))
                task.TaskName = dto.TaskName;

            if (dto.IS_completed == false || dto.IS_completed == true)
                task.IS_completed = dto.IS_completed;

            _db.SaveChanges();

            return Ok(new
            {
                message = "Task updated",
                data = task
            });
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var task = _db.TaskUsers.Find(id);

            if (task == null)
                return NotFound("Task not found !");

            _db.TaskUsers.Remove(task);
            _db.SaveChanges();

            return Ok(new
            {
                message = "Task deleted"
            });
        }

        // [HttpGet("ListAll")]
        // public IActionResult ListAllNote()
        // {
        //     var noteUsers = db.NoteUsers.ToList();

        //     return Ok(
        //         new{
        //         message = noteUsers.Any()? "Get noteUsers, success !!!": "Aucune note",
        //         list = noteUsers
        //     }
        //     );
        // }

        [HttpGet("ListAllTask/{userId}")]
        public IActionResult ListAllTask(int userId)
        {
            var list = _db.TaskUsers
                .Where(n => n.UserId == userId)
                .ToList();

            return Ok(list);
        }

        [HttpGet("oneTaskId/{taskId}")]
        public IActionResult OneTaskUser(int taskId)
        {
            var list = _db.TaskUsers
                .Where(n => n.Id == taskId)
                .ToList();

            return Ok(list);
        }
    }
}