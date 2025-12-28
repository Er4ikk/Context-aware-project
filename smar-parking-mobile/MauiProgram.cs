using Shiny;
using Shiny.Hosting;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Maps;
using smar_parking_mobile.ViewModels;
using smar_parking_mobile.Views;

namespace smar_parking_mobile;

public static class MauiProgram
{

	public static MauiApp CreateMauiApp()
	{

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiCommunityToolkit()



			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiMaps()
			.UseShiny();

		//SERVICE REGISTRATION
		builder.Services.AddTransient<MapViewModel>();
		builder.Services.AddTransient<MapView>();
		builder.Services.AddTransient<MainPage>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
