using System.Windows.Input;
using ui.Models;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using ui.Services;
using ui.DTOs;
using CommunityToolkit.Mvvm.Input;
using ui.Helpers;
namespace ui.ViewModels
{
    // we have to make a class partial
    // we have to inherit it from ObservableObject
    public partial class RegisterViewModel : ObservableObject
    {
       private readonly IAuthService _authService;

       public RegisterViewModel(IAuthService authService)
       {
            _authService = authService;
            //async relay command is a command that can be executed asynchronously
            RegisterCommand = new AsyncRelayCommand(Register);
       }

       [ObservableProperty]
       private RegisterDTO registerModel = new RegisterDTO();

       public IAsyncRelayCommand RegisterCommand {get; }

       private async Task Register()
       {
            var response = await _authService.Register(registerModel);
            if(response != null)
            {
               await Shell.Current.GoToAsync("//QuizStartPage");
            }
       }
    }
}