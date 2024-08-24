using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
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
            RequestPermissionAsync(this);
            // Android.Gms.SafetyNet.SafetyNetClass.GetClient(Platform.CurrentActivity);
            //UserDialogs.Init(this);  // Para aplicar la autenticacion con huella 
            //CrossFingerprint.SetCurrentActivityResolver(() => this);
            //global::ZXing.Net.Mobile.Forms.Android.Platform.Init();  // Para aplicar el lector QR y de Barras
        }

        public void RequestPermissionAsync(Activity activity)
        {

            if (ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.Camera) != Permission.Granted &&
                ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.AccessFineLocation) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.Camera, Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation }, 1);
            }
            if (ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.WriteExternalStorage) != Permission.Granted ||
                ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, 1);
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            //ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (grantResults.Length > 0)
                /*if (requestCode == CameraPermissionsCode && grantResults[0] == Permission.Denied)
                {
                    Snackbar.Make(_layout, "Si tienes alguna tarjeta de Combustibles, necesitaras el acceso a la camara.", Snackbar.LengthIndefinite)
                        .SetAction("OK", v => RequestPermissions(CameraPermissions, CameraPermissionsCode))
                        .Show();
                    return;
                }*/


                //CameraPermissionGranted?.Invoke(this, EventArgs.Empty);

                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async override void OnBackPressed()
        {
            var navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;
            if (navigation is null || navigation.NavigationStack.Count > 1 || navigation.ModalStack.Count > 0)
            {
                base.OnBackPressed();
            }

        }
    }
}
