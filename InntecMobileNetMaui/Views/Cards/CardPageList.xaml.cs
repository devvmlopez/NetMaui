using Microsoft.Maui.Controls;

namespace InntecMobileNetMaui.Views.Cards;

public partial class CardPageList : ContentPage
{
    private const uint AnimationDuration = 200u;
    private bool isClosed = true;
    public CardPageList()
	{
		InitializeComponent();
    }
    private void CerrarSession_Tapped(object sender, TappedEventArgs e)
    {
        CloseAnimation();
        Shell.Current.GoToAsync("//CardPage");
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

    private void menu_Tapped(object sender, TappedEventArgs e)
    {
        if (isClosed) OpenAnimation();
        else CloseAnimation();
    }
    private void MisTarjetas_Tapped(object sender, TappedEventArgs e)
    {
        CloseAnimation();
        Shell.Current.GoToAsync("//CardPageList");
    }
    async void TapGestureRecognizer_Tapped_CerrarSesion(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        GridArea_Tapped(this, new TappedEventArgs(null));

        await Shell.Current.GoToAsync("//Login");

    }
}