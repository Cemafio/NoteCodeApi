namespace NoteCodeApi.Dto
{
    public class AddNoteDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Code { get; set; }

    }
}