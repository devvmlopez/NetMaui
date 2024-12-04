using Microsoft.Maui.Controls.Compatibility;

namespace InntecMobileNetMaui.Views.BenefitHub;

public partial class BenefitHubTemsPage : ContentPage
{
    public BenefitHubTemsPage()
    {
        InitializeComponent();
    }
    /// <summary>
    /// Mostrar terminos y condiciones
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Title = "Terminos y Condiciones";
        Terms.Source = "https://inntecdescuentos.benefithub.com/ContentV2/TermsAndConditions/180913_Website_Terms&ConditionsESMX.pdf";
    }
    
}