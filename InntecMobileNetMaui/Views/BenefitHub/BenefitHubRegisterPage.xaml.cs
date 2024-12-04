using InntecMobileNetMaui.ViewModels.BenefitHub;
using InntecMobileNetMaui.Views.Cards;

namespace InntecMobileNetMaui.Views.BenefitHub;

public partial class BenefitHubRegisterPage : ContentPage
{
    private BenefitHubRegisterViewModel ViewModels;
    /// <summary>
    /// Inicializacion de objetos
    /// </summary>
    public BenefitHubRegisterPage(CardsPage cardsPage)
    {
        InitializeComponent();

        this.BindingContext = ViewModels = new BenefitHubRegisterViewModel(this, cardsPage);
        BtnRegister.Clicked -= BtnRegister_Clicked;
        BtnRegister.Clicked += new EventHandler(BtnRegister_Clicked);
            BtnTerms.Clicked += new EventHandler(BtnTerms_Clicked);
    }
    /// <summary>
    /// Terminos y condiciones
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void BtnTerms_Clicked(object sender, EventArgs e)
    {
        if (Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.Android)
            await Launcher.OpenAsync("https://inntecdescuentos.benefithub.com/ContentV2/TermsAndConditions/180913_Website_Terms&ConditionsESMX.pdf").ConfigureAwait(true);
        else
            await Navigation.PushAsync(new BenefitHubTemsPage(), true).ConfigureAwait(true);
    }
    /// <summary>
    /// Registrarse a BenefitHub
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnRegister_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(CP.Text, out int i))
        {
            DisplayAlert("Atencion", "Código  postal incorrecto", "Aceptar");
        }

        else if (!chkTerm.IsChecked)
            DisplayAlert("Atencion", "Los términos y condiciones no han sido aceptados.", "Aceptar");
        else
            ViewModels.BenefitHubCommand.Execute(LoginBenefit);
    }
    /// <summary>
    /// Pre-Registrar o Mostrar Pagina de Login Benefit
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!App.BenefitLogin)
            ViewModels.GetUserDataCommand.Execute(LoginBenefit);
    }
    /// <summary>
    /// Password para cuenta Benefit
    /// </summary>
    /// <param name="sender">Objeto con el que se trabaja</param>
    /// <param name="e">Parametros de evento</param>
    void Entry_TextChanged(System.Object sender, TextChangedEventArgs e)
    {
        ViewModels.rules();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GC.SuppressFinalize(this);
    }
}