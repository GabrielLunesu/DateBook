namespace ui.Views.Quiz;
using ui.ViewModels;

public partial class QuizAgePreferencePage : ContentPage
{
    private readonly QuizViewModel _viewModel;

    public QuizAgePreferencePage(QuizViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
} 