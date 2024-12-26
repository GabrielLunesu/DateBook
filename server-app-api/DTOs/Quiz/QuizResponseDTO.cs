namespace DatingApp.DTOs
{
    public class QuizResponseDTO
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public bool UserResponse { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}