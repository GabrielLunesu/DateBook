namespace ui.Views.Quiz;
using ui.ViewModels;

public partial class QuizStartPage : ContentPage
{
    private readonly QuizViewModel _viewModel;

    public QuizStartPage(QuizViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void OnStartQuizClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(Handler.MauiContext.Services.GetService<QuizQuestionPage>());
    }
} 