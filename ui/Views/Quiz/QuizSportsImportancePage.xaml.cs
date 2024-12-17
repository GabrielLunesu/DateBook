namespace ui.Views.Quiz;
using ui.ViewModels;

public partial class QuizSportsImportancePage : ContentPage
{
    private readonly QuizViewModel _viewModel;

    public QuizSportsImportancePage(QuizViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
} 