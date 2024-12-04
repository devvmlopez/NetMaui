using Acr.UserDialogs;
using InntecMobileNetMaui.ViewModels;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.ViewModels.Cards;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;

namespace InntecMobileNetMaui.Views;

public partial class MyDataPage : ContentPage
{
    private MyDataViewModel _viewModel;
    bool _exit;
    string evaluarDark;
    public MyDataPage()
	{
		InitializeComponent();
        this.BindingContext = _viewModel = new MyDataViewModel(this);
        Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _exit = false;
        _viewModel.LoadUserData.Execute(null);


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
    /// <summary>
    /// Actualizar datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
        _viewModel.IsBusy = true;
        _viewModel.SaveUserData.Execute(null);
    }
    /// <summary>
    /// Regresar a pagina principal
    /// </summary>
    /// <returns></returns>
    protected override bool OnBackButtonPressed()
    {
        return false;
    }

    //async void Tapped_CerrarSesion(object sender, TappedEventArgs e)
    //{
    //    var popup = new YesOrNotAlert();
    //    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
    //    InformativeViewModel.Instance.Title = "Mensaje";
    //    InformativeViewModel.Instance.Message = "¿ Deseas salir de la sesión ?";

    //    await MopupService.Instance.PushAsync(popup);
    //    var rvalue = await popup.PopupDismissedTask;

    //    if (rvalue == "YES")
    //    {
    //        await Shell.Current.GoToAsync("//Login");
    //    }
        
    //}
}