namespace ui.Views.Quiz;
using ui.ViewModels;

public partial class QuizLookingForPage : ContentPage
{
    private readonly QuizViewModel _viewModel;

    public QuizLookingForPage(QuizViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
} 