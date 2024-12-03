namespace ui.Views.Profile;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Handle logout
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnDeleteAccountClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Delete Account", 
            "Are you sure you want to delete your account? This action cannot be undone.", 
            "Yes", "No");

        if (answer)
        {
            // Handle account deletion
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
} 