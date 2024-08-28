using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Services;
using InntecMobileNetMaui.ViewModels;



namespace InntecMobileNetMaui.Views.Cards;

public partial class CardPageList : ContentPage
{   
    CardViewModel cardViewModel;
    ICardsService<CardModel> cardService;
    private bool isClosed = true;
    private const uint AnimationDuration = 200u;
   // public CardPageList(CardViewModel cardViewModel, ICardService<CardModel> cardService)
    public CardPageList()
    {   
		InitializeComponent();
        this.cardViewModel = cardViewModel;
        this.BindingContext = cardViewModel;
        this.cardService = cardService;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //if (cardViewModel.Cards.Count == 0)
        //{
        //    cardViewModel.ExecuteLoadCardsCommand.Execute(null);
        //}

    }
    private async void CerrarSession_Tapped(object sender, TappedEventArgs e)
    {
        CloseAnimation();
        await Shell.Current.GoToAsync("//Login");
        //Falta implementar la logica de cerrar la session
    }
    private void menu_Tapped(object sender, TappedEventArgs e)
    {
        if (isClosed) OpenAnimation();
        else CloseAnimation();
    }
    private async void MisTarjetas_Tapped(object sender, TappedEventArgs e)
    {
        CloseAnimation();
        await Shell.Current.GoToAsync("//CardPage");
        // Como Demo la logica dicta que deberia ir al listado de todas las tarjetas
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        _ = MainContentGrid.TranslateTo(this.Width * 0.5, 0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.RotateTo(-10, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.ScaleTo(0.9, AnimationDuration);


        //_ = MainContentGrid.FadeTo(0.8, AnimationDuration);
    }

    private void GridArea_Tapped(object sender, TappedEventArgs e)
    {
        _ = MainContentGrid.TranslateTo(0, 0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.RotateTo(0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.ScaleTo(1, AnimationDuration);

        //_ = MainContentGrid.FadeTo(1, AnimationDuration);
    }
    private void OpenAnimation()
    {
        _ = MainContentGrid.TranslateTo(this.Width * 0.5, 0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.RotateTo(-10, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.ScaleTo(0.9, AnimationDuration);
        isClosed = false;
    }

    private void CloseAnimation()
    {
        _ = MainContentGrid.TranslateTo(0, 0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.RotateTo(0, AnimationDuration, Easing.CubicIn);
        _ = MainContentGrid.ScaleTo(1, AnimationDuration);
        isClosed = true;
    }

  
    //async void TapGestureRecognizer_Tapped_CerrarSesion(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    //{
    //    GridArea_Tapped(this, new TappedEventArgs(null));

    //    await Navigation.PushAsync(new LoginPage()); //Falta implementar la logica de cerrar la session

    //}
}