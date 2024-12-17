namespace ui.Views.Quiz;
using ui.ViewModels;

public partial class QuizWeekendPreferencePage : ContentPage
{
    private readonly QuizViewModel _viewModel;

    public QuizWeekendPreferencePage(QuizViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}