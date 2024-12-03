namespace ui;
using System.Windows.Input;

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
		await Navigation.PushAsync(new Views.RegisterPage());
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

