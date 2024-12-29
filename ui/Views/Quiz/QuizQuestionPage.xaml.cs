namespace ui.Views.Quiz;
using ui.ViewModels;

public partial class QuizQuestionPage : ContentPage
{
    private readonly QuizViewModel _viewModel;

    public QuizQuestionPage(QuizViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            await _viewModel.LoadQuestionsAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}