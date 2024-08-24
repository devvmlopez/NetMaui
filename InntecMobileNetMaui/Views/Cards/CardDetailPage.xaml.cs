using InntecMobileNetMaui.Views.CustomView;
using Mopups.Services;

namespace InntecMobileNetMaui.Views.Cards;

public partial class CardDetailPage : ContentPage
{
	public CardDetailPage()
	{
		InitializeComponent();
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
}