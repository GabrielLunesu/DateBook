namespace DatingApp.DTOs
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Question { get; set; }
        public bool Status { get; set; }
        public DateTime CompletedAt { get; set; }
        public ICollection<QuizResponseDTO> Responses { get; set; }
    }
} 