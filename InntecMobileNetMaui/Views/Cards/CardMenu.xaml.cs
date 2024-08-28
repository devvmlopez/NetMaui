using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Cards;

namespace InntecMobileNetMaui.Views.Cards;

public partial class CardMenu : ContentPage
{

    bool _exit;
    CardsViewModel viewModel;
    public CardMenu()
	{
		InitializeComponent();
 
        //this.BindingContext = viewModel = new CardsViewModel(this);
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();



        if (!Constants.RecordatorioBloqueos)
        {
           // await Navigation.PushPopupAsync(new Popups.CardPage.RecordatorioBloqueoPage(), true);
            Constants.RecordatorioBloqueos = true;
        }

        _exit = false;
        if (viewModel.Cards.Count == 0)
        {
            viewModel.LoadCardsCommand.Execute(null);
           // await menu_button.TranslateTo(50, 0, 500, Easing.Linear);
            await Task.Delay(1000);
            //await menu_button.TranslateTo(65, 0, 200, Easing.BounceOut);
        }

        //if (App.benefithub)
        //{
        //    App.benefithub = false;

        //    await Xamarin.Essentials.Launcher.OpenAsync(@"https://inntecdescuentos.benefithub.com");

        //}
    }

    protected override void OnDisappearing()
    {
        //base.OnDisappearing();
        //MessagingCenter.Unsubscribe<NewCardPage, CardModel>(this, "Nueva tarjeta");
        //MessagingCenter.Unsubscribe<MoreOptionsPage, CardModel>(this, Constants.LoadCards);
        //MessagingCenter.Unsubscribe<CardNipPage, CardNipModel>(this, "Cambiar NIP");
    }
    private async void MisTarjetas_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//CardDetailPage");
        // Como Demo la logica dicta que deberia ir al listado de todas las tarjetas
    }
}