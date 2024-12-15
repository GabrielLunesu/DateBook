using System.Windows.Input;
using ui.Models;
using Microsoft.Maui.Controls;

namespace ui.ViewModels
{
    public class RegisterViewModel : BindableObject
    {
        private string email;
        private string username;
        private string password;
        private string name;
        private DateTime birthDate = DateTime.Today;
        private string location;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get => location;
            set
            {
                location = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked()
        {
            var registration = new RegistrationModel
            {
                Email = Email,
                Username = Username,
                Password = Password,
                Name = Name,
                BirthDate = BirthDate,
                Location = Location
            };

            // TODO: Implement registration logic
            await Application.Current.MainPage.DisplayAlert("Registration", "Registration will be implemented soon!", "OK");
        }
    }
}