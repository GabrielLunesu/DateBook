namespace ui.Views;

using ui.Views.Quiz;
using ui.ViewModels;


public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
       
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