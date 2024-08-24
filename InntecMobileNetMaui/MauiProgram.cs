using Microsoft.Extensions.Logging;
//using InntecMobileNetMaui.Droid.Services;
//using InntecMobileNetMaui.iOS.Services;
using InntecMobileNetMaui.Views.Login;

namespace InntecMobileNetMaui
{
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
                    fonts.AddFont("Montserrat.ttf", "FontMont");
                    fonts.AddFont("Montserrat-Bold.ttf", "FontMontB");
                });
                   // builder.Services.AddSingleton<LoginPage>();builder.Services.AddSingleton<ReCaptchaService>();
                    return builder.Build();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
