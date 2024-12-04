using CommunityToolkit.Maui.Views;
using InntecMobileNetMaui.ViewModels;
using InntecMobileNetMaui.ViewModels.Cards;
using Mopups.Services;
using System.ComponentModel;
using static InntecMobileNetMaui.Models.Enumeradores;
using static System.Net.Mime.MediaTypeNames;

namespace InntecMobileNetMaui.Views.FAQ;

public partial class FAQmenu 
{
    FAQmenuViewModel viewModel;

    public FAQmenu()
	{
		InitializeComponent();
       
	}
    protected override async void OnAppearing()
    {
        //BtnToken.IsEnabled = true;
        //BtnDespensa.IsEnabled = true;
        //BtnPremium.IsEnabled = true;
        //BtnViaticos.IsEnabled = true;
        //BtnToken.IsEnabled = true;
        //BtnTerminos.IsEnabled = true;
        BindingContext = new FAQmenuViewModel();
    }
    async void btn_Cerrar_Pressed(System.Object sender, System.EventArgs e)
    {
        // await productos_button.TranslateTo(200, 0, 200, Easing.BounceOut);
        // await btn_Cerrar.TranslateTo(200, 0, 200, Easing.BounceOut);
    }
    async void BtnCombustible_Pressed(System.Object sender, System.EventArgs e)
    {
       // BtnCombustible.IsEnabled = false;
       // btn_Cerrar_Pressed(sender, e);
       ////var valor =  await MopupService.Instance.PushAsync(new FAQsPage(enumFAQs.COMBUSTIBLE), true);

       // var popup = new FAQsPage(enumFAQs.COMBUSTIBLE);
       // await MopupService.Instance.PushAsync(popup);
       // var rvalue = await popup.PopupDismissedTask;
       // btnCombustible = rvalue.ToString();
    }

    async void BtnDespensa_Pressed(System.Object sender, System.EventArgs e)
    {
        BtnDespensa.IsEnabled = false;
        btn_Cerrar_Pressed(sender, e);
        await MopupService.Instance.PushAsync(new FAQsPage(enumFAQs.DESPENSA), true);
        
    }

    async void BtnPremium_Pressed(System.Object sender, System.EventArgs e)
    {
        BtnPremium.IsEnabled = false;   
        btn_Cerrar_Pressed(sender, e);
        await MopupService.Instance.PushAsync(new FAQsPage(enumFAQs.PREMIUM), true);
        
    }

    async void BtnViaticos_Pressed(System.Object sender, System.EventArgs e)
    {
        BtnViaticos.IsEnabled = false;
        btn_Cerrar_Pressed(sender, e);
        await MopupService.Instance.PushAsync(new FAQsPage(enumFAQs.VIATICOS), true);
        
    }

    async void BtnToken_Pressed(System.Object sender, System.EventArgs e)
    {
        BtnToken.IsEnabled = false;
        btn_Cerrar_Pressed(sender, e);
        await MopupService.Instance.PushAsync(new FAQsPage(enumFAQs.TOKEN), true);
        
    }

    private async void BtnTerminos_Pressed(object sender, EventArgs e)
    {
        BtnTerminos.IsEnabled = false;
        productos_button.IsVisible = false;
        await Navigation.PushModalAsync(new TermsAndConditionsPage(false));
        
    }

    private void BtnPrivacidad_Pressed(object sender, EventArgs e)
    {
        Launcher.Default.OpenAsync("https://www.inntecmp.com.mx/sitio/AvisoPrivacidad.aspx").ConfigureAwait(true);
    }
}