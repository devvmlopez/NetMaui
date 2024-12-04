using InntecMobileNetMaui.Services.SQL;
using InntecMobileNetMaui.ViewModels.Login;
using InntecMobileNetMaui.Views.Login;
using Microsoft.Extensions.Logging;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Plugin.LocalNotification;
using ZXing.Net.Maui.Controls;
using CommunityToolkit.Maui;
using InntecMobileNetMaui.Models;

namespace InntecMobileNetMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
           

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterViewModels()
                .UseMauiCommunityToolkit()
                .UseLocalNotification()
                .UseBarcodeReader() // Iniciar el ZXing Maui 
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Montserrat.ttf", "FontMont");
                    fonts.AddFont("Montserrat-Bold.ttf", "FontMontB");
                    fonts.AddFont("fontello.ttf", "FontIco");
                }).ConfigureEssentials(essentials =>
                {
                    essentials.UseVersionTracking();
                });

            //builder.Services.AddTransient<Services.IReCaptchaService>();     
            return builder.Build();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();


        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<ViewModels.MainViewModel>();
            mauiAppBuilder.Services.AddSingleton<ViewModels.BaseViewModel>();
            mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
            mauiAppBuilder.Services.AddSingleton<UserModel>();
            mauiAppBuilder.Services.AddSingleton<SQLiteDataLogin>();
            mauiAppBuilder.Services.AddTransient<LoginPage>();
            mauiAppBuilder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);  //Para iOS
            //mauiAppBuilder.Services.AddSingleton<Services.IReCaptchaService>();
            //builder.Services.AddSingleton<IReCaptchaService, ReCaptchaService>();
            //mauiAppBuilder.Services.AddSingleton<IReCaptchaService>();

            return mauiAppBuilder;
        }

    }
}
