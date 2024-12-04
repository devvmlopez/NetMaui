using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels;
using InntecMobileNetMaui.ViewModels.Cards;
using InntecMobileNetMaui.Views.CustomView;
using Mopups.Services;
using System;

namespace InntecMobileNetMaui.Views.Cards;

public partial class CardDetailPage : ContentPage, IDisposable
{
    CardDetailViewModel ViewModels;
    CardModel cardModel;
    CardsViewModel viewModel;
    string evaluarDark;
    /// <summary>
    /// Inicializar objetos
    /// </summary>
    /// <param name="cardModel">Datos de tarjeta</param>
    /// <param name="login">Datos del usuario que inicio sesion</param>
    public CardDetailPage(CardModel cardModel)
    {
        var OnlyOneCard = cardModel as CardModel;
        InitializeComponent();
        this.cardModel = cardModel;
        this.BindingContext = ViewModels = new CardDetailViewModel(cardModel, this);
        Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
    }
    protected override void OnAppearing()
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
    /// Navega hacia el login "Evento cerrar session"
    /// </summary>
    async void TapGestureRecognizer_Tapped_CerrarSesion(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//Login");
    }
    /// <summary>
    /// Navega hacia el detalle de la tarjeta "para refrescar los cambios"
    /// </summary>
    private void MisTarjetas_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("//CardPageList");
    }
    /// <summary>
    /// Evento que controla el filtro de movimientos de la tarjeta
    /// </summary>
    private void Pkr_Month_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewModels.CardMovementsMonthCommand.Execute(((Picker)sender).SelectedItem);
    }
    public void Dispose()
    {
        ViewModels.Dispose();
    }
    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GC.Collect();
    }

    /// <summary>
    /// Cancelar tarjeta 
    /// </summary>
    private void Cancelar_card(object sender, TappedEventArgs e)
    {
        var myCard = cardModel as CardModel;
        viewModel.CancelCardCommand.Execute(cardModel as CardModel);
    }

    private async void Cancelar_Tarjeta(object sender , TappedEventArgs e)
    {
        var myCard = cardModel as CardModel;
        viewModel.CancelCardCommand.Execute(cardModel as CardModel);
    }
    /// <summary>
    /// Bloquear tarjeta 
    /// </summary>
    private void BlockCard_Tapped(object sender, TappedEventArgs e)
    {
        var myCard = cardModel as CardModel;
        ViewModels.BlockCardCommand.Execute(cardModel as CardModel);
    }
    /// <summary>
    /// Eliminar tarjeta 
    /// </summary>
    private async void DeleteCard_Tapped(object sender, TappedEventArgs e)
    {
        DeleteCard.IsEnabled = false;
        var myCard = cardModel as CardModel;
        ViewModels.DeleteCardCommand.Execute(cardModel as CardModel);//.ConfigureAwait(true);
        DeleteCard.IsEnabled = true;
    }
    /// <summary>
    /// Cancelar tarjeta BTN
    /// </summary>
    private async void CancelCard_Tapped(object sender, TappedEventArgs e)
    {
        TimeSpan.FromSeconds(1);
        CancelCard.IsEnabled = false;
           // var myCard = cardModel as CardModel;
        ViewModels.CancelCardCommand.Execute(cardModel);
        CancelCard.IsEnabled = true;
        
    }

    /// <summary>
    /// MoreOptions  de tarjeta  
    /// </summary>
    private async void MoreOptionsCard_Tapped(object sender, TappedEventArgs e)
    {
        BlockCard.IsEnabled = false;
        var myCard = cardModel as CardModel;
        await MopupService.Instance.PushAsync(new Views.Alerts.MoreOptionsPage(cardModel, true)).ConfigureAwait(true);
        BlockCard.IsEnabled = true;
    }

    void TapFiltroMes_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
         Pkr_Month.Focus();
    }
}