namespace ui.Views.Quiz;

public partial class QuizStartPage : ContentPage
{
    public QuizStartPage()
    {
        InitializeComponent();
    }

    private async void OnStartQuizClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Quiz.QuizAgePreferencePage());
    }
} 