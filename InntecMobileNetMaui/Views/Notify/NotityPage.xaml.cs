using InntecMobileNetMaui.ViewModels.Notify;
using Plugin.LocalNotification;

namespace InntecMobileNetMaui.Views.Notify;

public partial class NotityPage : ContentPage
{
    public NotifyViewModel viewModel;
    string evaluarDark;
    public NotityPage()
    {
        InitializeComponent();
        this.BindingContext = viewModel = new NotifyViewModel(this);
        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
        Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (viewModel.Notifications.Count == 0)
            viewModel.getNotificationsCommand.Execute(null);

       // MenuPrincipal.IconIzq = Preferences.Default.Get("IconNotificacion", string.Empty) ;


        AppTheme currentTheme = Application.Current.RequestedTheme;
        evaluarDark = Preferences.Default.Get("IconNotificacion", string.Empty);
        if (currentTheme == AppTheme.Light && DeviceInfo.Platform == DevicePlatform.Android)
        {
            evaluarDark = "png" + evaluarDark;
        }
        MenuPrincipal.IconIzq = evaluarDark;
    }
    /// <summary>
    /// Registra los cambios del tema para mostrar el icono de notificaciones dependiendo el modo 
    /// </summary>
    private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {

        AppTheme currentTheme = Application.Current.RequestedTheme;
        evaluarDark = Preferences.Default.Get("IconNotificacion", string.Empty);
        if (currentTheme == AppTheme.Light && DeviceInfo.Platform == DevicePlatform.Android)
        {
            evaluarDark = "png" + evaluarDark;
        }
        MenuPrincipal.IconIzq = evaluarDark;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GC.Collect();
    }

    private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
    {
        if (e.IsDismissed)
        {

        }
        else if (e.IsTapped)
        {

        }
    }

    void Button_Pressed(System.Object sender, System.EventArgs e)
    {
        this.Navigation.PopModalAsync();
    }
}
