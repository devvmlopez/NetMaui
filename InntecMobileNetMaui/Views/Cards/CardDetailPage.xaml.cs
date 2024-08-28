using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Cards;
using InntecMobileNetMaui.Views.CustomView;
using Mopups.Services;
using System;

namespace InntecMobileNetMaui.Views.Cards;

public partial class CardDetailPage : ContentPage, IDisposable
{
    CardDetailViewModel ViewModels;
    CardModel cardModel;
    //AssistModel assistModel;
    //bool Assist;
    /// <summary>
    /// Inicializar objetos
    /// </summary>
    /// <param name="cardModel">Datos de tarjeta</param>
    /// <param name="login">Datos del usuario que inicio sesion</param>
    public CardDetailPage(CardModel cardModel)
    {
        InitializeComponent();
        this.cardModel = cardModel;
        //assistModel = new AssistModel();
        //assistModel.CsmId = cardModel.UsuarioCsmTarjetaId;
        this.BindingContext = ViewModels = new CardDetailViewModel(cardModel, this);
        //Assist = false;

    }
    async void TapGestureRecognizer_Tapped_CerrarSesion(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        // GridArea_Tapped(this, new TappedEventArgs(null));

        await Shell.Current.GoToAsync("//Login");

    }
    private void MisTarjetas_Tapped(object sender, TappedEventArgs e)
    {
        //CloseAnimation();
        Shell.Current.GoToAsync("//CardPageList");
    }

    private async void DeleteCard_Tapped(object sender, TappedEventArgs e)
    {
        await MopupService.Instance.PushAsync(new DeleteCard());
    }

    private async void CancelCard_Tapped(object sender, TappedEventArgs e)
    {
        await MopupService.Instance.PushAsync(new CancelCard());
    }

    async void AddNewCard_Tapped(System.Object sender, System.EventArgs e)
    {
        //await MopupService.Instance.PushAsync(new NewCardPage(new NewCardViewModel()), true);
        await MopupService.Instance.PushAsync(new NewCardPage());
    }
    async void BlockCard_Tapped(System.Object sender, System.EventArgs e)
    {
        //var item = Crv_Cards.CurrentItem;

        //BlockCardOptionsPage blockCard = new BlockCardOptionsPage((Models.CardModel)item, new BlockCardOptionsViewModel(cardService));
        //await MopupService.Instance.PushAsync(blockCard);

        await MopupService.Instance.PushAsync(new BlockCardOptionsPage());

    }
    private void Pkr_Month_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewModels.CardMovementsMonthCommand.Execute(((Picker)sender).SelectedItem);
    }

    public void Dispose()
    {
        ViewModels.Dispose();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GC.Collect();
    }

}