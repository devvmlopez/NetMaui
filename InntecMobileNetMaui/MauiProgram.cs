using Microsoft.Extensions.Logging;
//using InntecMobileNetMaui.Platforms.Android.Services;
//using InntecMobileNetMaui.iOS.Services;
using InntecMobileNetMaui.Services;

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
//             .ConfigureMauiHandlers((handlers) =>
//              {
//#if ANDROID
//                handlers.AddHandler(typeof(IReCaptchaService), typeof(InntecMobileNetMaui.Droid.Services.ReCaptchaService));
//#elif IOS
//                handlers.AddHandler(typeof(IReCaptchaService), typeof(InntecMobileNetMaui.iOS.Services.ReCaptchaService));
//#endif
//              });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
