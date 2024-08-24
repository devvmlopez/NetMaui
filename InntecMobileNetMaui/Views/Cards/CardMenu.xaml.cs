namespace InntecMobileNetMaui.Views.Cards;

public partial class CardMenu : ContentPage
{
	public CardMenu()
	{
		InitializeComponent();
	}
    private async void MisTarjetas_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//CardDetailPage");
        // Como Demo la logica dicta que deberia ir al listado de todas las tarjetas
    }
}