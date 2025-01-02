using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ui.Models;
using ui.Services;

namespace ui.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly IMatchService _matchService;
    private readonly IAuthService _authService;
    private List<MatchDTO> _allMatches = new();
    private int _currentIndex = -1;  // Start at -1 so first increment goes to 0

    [ObservableProperty]
    private MatchDTO currentMatch;

    public HomeViewModel(IMatchService matchService, IAuthService authService)
    {
        _matchService = matchService;
        _authService = authService;
    }

    public async Task LoadMatches()
    {
        try 
        {
            var userId = await _authService.GetCurrentUserId();
            Console.WriteLine($"Current user ID: {userId}");
            
            _allMatches = await _matchService.GetPotentialMatches(userId);
            ShowNextMatch();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading matches: {ex}");
        }
    }

    [RelayCommand]
    private void SwipeLeft()
    {
        ShowNextMatch();
    }

    [RelayCommand]
    private async Task SwipeRight()
    {
        // TODO: Implement chat navigation
        await Shell.Current.DisplayAlert("Match!", $"You matched with {CurrentMatch.Name}!", "OK");
        ShowNextMatch();
    }

    private void ShowNextMatch()
    {
        if (_allMatches?.Count == 0) 
        {
            Console.WriteLine("No matches available");
            return;
        }

        _currentIndex++;
        if (_currentIndex >= _allMatches.Count)
        {
            _currentIndex = 0; // Loop back to start
        }

        CurrentMatch = _allMatches[_currentIndex];
        Console.WriteLine($"Showing match: {CurrentMatch?.Name}");
        Console.WriteLine($"Photo URL: {CurrentMatch?.MainPhotoUrl}");
        Console.WriteLine($"Raw photo data: {string.Join(", ", CurrentMatch?.Photos ?? new List<string>())}");
    }
} 