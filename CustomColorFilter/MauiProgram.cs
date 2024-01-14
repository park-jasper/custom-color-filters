using CommunityToolkit.Maui;
using CustomColorFilter.Contracts;
using Microsoft.Extensions.Logging;

namespace CustomColorFilter
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if WINDOWS
            builder.Services.AddSingleton<IColorFilter, Platforms.Windows.MagnificationColorFilter>();
            builder.Services.AddSingleton<IFileService, Platforms.Windows.WindowsFileService>();
#endif

            builder.Services
                .AddSingleton<MainPage>()
                .AddSingleton<MainPageViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            app.Services.GetRequiredService<IColorFilter>().Initialize();

            return app;
        }
    }
}
