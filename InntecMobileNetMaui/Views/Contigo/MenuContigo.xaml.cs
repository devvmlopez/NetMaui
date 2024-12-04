using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.ViewModels.Cards;
using InntecMobileNetMaui.ViewModels.Contigo;
using InntecMobileNetMaui.Views.Alerts;
using Microsoft.Maui.Layouts;
using Mopups.Services;


namespace InntecMobileNetMaui.Views.Contigo;

public partial class MenuContigo : ContentPage
{
    string evaluarDark ;
    MenuContigoViewModel viewModel;
    public MenuContigo()
    {
		InitializeComponent();
        Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
        BindingContext = viewModel = new MenuContigoViewModel();
    }

    protected override async void OnAppearing()
    {
        
        IsBusy = false;
        viewModel.LoadUserData.Execute(null);
        AppTheme currentTheme = Application.Current.RequestedTheme;
        evaluarDark = Preferences.Default.Get("IconNotificacion", string.Empty);
        if (currentTheme == AppTheme.Light && DeviceInfo.Platform == DevicePlatform.Android)
        {
            evaluarDark = "png" + evaluarDark;
        }
        MenuPrincipal.IconIzq = evaluarDark;
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

    private async void Consultar_Tapped(object sender, TappedEventArgs e)
    {
        InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
        InformativeViewModel.Instance.Title = "Mensaje";
        InformativeViewModel.Instance.Message = "Limpieza Dental Gratuita al año \r\npara el titular";
        await MopupService.Instance.PushAsync(InformativeAlert.Instance);
    }
    private async void NoDisponible_Tapped(object sender, TappedEventArgs e)
    {
        InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
        InformativeViewModel.Instance.Title = "Mensaje";
        InformativeViewModel.Instance.Message = "Esta opcion no esta disponible el beneficio ya fue utilizado, disculpe las molestias.";
        await MopupService.Instance.PushAsync(InformativeAlert.Instance);
    }
    private async void Disponible_Tapped(object sender, TappedEventArgs e)
    {
        IsBusy = true;
        var popup = new YesOrNotAlert();
        InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
        InformativeViewModel.Instance.Title = "Mensaje";
        InformativeViewModel.Instance.Message = "¿ Deseas utilizar este beneficio ?";

        await MopupService.Instance.PushAsync(popup);
        var rvalue = await popup.PopupDismissedTask;

        if (rvalue == "YES")
        {
            
            //Mostrar la informacion para usar el beneficio ....
            
        }
        else
        {
            
        }
        IsBusy = false;
    }

    private void TapDespensa_Tapped(object sender, TappedEventArgs e)
    {
        
        if (ModalDespensa.HeightRequest == 300)
        {

            ModalDespensa.HeightRequest = 60;
            ModalCombustible.HeightRequest = 300;
            AbsoluteLayout.SetLayoutBounds(ModalDespensa, new Rect(0, 0.596, 1, 3.4));
            AbsoluteLayout.SetLayoutFlags(ModalDespensa, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(ModalCombustible, new Rect(0, 0.596, 1, 1));
            AbsoluteLayout.SetLayoutFlags(ModalCombustible, AbsoluteLayoutFlags.All);

        }
         else 
        {

            ModalDespensa.HeightRequest = 300;
            ModalCombustible.HeightRequest = 60;
            AbsoluteLayout.SetLayoutBounds(ModalCombustible, new Rect(0, 0.596, 1, 3.4));
            AbsoluteLayout.SetLayoutFlags(ModalCombustible, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(ModalDespensa, new Rect(0, 0.6, 1, 1));
            AbsoluteLayout.SetLayoutFlags(ModalDespensa, AbsoluteLayoutFlags.All);
            
        }
      

    }
    private void TapCombustible_Tapped(object sender, TappedEventArgs e)
    {

         if (ModalCombustible.HeightRequest == 300)
        {
            ModalCombustible.HeightRequest = 60;
            ModalDespensa.HeightRequest = 300;
            AbsoluteLayout.SetLayoutBounds(ModalDespensa, new Rect(0, 0.596, 1, 1));
            AbsoluteLayout.SetLayoutFlags(ModalDespensa, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(ModalCombustible, new Rect(0, 0.6, 1, 3.4));
            AbsoluteLayout.SetLayoutFlags(ModalCombustible, AbsoluteLayoutFlags.All);
        }
        else 
        {
            ModalCombustible.HeightRequest = 300;
            ModalDespensa.HeightRequest = 60;
            AbsoluteLayout.SetLayoutBounds(ModalDespensa, new Rect(0, 0.596, 1, 3.4));
            AbsoluteLayout.SetLayoutFlags(ModalDespensa, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(ModalCombustible, new Rect(0, 0.6, 1, 1));
            AbsoluteLayout.SetLayoutFlags(ModalCombustible, AbsoluteLayoutFlags.All);
        }

    }
}