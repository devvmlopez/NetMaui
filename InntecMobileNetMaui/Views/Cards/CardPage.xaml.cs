using Mopups.Services;

namespace InntecMobileNetMaui.Views.Cards;

public partial class CardPage : ContentPage
{
    //CardViewModel cardViewModel;
    //ICardService<CardModel> cardService;
    private const uint AnimationDuration = 200u;
    public CardPage() /*CardViewModel cardViewModel, ICardService<CardModel> cardService*/
    {
        InitializeComponent();
        IsBusy = false;
        //this.cardViewModel = cardViewModel;
        //this.BindingContext = cardViewModel;
        //this.cardService = cardService;
        //txtUser.Text = Constants.Instance.UserName;
    }
    private async void OpenAnimation()
    {
        await swipeContent.ScaleYTo(0.9, 300, Easing.SinOut);
        await swipeContent.RotateTo(-15, 300, Easing.SinOut);
    }

    private async void CloseAnimation()
    {
        await swipeContent.RotateTo(0, 300, Easing.SinOut);
        await swipeContent.ScaleYTo(1, 300, Easing.SinOut);
    }

    private void OpenSwipe(object sender, EventArgs e)
    {
        //if (cardViewModel.Cerrado)
        //{
        //    //MainSwipeView.Open(OpenSwipeItem.LeftItems);
        //    OpenAnimation();
        //}
        //else
        //{
        //    //MainSwipeView.Close();
        //    CloseAnimation();
        //}
        //cardViewModel.Cerrado = !cardViewModel.Cerrado;
    }

    void SwipeGestureRecognizer_Swiped(System.Object sender, SwipedEventArgs e)
    {
        //if (e.Direction == SwipeDirection.Left)

        //{
        //    // MainSwipeView.Close();
        //    CloseAnimation();
        //    cardViewModel.Cerrado = true;
        //}
        //else if (e.Direction == SwipeDirection.Right)
        //{
        //    //MainSwipeView.Open(OpenSwipeItem.LeftItems);
        //    OpenAnimation();
        //    cardViewModel.Cerrado = false;
        //}
    }
    protected override void OnAppearing()
    {
        //base.OnAppearing();

        //if (cardViewModel.Cards.Count == 0)
        //{
        //    cardViewModel.ExecuteLoadCardsCommand.Execute(null);
        //}
    }

    void Crv_Cards_CurrentItemChanged(System.Object sender, CurrentItemChangedEventArgs e)
    {
        //cardViewModel.Position = ((CarouselView)sender).Position;
        //if (!cardViewModel.LoadingBalance)
        //{
        //    cardViewModel.LoadingBalance = true;
        //    return;
        //}
        //List<object> list = new List<object>();
        //list.Add(DateTime.Now.Date);
        //list.Add(e.CurrentItem);
        //cardViewModel.ExecutCardMovementsCommand.Execute(list);
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
 

    private  void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        _ = MainContentGrid.TranslateTo(this.Width * 0.5, 0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.RotateTo(-10, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.ScaleTo(0.9, AnimationDuration);


        //_ = MainContentGrid.FadeTo(0.8, AnimationDuration);
    }

    private  void GridArea_Tapped(object sender, TappedEventArgs e)
    {
        _ = MainContentGrid.TranslateTo(0, 0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.RotateTo(0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.ScaleTo(1, AnimationDuration);

        //_ = MainContentGrid.FadeTo(1, AnimationDuration);
    }

    async void TapGestureRecognizer_Tapped_CerrarSesion(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        GridArea_Tapped(this, new TappedEventArgs(null));

        await Shell.Current.GoToAsync("//Login");

    }
    private void MisTarjetas_Tapped(object sender, TappedEventArgs e)
    {
        CloseAnimation();
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
}