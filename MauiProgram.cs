using cadflash.Services;
using cadflash.ViewModels;
using cadflash.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace cadflash
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
                    fonts.AddFont("OCRAEXT.ttf", "OCRA");
                });

            builder.Services.AddSingleton<LocalDbService>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<DifficultyView>();
            builder.Services.AddTransient<CRUDView>();
            builder.Services.AddTransient<CRUDViewModel>();
            builder.Services.AddTransient<DifficultyViewModel>();
            builder.Services.AddTransient<HomeViewModel>();


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
