using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ui.Services;
using ui.DTOs;
using ui.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;

namespace ui.ViewModels;

public class QuizViewModel : BaseViewModel
{
    private readonly IQuizService _quizService;
    private readonly IAuthService _authService;
    private int _currentIndex = 0;
    private QuizDTO _currentQuestion;
    private HashSet<int> _answeredQuestions;
    
    public ObservableCollection<QuizDTO> Questions { get; } = new();
    
    public IRelayCommand<bool> AnswerCommand { get; }

    public QuizDTO CurrentQuestion
    {
        get => _currentQuestion;
        set => SetProperty(ref _currentQuestion, value);
    }

    public QuizViewModel(IQuizService quizService, IAuthService authService)
    {
        _quizService = quizService;
        _authService = authService;
        _answeredQuestions = new HashSet<int>();
        AnswerCommand = new RelayCommand<bool>(async (response) => await HandleAnswerAsync(response));
    }

    private async Task HandleAnswerAsync(bool response)
    {
        try 
        {
            if (_currentIndex >= Questions.Count) return;

            var currentQuestion = Questions[_currentIndex];
            var userId = await _authService.GetCurrentUserId();

            var quizResponse = new QuizResponseDTO
            {
                QuizId = currentQuestion.Id,
                UserResponse = response,
                UserId = userId
            };

            var success = await _quizService.SubmitQuizResponseAsync(quizResponse);
            if (!success)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to submit answer", "OK");
                return;
            }

            _currentIndex++;

            if (_currentIndex >= Questions.Count)
            {
                // Quiz completed
                await Shell.Current.DisplayAlert("Complete", "You have completed the quiz!", "OK");
                await Shell.Current.GoToAsync("//HomePage");
                return;
            }

            // Move to next question
            CurrentQuestion = Questions[_currentIndex];
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    public async Task LoadQuestionsAsync()
    {
        try
        {
            IsBusy = true;
            
            // Get all questions first
            var allQuestions = await _quizService.GetAllQuestionsAsync();
            
            try
            {
                // Try to get user's existing responses
                var answeredQuestions = await _quizService.GetUserResponses();
                _answeredQuestions = new HashSet<int>(answeredQuestions.Select(r => r.QuizId));
                
                // Filter out already answered questions
                allQuestions = allQuestions.Where(q => !_answeredQuestions.Contains(q.Id)).ToList();
            }
            catch
            {
                // If getting user responses fails, continue with all questions
                _answeredQuestions.Clear();
            }

            Questions.Clear();
            foreach (var question in allQuestions)
            {
                Questions.Add(question);
            }

            if (Questions.Any())
            {
                CurrentQuestion = Questions[0];
            }
            else
            {
                await Shell.Current.DisplayAlert("Complete", "You have already completed all questions!", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
