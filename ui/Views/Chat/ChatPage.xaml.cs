namespace ui.Views.Chat;



public partial class ChatPage : ContentPage
{
    public List<ChatModel> Chats { get; set; }

    public ChatPage()
    {
        InitializeComponent();
        LoadChats();
        BindingContext = this;
    }

    private void LoadChats()
    {
        Chats = new List<ChatModel>
        {
            new ChatModel 
            { 
                ImageSource = "headshot.jpeg", 
                Name = "Siliva",
                LastMessage = "I'm not a hoarder but I really...",
                Time = "11:30"
            },
            new ChatModel 
            { 
                ImageSource = "headshot.jpeg", 
                Name = "Lucy",
                LastMessage = "Is your body from Mcdonals",
                Time = "13:51"
            },
            new ChatModel 
            { 
                ImageSource = "headshot.jpeg", 
                Name = "Lucy",
                LastMessage = "Is your body from Mcdonals",
                Time = "13:51"
            },
            new ChatModel 
            { 
                ImageSource = "headshot.jpeg", 
                Name = "Lucy",
                LastMessage = "Is your body from Mcdonals",
                Time = "13:51"
            }
        };
    }

    private async void OnChatSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ChatModel selectedChat)
        {
            await Navigation.PushAsync(new ChatDetailPage(selectedChat));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
} 