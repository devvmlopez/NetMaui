using Acr.UserDialogs;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Cards;
using InntecMobileNetMaui.Views.CustomView;
using InntecMobileNetMaui.Views.Login;
using Microsoft.Maui.Controls.Handlers.Items;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace InntecMobileNetMaui.Views.Cards;

public partial class CardsPage : ContentPage
{
    bool _exit;
    CardsViewModel viewModel;
    public ObservableCollection<CardModel> Cards { get; set; } //Anotacion
    //CardsPage RootPage => Application.Current.MainPage as CardsPage;

    /// <summary>
    /// Inicializar objetos
    /// </summary>
    public CardsPage()
    {
        InitializeComponent();
        this.BindingContext = viewModel = new CardsViewModel(this);
        //productos_button.TranslateTo(200, 0);
        //btn_Cerrar.TranslateTo(200, 0);


        //Task.Delay(1000);
        //Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        //{
        //    if (viewModel.Images.Count == 0) return true;
        //    try
        //    {
        //        MainCarouselPromos.Position = (MainCarouselPromos.Position + 1) % viewModel.Images.Count;

        //        return true;
        //    }
        //    catch { return false; }

        //});

    }

    /// <summary>
    /// Cargar tarjetas de usuario
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!Constants.RecordatorioBloqueos)
        {
            //await Navigation.PushPopupAsync(new Popups.CardPage.RecordatorioBloqueoPage(), true);
            Constants.RecordatorioBloqueos = true;
        }


        _exit = false;
        if (viewModel.Cards.Count == 0)
        {
            viewModel.LoadCardsCommand.Execute(null);
            //await menu_button.TranslateTo(50, 0, 500, Easing.Linear);
            await Task.Delay(1000);
            //await menu_button.TranslateTo(65, 0, 200, Easing.BounceOut);
        }

        if (App.benefithub)
        {
            App.benefithub = false;

            //await Xamarin.Essentials.Launcher.OpenAsync(@"https://inntecdescuentos.benefithub.com");

        }
        if (App.Cards.Count == 0)
        this.BindingContext = viewModel = new CardsViewModel(this);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        //MessagingCenter.Unsubscribe<NewCardPage, CardModel>(this, "Nueva tarjeta");
        //MessagingCenter.Unsubscribe<MoreOptionsPage, CardModel>(this, Constants.LoadCards);
        //MessagingCenter.Unsubscribe<CardNipPage, CardNipModel>(this, "Cambiar NIP");
    }


    /// <summary>
    /// Desactivar tarjeta temporalmente
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        Switch item = sender as Switch;
        Models.CardModel selectedCard = item.BindingContext as Models.CardModel;
        if (selectedCard == null) return;
        if (e.Value != selectedCard.BEstatus)
        {
            if (item.IsToggled)
            {
                if (await DisplayAlert("Alerta!", "Estas seguro que quieres activar la tarjeta?", "Si, Activar.", "Cancelar").ConfigureAwait(true))
                {
                    viewModel.BlockCardCommand.Execute(selectedCard);
                    item.ThumbColor = Color.FromHex("3487CB");
                    item.OnColor = Color.FromHex("503487CB");
                }
                else
                {
                   // item.ThumbColor = Color.LightGray;
                    item.IsToggled = selectedCard.BEstatus;
                    return;
                }
            }
            else
            {
                if (await DisplayAlert("Alerta!", "Estas seguro que quieres desactivar la tarjeta?", "Si, Desactivar", "Cancelar").ConfigureAwait(true))
                {
                    viewModel.BlockCardCommand.Execute(selectedCard);
                   // item.ThumbColor = Color.LightGray;
                }
                else
                {
                    item.ThumbColor = Color.FromHex("3487CB");
                    item.OnColor = Color.FromHex("503487CB");
                    item.IsToggled = selectedCard.BEstatus;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Modulo de Combustibles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Gas_Clicked(object sender, EventArgs e)
    {
        viewModel.mainViewModel.VerifyToken();
        //Navigation.PushAsync(new MainGasPage(ref viewModel.mainViewModel));
    }

    /// <summary>
    /// Modulo de Viaticos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Viatics_Clicked(object sender, EventArgs e)
    {
        viewModel.mainViewModel.VerifyToken();
        //Navigation.PushAsync(new MainViaticsPage(ref viewModel.mainViewModel));
    }

    /// <summary>
    /// Modulo de BenefitHub
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Benefit_Hub(object sender, EventArgs e)
    {
        App.BenefitLogin = false;
        //Navigation.PushAsync(new BenefitHub.BenefitHubRegisterPage(this), true);
    }

    /// <summary>
    /// Alerta ANDROID al presionar boton Back
    /// </summary>
    /// <returns></returns>
    protected override bool OnBackButtonPressed()
    {
        //if (!_exit)
        //{
        //    ToastConfig toastConfig = new ToastConfig("Presiona nuevamente para salir");
        //    toastConfig.SetDuration(3000);
        //    toastConfig.SetBackgroundColor(Color.DimGray);
        //    UserDialogs.Instance.Toast(toastConfig);
        //    _exit = true;
        //    return true;
        //}
        return base.OnBackButtonPressed();
    }

    //async void SwipeGestureRecognizer_Swiped(System.Object sender, Xamarin.Forms.SwipedEventArgs e)
    //{
    //    if (e.Direction == SwipeDirection.Left)
    //    {
    //        await menu_button.TranslateTo(0, 0, 500, Easing.Linear);
    //        await Task.Delay(3000);
    //        await menu_button.TranslateTo(65, 0, 200, Easing.BounceOut);
    //    }
    //}

    async void TapGestureRecognizer_Tapped(Object sender, EventArgs e)
    {
        int finalizacion = 0;
        if (Device.iOS == Device.RuntimePlatform)
            finalizacion = 30;
        //await productos_button.TranslateTo(0, 0, 500, Easing.Linear);
        //await btn_Cerrar.TranslateTo(finalizacion, 0, 400, Easing.Linear);

    }

    async void btn_Cerrar_Pressed(System.Object sender, System.EventArgs e)
    {
        //await productos_button.TranslateTo(200, 0, 200, Easing.BounceOut);
        //await btn_Cerrar.TranslateTo(200, 0, 200, Easing.BounceOut);
    }

    async void BtnCombustible_Pressed(System.Object sender, System.EventArgs e)
    {
        btn_Cerrar_Pressed(sender, e);
        //await Navigation.PushPopupAsync(new FAQsPage(enumFAQs.COMBUSTIBLE), true);
    }

    async void BtnDespensa_Pressed(System.Object sender, System.EventArgs e)
    {
        btn_Cerrar_Pressed(sender, e);
       // await Navigation.PushPopupAsync(new FAQsPage(enumFAQs.DESPENSA), true);
    }

    async void BtnPremium_Pressed(System.Object sender, System.EventArgs e)
    {
        btn_Cerrar_Pressed(sender, e);
        //await Navigation.PushPopupAsync(new FAQsPage(enumFAQs.PREMIUM), true);
    }

    async void BtnViaticos_Pressed(System.Object sender, System.EventArgs e)
    {
        btn_Cerrar_Pressed(sender, e);
        //await Navigation.PushPopupAsync(new FAQsPage(enumFAQs.VIATICOS), true);
    }

    async void BtnToken_Pressed(System.Object sender, System.EventArgs e)
    {
        btn_Cerrar_Pressed(sender, e);
        //await Navigation.PushPopupAsync(new FAQsPage(enumFAQs.TOKEN), true);
    }


    private void LstCards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var myCard = e.SelectedItem as CardModel;
        Navigation.PushAsync(new CardDetailPage(myCard));
    }

    private void MiTarjeta_Tapped(object sender, TappedEventArgs e)
    {
        //var myCard = e.SelectedItem as CardModel;
        //Navigation.PushAsync(new CardDetailPage(myCard));
    }

    private void LstCards_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
    {
        var myCard = e.CurrentItem as CardModel;
        Navigation.PushAsync(new CardDetailPage(myCard));
    }
}