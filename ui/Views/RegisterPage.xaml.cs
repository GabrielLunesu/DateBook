namespace ui.Views;

using ui.Views.Quiz;
using ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ui.Models;
using System.Collections.ObjectModel;
public partial class RegisterPage : ContentPage
{
    private readonly RegisterViewModel _viewModel;

    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public async void OnGenderSelected(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        if (picker?.SelectedItem != null)
        {
            var selectedGender = (Gender)picker.SelectedItem;
            _viewModel.RegisterModel.Gender = selectedGender;
        }
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