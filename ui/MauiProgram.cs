using Microsoft.Extensions.Logging;
using DotNet.Meteor.HotReload.Plugin;
using ui.Views.Chat;
using ui.ViewModels;
using ui.Views;
using ui.Services;
using ui.Views.Quiz;
using ui.Views.Home;

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

		// Register HttpClient
		builder.Services.AddSingleton<HttpClient>();

		// Register Services
		builder.Services.AddSingleton<IQuizService, QuizService>();
		builder.Services.AddSingleton<IAuthService, AuthService>();
		builder.Services.AddSingleton<IMatchService, MatchService>();

		// Register ViewModels
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<RegisterViewModel>();
		builder.Services.AddTransient<QuizViewModel>();
		builder.Services.AddTransient<QuizQuestionPage>();
		builder.Services.AddTransient<ChatPageViewModel>();
		builder.Services.AddTransient<HomeViewModel>();

		// Register Pages
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<RegisterPage>();
		builder.Services.AddTransient<ChatPageViewExample>();
		builder.Services.AddTransient<LoginErrorPage>();
		builder.Services.AddTransient<HomePage>();
		
		// Register Quiz Pages
		builder.Services.AddTransient<QuizStartPage>();
		builder.Services.AddTransient<QuizQuestionPage>();
		// builder.Services.AddTransient<QuizAgePreferencePage>();
		// builder.Services.AddTransient<QuizLookingForPage>();
		// builder.Services.AddTransient<QuizSportsImportancePage>();
		// builder.Services.AddTransient<QuizWeekendPreferencePage>();

		return builder.Build();
	}
}
