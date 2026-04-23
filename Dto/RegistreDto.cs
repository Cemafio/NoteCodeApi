namespace NoteCodeApi.Dto
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ImageUrl { get; set; }
        public bool Is_activate { get; set; }
    }
}