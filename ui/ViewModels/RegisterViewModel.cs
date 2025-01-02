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

        public IAsyncRelayCommand RegisterCommand { get; }

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
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Pick a profile photo"
                });

                if (result != null)
                {
                    // Store the filename and path
                    UploadedPhotos.Add(result.FileName);
                    RegisterModel.Photos = UploadedPhotos.ToArray();
                    SelectedImageSource = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to select photo: {ex.Message}", "OK");
            }
        }

        private async Task Register()
        {
            var streamsToDispose = new List<Stream>();
            
            try
            {
                var content = new MultipartFormDataContent();

                // Add registration data
                content.Add(new StringContent(RegisterModel.Email), "Email");
                content.Add(new StringContent(RegisterModel.Username), "Username");
                content.Add(new StringContent(RegisterModel.Password), "Password");
                content.Add(new StringContent(RegisterModel.Name), "Name");
                content.Add(new StringContent(RegisterModel.Gender.ToString()), "Gender");
                content.Add(new StringContent(RegisterModel.BirthDate.ToString("yyyy-MM-dd")), "BirthDate");
                content.Add(new StringContent(RegisterModel.Location ?? ""), "Location");
                content.Add(new StringContent(RegisterModel.IsActive.ToString()), "IsActive");
                content.Add(new StringContent(RegisterModel.CreatedAt?.ToString("yyyy-MM-dd") ?? ""), "CreatedAt");

                // Add photo file if selected
                if (!string.IsNullOrEmpty(SelectedImageSource))
                {
                    var fileBytes = await File.ReadAllBytesAsync(SelectedImageSource);
                    var memoryStream = new MemoryStream(fileBytes);
                    streamsToDispose.Add(memoryStream);

                    var streamContent = new StreamContent(memoryStream);
                    var extension = Path.GetExtension(SelectedImageSource).ToLowerInvariant();
                    var contentType = extension switch
                    {
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        ".bmp" => "image/bmp",
                        ".webp" => "image/webp",
                        _ => "application/octet-stream"
                    };
                    
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                    // Change the name to match what the backend expects
                    content.Add(streamContent, "Photos", Path.GetFileName(SelectedImageSource));
                }

                var response = await _authService.Register(content);
                if (response != null)
                {
                    await Shell.Current.GoToAsync("//QuizStartPage");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
            }
            finally
            {
                foreach (var stream in streamsToDispose)
                {
                    stream.Dispose();
                }
            }
        }
    }
}