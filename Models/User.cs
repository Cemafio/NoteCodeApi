using System.ComponentModel.DataAnnotations;

namespace NoteCodeApi.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool Is_active { get; set; }
        public string? ImageUrl { get; set;}

        public List<NoteUser> Notes { get; set; }
        public List<TaskUser> Tasks { get; set; }
    }
}