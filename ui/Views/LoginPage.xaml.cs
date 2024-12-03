namespace ui.Views;

using ui.Views.Quiz;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
       
    }

    private async void OnLoginSubmitClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//QuizStartPage");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
} 