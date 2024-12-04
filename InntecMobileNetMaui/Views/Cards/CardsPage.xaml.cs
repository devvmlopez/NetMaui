using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Cards;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Contigo;
using InntecMobileNetMaui.Views.Gas;
using InntecMobileNetMaui.Views.Viatics;
using Plugin.LocalNotification;

namespace InntecMobileNetMaui.Views.Cards;

public partial class CardsPage : ContentPage
{
    bool _exit;
    string evaluarDark;
    CardsViewModel viewModel;
   
    CardsPage RootPage => Application.Current.MainPage as CardsPage;

    /// <summary>
    /// Inicializar objetos
    /// </summary>
    [Obsolete]
    public CardsPage()
    {
        
        InitializeComponent();

        Task.Delay(1000);
        Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        {
            if (viewModel.Images.Count == 0) return true;
            try
            {
                MainCarouselPromos.Position = (MainCarouselPromos.Position + 1) % viewModel.Images.Count;

                return true;
            }
            catch { return false; }

        });


        Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;

    }

    /// <summary>
    /// Cargar tarjetas de usuario y algunos procesos relacionados al icono de notificaciones
    /// </summary>
    protected override async void OnAppearing()
    {
        lottie.IsVisible = true;
        BindingContext = viewModel = new CardsViewModel(this);
        if (viewModel.notifyIco == "" || viewModel.notifyIco == null)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
                viewModel.notifyIco = "notification.svg";
            else
                viewModel.notifyIco = "pngnotification.png";
        }
        lottie.IsVisible = false;
        base.OnAppearing();
        if (!Constants.RecordatorioBloqueos)
        {
            Constants.RecordatorioBloqueos = true;
        }


        _exit = false;
        if (viewModel.Cards.Count == 0)
        {
            viewModel.LoadCardsCommand.Execute(null);
            await Task.Delay(1000);
        }

        if (App.benefithub)
        {
            App.benefithub = false;

            await Launcher.Default.OpenAsync(@"https://inntecdescuentos.benefithub.com");

        }
        if (App.Cards.Count == 0);

        if (viewModel.notifyIco == "" || viewModel.notifyIco == null)
            viewModel.notifyIco = Preferences.Default.Get("IconNotificacion", string.Empty);
        else
            MenuPrincipal.IconIzq = viewModel.notifyIco;

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
    }

    /// <summary>
    /// Modulo de Combustibles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Gas_Clicked(object sender, EventArgs e)
    {
        viewModel.mainViewModel.VerifyToken();
        Navigation.PushAsync(new MainGasPage(ref viewModel.mainViewModel));
    }

    /// <summary>
    /// Modulo de Viaticos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Viatics_Clicked(object sender, EventArgs e)
    {
        viewModel.mainViewModel.VerifyToken();
        Navigation.PushAsync(new MainViaticsPage(ref viewModel.mainViewModel)); 
    }

    /// <summary>
     /// Modulo de BenefitHub
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Benefit_Hub(object sender, EventArgs e)
    {
        App.BenefitLogin = false;
        
        await Launcher.Default.OpenAsync(@"https://inntecdescuentos.benefithub.com");
        //await Navigation.PushAsync(new MenuContigo());  Implementacion de inntec Contigo
    }

    /// <summary>
    /// Alerta ANDROID al presionar boton Back
    /// </summary>
    /// <returns></returns>
    protected override bool OnBackButtonPressed()
    {
        return base.OnBackButtonPressed();
    }

    async void TapGestureRecognizer_Tapped(Object sender, EventArgs e)
    {
        int finalizacion = 0;
        if (DeviceInfo.Platform == DevicePlatform.iOS)
            finalizacion = 30;

    }

    async void btn_Cerrar_Pressed(System.Object sender, System.EventArgs e)
    {

    }
    async void SwipeGestureRecognizer_Swiped(System.Object sender, SwipedEventArgs e)
    {
        if (e.Direction == SwipeDirection.Left)
        {
            await Task.Delay(3000);
        }
    }
    /// <summary>
    /// Nos ayuda a viajar con la Notificacion Local
    /// </summary>
    /// <returns></returns>
    private async void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
    {
        if (e.IsDismissed)
        {

        }
        else if (e.IsTapped)
        {
            await Shell.Current.GoToAsync("//NotifyPage");
        }
    }
}