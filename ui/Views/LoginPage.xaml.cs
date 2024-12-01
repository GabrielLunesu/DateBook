namespace ui.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnLoginSubmitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuizStartPage());
    }
} 