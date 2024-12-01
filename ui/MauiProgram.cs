using Microsoft.Extensions.Logging;
using DotNet.Meteor.HotReload.Plugin;


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

		builder.Services.AddTransient<Views.LoginPage>();
		builder.Services.AddTransient<Views.RegisterPage>();

		return builder.Build();
	}
}
