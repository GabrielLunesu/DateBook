namespace ui.Views.Quiz;

public partial class QuizLookingForPage : ContentPage
{
    public QuizLookingForPage()
    {
        InitializeComponent();
    }

    private async void OnOptionSelected(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            // Store selection if needed
            await Navigation.PushAsync(new QuizSportsImportancePage());
        }
    }
} 