namespace NoteCodeApi.Dto
{
  public class LoginDto
  {
      public string UserName { get; set; }
      public string Password { get; set; }
      public bool Is_activate{get; set;}
  }
}