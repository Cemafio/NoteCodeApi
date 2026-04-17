namespace NoteCodeApi.Dto
{
  public class TaskUserDto
  {
      public int UserId { get; set; }
      public string TaskName { get; set; }
      public bool IS_completed { get; set; }
  }
}