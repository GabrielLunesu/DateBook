namespace ui.Views.Quiz;

public partial class QuizSportsImportancePage : ContentPage
{
    public QuizSportsImportancePage()
    {
        InitializeComponent();
    }

    private async void OnRatingSelected(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            // Store rating if needed
            await Navigation.PushAsync(new QuizWeekendPreferencePage());
        }
    }
} 