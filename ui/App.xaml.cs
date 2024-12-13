namespace ui;
using ui.Views.Chat;
using ui.ViewModels;

public partial class App : Application
{
	// public App()
	// {
	// 	InitializeComponent();
	// 	// MainPage = new AppShell();

	// 	MainPage = new ChatPageViewExample(ChatPageViewModel viewModel);

	// }

	public App(ChatPageViewExample chatPage)
    {
        InitializeComponent();
        MainPage = chatPage;
    }
}

