namespace ui.Views.Quiz;

public partial class QuizAgePreferencePage : ContentPage
{
    public QuizAgePreferencePage()
    {
        InitializeComponent();
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        // Change from Navigation.PushAsync to Shell navigation
        await Shell.Current.GoToAsync("//QuizLookingForPage");
    }


} 