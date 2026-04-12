namespace NoteCodeApi.Models
{
    public class NoteUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public Users User { get; set; }
    }
}