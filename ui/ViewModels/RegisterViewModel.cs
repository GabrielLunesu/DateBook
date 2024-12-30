using System.Windows.Input;
using ui.Models;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using ui.Services;
using ui.DTOs;
using CommunityToolkit.Mvvm.Input;
using ui.Helpers;
using System.Collections.ObjectModel;
namespace ui.ViewModels
{
    // we have to make a class partial
    // we have to inherit it from ObservableObject
    public partial class RegisterViewModel : ObservableObject
    {
       private readonly IAuthService _authService;

       [ObservableProperty]
       private ObservableCollection<GenderOption> genderOptions;

       [ObservableProperty]
       private GenderOption selectedGenderOption;

       [ObservableProperty]
       private RegisterDTO registerModel = new RegisterDTO();

       [ObservableProperty]
       private string selectedImageSource;

       [ObservableProperty]
       private List<string> uploadedPhotos = new();

       public IAsyncRelayCommand RegisterCommand {get; }

       public ICommand UploadPhotoCommand { get; }

       public RegisterViewModel(IAuthService authService)
       {
            _authService = authService;
            //async relay command is a command that can be executed asynchronously
            RegisterCommand = new AsyncRelayCommand(Register);
            UploadPhotoCommand = new AsyncRelayCommand(UploadPhoto);
            
            // Initialize gender options with display text
            GenderOptions = new ObservableCollection<GenderOption>
            {
                new GenderOption { Value = Gender.Male, DisplayText = "Man" },
                new GenderOption { Value = Gender.Female, DisplayText = "Vrouw" }
            };
       }

       partial void OnSelectedGenderOptionChanged(GenderOption value)
       {
           if (value != null)
           {
               RegisterModel.Gender = value.Value;
           }
       }

       private async Task UploadPhoto()
       {
           try
           {
               var result = await FilePicker.PickAsync(new PickOptions
               {
                   FileTypes = FilePickerFileType.Images,
                   PickerTitle = "Pick a profile photo"
               });

               if (result != null)
               {
                   // Convert to base64 string
                   var stream = await result.OpenReadAsync();
                   var bytes = new byte[stream.Length];
                   await stream.ReadAsync(bytes, 0, (int)stream.Length);
                   var base64String = Convert.ToBase64String(bytes);

                   // Add to photos list
                   UploadedPhotos.Add(base64String);
                   
                   // Update the RegisterModel
                   RegisterModel.Photos = UploadedPhotos.ToArray();

                   // Update UI preview
                   SelectedImageSource = result.FullPath;
               }
           }
           catch (Exception ex)
           {
               await Shell.Current.DisplayAlert("Error", "Failed to upload photo: " + ex.Message, "OK");
           }
       }

       private async Task Register()
       {
            // Ensure photos are included in registration
            RegisterModel.Photos = UploadedPhotos.ToArray();
            
            var response = await _authService.Register(RegisterModel);
            if(response != null)
            {
               await Shell.Current.GoToAsync("//QuizStartPage");
            }
       }
    }
}