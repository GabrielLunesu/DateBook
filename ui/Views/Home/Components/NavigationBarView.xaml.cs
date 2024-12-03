namespace ui.Views.Home.Components;
using ui.Views.Profile;
using ui.Views.Likes;
using ui.Views.Chat;

public partial class NavigationBarView : ContentView
{
    public NavigationBarView()
    {
        InitializeComponent();
    }

    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }

    private async void OnLikesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LikesPage());
    }

    private async void OnChatClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChatPage());
    }
} 