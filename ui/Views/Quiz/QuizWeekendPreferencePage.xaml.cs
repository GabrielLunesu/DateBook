namespace ui.Views.Quiz;

public partial class QuizWeekendPreferencePage : ContentPage
{
    public QuizWeekendPreferencePage()
    {
        InitializeComponent();
    }

    private async void OnOptionSelected(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            // Store selection if needed
            // Navigate to the main app after completing quiz
            // await Navigation.PushAsync(new HomePage());
        }
    }
} 