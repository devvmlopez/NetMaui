using Microsoft.Maui.Devices;

namespace InntecMobileNetMaui.Views;

public partial class TermsAndConditionsPage : ContentPage
{
    bool navegacionAtras;
    /// <summary>
    /// Inicializar objetos
    /// </summary>
    /// <param name="userModel">Datos de usuario</param>
    public TermsAndConditionsPage(bool navegacion)
    {
        navegacionAtras = navegacion;
        InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //BindingContext = this;
        this.BindingContext = this;
    }

    private void Btn_Cancel_Clicked(object sender, EventArgs e)
    {
        if (navegacionAtras == false)
        {
            Navigation.PopModalAsync();
        }
        else
            Navigation.PopAsync();
    }
}