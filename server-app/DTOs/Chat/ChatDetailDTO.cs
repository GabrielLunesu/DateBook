namespace DatingApp.DTOs
{
    public class ChatDetailDTO : ChatDTO
    {
        public ICollection<MessageDTO> Messages { get; set; }
    }
} 