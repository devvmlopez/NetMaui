using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Mopups.Services;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
using static Microsoft.Maui.LifecycleEvents.AndroidLifecycle;

namespace InntecMobileNetMaui
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);
            //OnBackPressed();
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
           // Android.Gms.SafetyNet.SafetyNetClass.GetClient(Platform.CurrentActivity);
            //UserDialogs.Init(this);  // Para aplicar la autenticacion con huella 
            //CrossFingerprint.SetCurrentActivityResolver(() => this);
            //global::ZXing.Net.Mobile.Forms.Android.Platform.Init();  // Para aplicar el lector QR y de Barras
        }

        
        public async override void OnBackPressed()
        {
            var navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;
            if (navigation is null || navigation.NavigationStack.Count > 1 || navigation.ModalStack.Count > 0)
            {
                base.OnBackPressed();
            }
            //else
            //{
            //    const int delay = 2000;
            //    if (backPressed + delay > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
            //    {
            //        FinishAndRemoveTask();
            //        Process.KillProcess(Process.MyPid());
            //    }
            //    else
            //    {
            //        Toast.MakeText(ApplicationContext, "Close", ToastLength.Long)?.Show();
            //        backPressed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            //    }
            //}

        }
    }
}
