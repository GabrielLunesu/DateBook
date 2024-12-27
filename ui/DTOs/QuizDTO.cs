using System.Text.Json.Serialization;

namespace ui.DTOs;

public class QuizDTO
{
    public int Id { get; set; }
    public string Question { get; set; }
    public bool Status { get; set; }
}
