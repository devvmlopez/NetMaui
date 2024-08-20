using InntecMobileNetMaui.Views;
using InntecMobileNetMaui.Views.Cards;
using Mopups.Services;
namespace InntecMobileNetMaui.Views.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void Btn_Entrar_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CardPageList");
    }

    private void Btn_Recuperar_Clicked(object sender, EventArgs e)
    {
        // Shell.Current.GoToAsync("//NewCardPage");
        
    }
}