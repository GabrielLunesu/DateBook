namespace ui.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnRegisterSubmitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuizStartPage());
    }
} 