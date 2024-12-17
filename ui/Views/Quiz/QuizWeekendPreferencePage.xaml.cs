namespace ui.Views.Quiz;
using ui.Views.Home;


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
            // For now, just go back to the home page after completing the quiz
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}