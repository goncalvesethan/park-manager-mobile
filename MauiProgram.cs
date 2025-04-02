using Microsoft.Extensions.Logging;
using ParkManagerMobile.Pages;

namespace ParkManagerMobile;

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
			});

        builder.Services.AddScoped<LoginPage>();
        builder.Services.AddScoped(sp =>
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5296/api/")
            };

            var token = Preferences.Get("jwt_token", string.Empty);
            if (!string.IsNullOrWhiteSpace(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return httpClient;
        });

        builder.Services.AddTransient<ListDevicesPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        return app;
    }
}
