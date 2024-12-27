using CommunityToolkit.Mvvm.ComponentModel;
using ui.Services;
using ui.DTOs;
using CommunityToolkit.Mvvm.Input;
using ui.Views;
using Microsoft.Extensions.DependencyInjection;
using ui.Views.Quiz;
using ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ui.Models;
using System.Collections.ObjectModel;



namespace ui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        private readonly IAuthService _authService;

        [ObservableProperty]
        private LoginDTO loginModel = new LoginDTO();


        public IAsyncRelayCommand LoginCommand {get;}

       
       public LoginViewModel(IAuthService authService)
       {
            _authService = authService;
            //async relay command is a command that can be executed asynchronously
            LoginCommand = new AsyncRelayCommand(Login);
            
          
       }

       private async Task Login()
       {
            var response = await _authService.Login(LoginModel);
            if(response != null && !response.Contains("error login"))
            {
               await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await Shell.Current.GoToAsync("//LoginErrorPage");
            }
       }

    }
}