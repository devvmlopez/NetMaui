using Foundation;
using UIKit;
using UserNotifications;

namespace InntecMobileNetMaui
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            RegisterForRemoteNotifications(application);

            return base.FinishedLaunching(application, launchOptions);
        }

        /// <summary>
        /// Reqeust permission for notifications
        /// </summary>
        /// <param name="application"></param>
        /// <remarks>from https://github.com/DimaOrdenov/MauiSamples/blob/main/MauiPushNotifications/Platforms/iOS/AppDelegate.cs</remarks>
        private void RegisterForRemoteNotifications(UIApplication application)
        {
            UNUserNotificationCenter.Current.RequestAuthorization(
                UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                (isSuccess, error) =>
                {
                    if (error != null)
                    {
                        // no op, not going to do anything about this for now
                    }
                });

            application.RegisterForRemoteNotifications();
        }
    }
   
}
