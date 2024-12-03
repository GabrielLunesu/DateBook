namespace ui.Views.Home.Components;
using ui.Views.Profile;

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
} 