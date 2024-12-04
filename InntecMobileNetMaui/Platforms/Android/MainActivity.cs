using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Google.Android.Material.Snackbar;
using Mopups.Services;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
using static Microsoft.Maui.LifecycleEvents.AndroidLifecycle;

namespace InntecMobileNetMaui
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density) ]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            OnBackPressed();
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            RequestPermissionAsync(this);
            //Android.Gms.SafetyNet.SafetyNetClass.GetClient(Platform.CurrentActivity);  SafetyNet Recapchat
            CrossFingerprint.SetCurrentActivityResolver(() => this);
        }

        public void RequestPermissionAsync(Activity activity)
        {

            if (ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.Camera) != Permission.Granted &&
                ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.AccessFineLocation) != Permission.Granted && 
                ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.PostNotifications) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.Camera, Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation, Manifest.Permission.PostNotifications, }, 1);
            }
            if (ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.WriteExternalStorage) != Permission.Granted ||
                ContextCompat.CheckSelfPermission(activity.ApplicationContext, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, 1);
            }
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        //{
        //    //ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    if (grantResults.Length >= 0)
        //        if (permissions[0] == Permission.Denied)
        //        {
        //             Toast.MakeText((Android.Content.Context)"Se necesitan todos los permisos solicitados para que la aplicacion funcione correctamente", 0, ToastLength.Short ).Show();
        //        }

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        public async override void OnBackPressed()
        {
            var navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;
            if (navigation is null || navigation.NavigationStack.Count > 1 || navigation.ModalStack.Count > 0)
            {
                base.OnBackPressed();
            }

        }
        public void onBackPressed()
        {

        }
    }
}
