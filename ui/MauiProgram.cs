using Microsoft.Extensions.Logging;
using DotNet.Meteor.HotReload.Plugin;
using ui.Views.Chat;
using ui.ViewModels;
using ui.Views;
using ui.Services;

namespace ui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})

#if DEBUG
		.EnableHotReload();
		builder.Logging.AddDebug();
#endif
		// transient means that the viewmodel is created for each instance of the view
		builder.Services.AddSingleton<IAuthService, AuthService>();
		builder.Services.AddSingleton<ChatPageViewExample>();
		builder.Services.AddSingleton<ChatPageViewModel>();
		builder.Services.AddTransient<Views.LoginPage>();
		builder.Services.AddTransient<Views.RegisterPage>();
		builder.Services.AddTransient<Views.Quiz.QuizStartPage>();
		builder.Services.AddTransient<Views.Quiz.QuizAgePreferencePage>();
		builder.Services.AddTransient<Views.Quiz.QuizLookingForPage>();
		builder.Services.AddTransient<Views.Quiz.QuizSportsImportancePage>();
		builder.Services.AddTransient<Views.Quiz.QuizWeekendPreferencePage>();
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<RegisterViewModel>();
		builder.Services.AddTransient<RegisterPage>();

		return builder.Build();
	}
}
