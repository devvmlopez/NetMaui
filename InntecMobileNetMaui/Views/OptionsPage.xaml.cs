using InntecMobileNetMaui.ViewModels;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;

namespace InntecMobileNetMaui.Views;

public partial class OptionsPage : ContentPage
{
    string evaluarDark;
    private OptionsPageViewModel _viewModel;
    public OptionsPage()
	{
		InitializeComponent();
        this.BindingContext = _viewModel = new OptionsPageViewModel();
        Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
    }
    protected override void OnAppearing()
    {
        IsBusy = false;
        base.OnAppearing();
        AppTheme currentTheme = Application.Current.RequestedTheme;
        evaluarDark = Preferences.Default.Get("IconNotificacion", string.Empty);
        if (currentTheme == AppTheme.Light && DeviceInfo.Platform == DevicePlatform.Android)
        {
            evaluarDark = "png" + evaluarDark;
        }
        MenuPrincipal.IconIzq = evaluarDark;

        CodeVersion.Text = "Version: " + VersionTracking.Default.CurrentVersion.ToString();

    }

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

    private async void User_Tapped(object sender, TappedEventArgs e)
    {
        IsBusy = true;
        await Navigation.PushAsync(new MyDataPage());
        IsBusy = false;
    }
    private async void Terminos_Tapped(object sender, TappedEventArgs e)
    {
        IsBusy = true;
        await Navigation.PushAsync(new TermsAndConditionsPage(true));
        IsBusy = false;
    }
    private async void Logout_Tapped(object sender, TappedEventArgs e)
    {
        IsBusy = true;
        var popup = new YesOrNotAlert();
        InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
        InformativeViewModel.Instance.Title = "Mensaje";
        InformativeViewModel.Instance.Message = "¿ Deseas salir de la sesión ?";

        await MopupService.Instance.PushAsync(popup);
        var rvalue = await popup.PopupDismissedTask;

        if (rvalue == "YES")
        {
            await Shell.Current.GoToAsync("//Login");
        }
        IsBusy = false;
    }

    private async void EliminarCuenta_Tapped(object sender, TappedEventArgs e)
    {
        await Launcher.Default.OpenAsync(@"https://www.inntecmp.com.mx/saldo/help-center/account");
    }
}