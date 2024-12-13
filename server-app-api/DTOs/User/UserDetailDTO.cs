namespace DatingApp.DTOs
{
    public class UserDetailDTO : UserDTO
    {
        public string UserTypeName { get; set; }
        public ProfileDTO Profile { get; set; }
        public QuizDTO Quiz { get; set; }
        // Note: We can add other related data as needed
        // public ICollection<MatchDTO> Matches { get; set; }
        // public ICollection<DateDTO> Dates { get; set; }
        // public ICollection<ReviewDTO> Reviews { get; set; }
    }
} 