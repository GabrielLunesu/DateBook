namespace ui.Views;

using ui.Views.Quiz;
using ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;

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
        // Get QuizStartPage from DI container
        var quizStartPage = Handler.MauiContext.Services.GetService<QuizStartPage>();
        if (quizStartPage == null)
        {
            await DisplayAlert("Error", "Could not create quiz page", "OK");
            return;
        }
        await Navigation.PushAsync(quizStartPage);
    }
} 