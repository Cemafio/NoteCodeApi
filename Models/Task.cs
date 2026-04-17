using System.ComponentModel.DataAnnotations;
namespace NoteCodeApi.Models
{
    public class TaskUser
    {
        public int Id { get; set; }

        public int UserId {get; set;}
        public string TaskName {get; set;}
        public bool IS_completed {get; set;}
    }
}