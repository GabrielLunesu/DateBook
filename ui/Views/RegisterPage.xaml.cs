namespace ui.Views;

using ui.Views.Quiz;
using ui.ViewModels;

public partial class RegisterPage : ContentPage
{
    private readonly RegisterViewModel _viewModel;
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
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