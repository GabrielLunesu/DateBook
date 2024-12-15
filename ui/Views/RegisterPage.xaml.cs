namespace ui.Views;

using ui.Views.Quiz;
using ui.ViewModels;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
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