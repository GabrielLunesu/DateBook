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
            // For now, just go back to the main page after completing the quiz
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}