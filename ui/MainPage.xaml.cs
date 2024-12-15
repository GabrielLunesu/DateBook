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
		await Navigation.PushAsync(Handler.MauiContext.Services.GetService<RegisterPage>());
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Views.LoginPage());
	}

	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}


}

