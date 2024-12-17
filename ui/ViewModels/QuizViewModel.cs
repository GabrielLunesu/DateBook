using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ui.Services;
using ui.DTOs;
using ui.Helpers;

namespace ui.ViewModels;

public partial class QuizViewModel : ObservableObject
{
    private readonly IQuizService _quizService;

    [ObservableProperty]
    private QuizDTO quizData = new();

    [ObservableProperty]
    private string minAgeText;

    [ObservableProperty]
    private string maxAgeText;

    public IAsyncRelayCommand SubmitQuizCommand { get; }

    public QuizViewModel(IQuizService quizService)
    {
        _quizService = quizService;
        SubmitQuizCommand = new AsyncRelayCommand(SubmitQuiz);
        InitializeQuizData();
    }

    [RelayCommand]
    public async Task SetAgePreference()
    {
        System.Diagnostics.Debug.WriteLine($"MinAgeText: {MinAgeText}, MaxAgeText: {MaxAgeText}");

        if (string.IsNullOrEmpty(MinAgeText) || string.IsNullOrEmpty(MaxAgeText))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter both ages", "OK");
            return;
        }

        if (int.TryParse(MinAgeText, out int minAge) && 
            int.TryParse(MaxAgeText, out int maxAge))
        {
            if (minAge <= 0 || maxAge <= 0 || minAge >= maxAge)
            {
                await Shell.Current.DisplayAlert("Error", "Please enter valid age range", "OK");
                return;
            }

            QuizData.AgePreference = $"{minAge},{maxAge}";
            System.Diagnostics.Debug.WriteLine($"Setting age preference to: {QuizData.AgePreference}");
            System.Diagnostics.Debug.WriteLine($"Full QuizData after setting age: {System.Text.Json.JsonSerializer.Serialize(QuizData)}");
            await Shell.Current.GoToAsync("//QuizLookingForPage");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Please enter valid ages", "OK");
        }
    }

    [RelayCommand]
    public async Task SetLookingFor(string preference)
    {
        if (!string.IsNullOrEmpty(preference))
        {
            QuizData.RelationshipType = preference;
            System.Diagnostics.Debug.WriteLine($"Full QuizData after setting relationship: {System.Text.Json.JsonSerializer.Serialize(QuizData)}");
            await Shell.Current.GoToAsync("//QuizSportsImportancePage");
        }
    }

    [RelayCommand]
    public async Task SetSportsImportance(string rating)
    {
        if (int.TryParse(rating, out int ratingValue))
        {
            QuizData.SportImportance = ratingValue;
            await Shell.Current.GoToAsync("//QuizWeekendPreferencePage");
        }
    }

    [RelayCommand]
    public async Task SetWeekendPreference(string preference)
    {
        if (!string.IsNullOrEmpty(preference))
        {
            QuizData.WeekendActivity = preference;
            await SubmitQuiz();
        }
    }

    private async Task SubmitQuiz()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"Starting quiz submission. Full QuizData: {System.Text.Json.JsonSerializer.Serialize(QuizData)}");

            // Validate required fields
            if (QuizData.UserId == 0)
            {
                await Shell.Current.DisplayAlert("Error", "User not authenticated", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
                return;
            }

            if (string.IsNullOrEmpty(QuizData.AgePreference))
            {
                System.Diagnostics.Debug.WriteLine("AgePreference is empty or null!");
                await Shell.Current.DisplayAlert("Error", "Age preference is required", "OK");
                return;
            }

            if (string.IsNullOrEmpty(QuizData.RelationshipType))
            {
                await Shell.Current.DisplayAlert("Error", "Relationship type is required", "OK");
                return;
            }

            // Set completion time just before submitting
            QuizData.CompletedAt = DateTime.UtcNow;

            // Debug output
            System.Diagnostics.Debug.WriteLine($"Submitting Quiz: {System.Text.Json.JsonSerializer.Serialize(QuizData)}");

            var result = await _quizService.SubmitQuiz(QuizData);
            if (result)
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in SubmitQuiz: {ex}");
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void InitializeQuizData()
    {
        var userId = await TokenManager.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            await Shell.Current.DisplayAlert("Error", "User not authenticated", "OK");
            await Shell.Current.GoToAsync("//LoginPage");
            return;
        }

        if (int.TryParse(userId, out int userIdInt))
        {
            QuizData.UserId = userIdInt;
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Invalid user ID", "OK");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
