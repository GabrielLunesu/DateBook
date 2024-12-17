namespace ui;
using System.Windows.Input;
using ui.Views.Chat;
using ui.ViewModels;
using ui.Views;
using Microsoft.Extensions.DependencyInjection;

public partial class MainPage : ContentPage
{
	public ICommand RegisterCommand { get; }

	public MainPage()
	{
		InitializeComponent();
		RegisterCommand = new Command(OnRegisterClicked);
		BindingContext = this;
	}

	private async void OnRegisterClicked()
	{
		var registerPage = Handler.MauiContext.Services.GetService<RegisterPage>();
		if (registerPage == null)
		{
			await DisplayAlert("Error", "Could not create registration page", "OK");
			return;
		}
		await Navigation.PushAsync(registerPage);
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		var loginPage=Handler.MauiContext.Services.GetService<LoginViewModel>();
		await Navigation.PushAsync(new LoginPage(loginPage));
	}

	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}


}

