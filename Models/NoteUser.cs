using NoteCodeApi.Interfaces;

namespace NoteCodeApi.Models
{
    public class NoteUser : ITrackable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }

        // public Users User { get; set; }
    }
    
}