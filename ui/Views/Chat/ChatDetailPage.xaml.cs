namespace ui.Views.Chat;

public class MessageModel
{
    public string Content { get; set; }
    public bool IsFromMe { get; set; }
    public string TimeStamp { get; set; }
}

public partial class ChatDetailPage : ContentPage
{
    private readonly ChatModel _chat;
    public string ImageSource => _chat.ImageSource;
    public string Name => _chat.Name;
    public List<MessageModel> Messages { get; set; }
    public string CurrentMessage { get; set; }

    public ChatDetailPage(ChatModel chat)
    {
        InitializeComponent();
        _chat = chat;
        LoadMessages();
        BindingContext = this;
    }

    private void LoadMessages()
    {
        Messages = new List<MessageModel>
        {
            new MessageModel 
            { 
                Content = "Can I follow you? Cause my mom told me to follow my dreams...",
                IsFromMe = true,
                TimeStamp = "Today 12:05"
            },
            new MessageModel 
            { 
                Content = "I'm not a hoarder but I really\nLoream ipls",
                IsFromMe = false,
                TimeStamp = ""
            }
        };
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnSendClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(CurrentMessage))
        {
            Messages.Add(new MessageModel 
            { 
                Content = CurrentMessage,
                IsFromMe = true,
                TimeStamp = DateTime.Now.ToString("HH:mm")
            });
            CurrentMessage = string.Empty;
            OnPropertyChanged(nameof(Messages));
            OnPropertyChanged(nameof(CurrentMessage));
        }
    }
} 