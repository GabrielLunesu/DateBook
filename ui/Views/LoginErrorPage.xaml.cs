using System.Diagnostics;

namespace ui.Views;

public partial class LoginErrorPage : ContentPage
{
	public LoginErrorPage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(System.Object sender, System.EventArgs e) 
	{
      Shell.Current.GoToAsync("//RegisterPage");

	 }

	private async void BackToLogin_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//LoginPage");
	}
}