using InntecMobileNetMaui.ViewModels.Gas;
namespace InntecMobileNetMaui.Views.Gas;

public partial class ActivateCardPage : ContentPage
{
    public ActivateCardViewModel viewModel { get; private set; }
    private Models.CardModel cardModel;
    public bool Exit { get; set; }


    private bool activateLts;
    public bool ActivateLts { get => activateLts; set => activateLts = value; }
    /// <summary>
    /// Inicializacion de objetos
    /// </summary>
    /// <param name="args">Datos de tarjeta</param>
    public ActivateCardPage(Models.CardModel args)
    {
        InitializeComponent();
        Exit = false;
        cardModel = args;
        this.BindingContext = viewModel = new ActivateCardViewModel(this, cardModel);
    }
    /// <summary>
    /// Validacion de campo de Precio y Litros
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtPrecio_Unfocused(object sender, FocusEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtPrecio.Text) && !string.IsNullOrEmpty(TxtLitros.Text) && Convert.ToDecimal(TxtPrecio.Text) > 0 && Convert.ToDecimal(TxtLitros.Text) > 0)
        {
            if (!TxtCantidad.IsEnabled && Convert.ToDecimal(TxtPrecio.Text) > 0 && Convert.ToDecimal(TxtLitros.Text) > 0)
            {
                TxtLitros_Unfocused(TxtLitros, e);
            }
            else if (Convert.ToDecimal(TxtPrecio.Text) > 0 && Convert.ToDecimal(TxtLitros.Text) > 0)
            {
                if (!TxtLitros.IsEnabled)
                    TxtCantidad_Unfocused(TxtCantidad, e);
                else
                    TxtLitros_Unfocused(TxtLitros, e);
            }

        }
        else if (!string.IsNullOrEmpty(TxtPrecio.Text) && !string.IsNullOrEmpty(TxtCantidad.Text) && Convert.ToDecimal(TxtCantidad.Text) > 0 && Convert.ToDecimal(TxtPrecio.Text) > 0)
        {
            if (!TxtLitros.IsEnabled && Convert.ToDecimal(TxtCantidad.Text) > 0 && Convert.ToDecimal(TxtPrecio.Text) > 0)
            {
                TxtCantidad_Unfocused(TxtCantidad, e);
            }
            else if (Convert.ToDecimal(TxtCantidad.Text) > 0 && Convert.ToDecimal(TxtPrecio.Text) > 0)
            {
                if (!TxtCantidad.IsEnabled)
                    TxtLitros_Unfocused(TxtLitros, e);
                else
                    TxtCantidad_Unfocused(TxtCantidad, e);
            }
        }
        else
        {
            TxtCantidad.IsEnabled = true;
            TxtLitros.IsEnabled = true;
        }
    }
    /// <summary>
    /// Validacion de campo de Precio y Litros
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtLitros_Unfocused(object sender, FocusEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtPrecio.Text) && !string.IsNullOrEmpty(TxtLitros.Text))
        {
            if (Convert.ToDecimal(TxtPrecio.Text) > 0 && Convert.ToDecimal(TxtLitros.Text) > 0)
            {
                if (!TxtLitros.IsEnabled)
                    return;
                TxtCantidad.Text = (Convert.ToDecimal(TxtPrecio.Text) * Convert.ToDecimal(TxtLitros.Text)).ToString();
                TxtCantidad.IsEnabled = false;
            }
            else
                TxtCantidad.IsEnabled = true;
        }
        else
            TxtCantidad.IsEnabled = true;
    }
    /// <summary>
    /// Validacion de campo de Precio y Litros (Formato de numero)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TxtCantidad_Unfocused(object sender, FocusEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtPrecio.Text) && !string.IsNullOrEmpty(TxtCantidad.Text))
        {
            if (Convert.ToDecimal(TxtPrecio.Text) > 0 && Convert.ToDecimal(TxtCantidad.Text) > 0)
            {
                if (!TxtCantidad.IsEnabled)
                    return;
                TxtLitros.Text = (Convert.ToDecimal(TxtCantidad.Text) / Convert.ToDecimal(TxtPrecio.Text)).ToString("F2");
                TxtLitros.IsEnabled = false;
            }
            else
                TxtLitros.IsEnabled = true;
        }
        else
            TxtLitros.IsEnabled = true;
    }
    /// <summary>
    /// validacion de QR y vista para solicitar activacion
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        viewModel.Busy = true;
        viewModel.Visible = false;
        if (await viewModel.CheckRestrictions().ConfigureAwait(true) && viewModel.QR == 1)
        {
            viewModel.Busy = false;
            viewModel.Visible = true;
            if (!viewModel.IsBusy && viewModel.QR == 1)
            {
                viewModel.QR = 2;
                viewModel.ActivateQrReader.Execute(null);
            }
        }
        else
        {
            viewModel.QR = 2;
            viewModel.Busy = false;
            viewModel.Visible = true;
        }
    }
    /// <summary>
    /// Llamar al recolector de basura al salir del Layout
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Activar tarjeta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
        viewModel.ActivateCard.Execute(null);
    }
    /// <summary>
    /// Regresar a pantalla principal
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PopModalAsync();
    }
    /// <summary>
    /// Inabilitar campo Cantidad/Litros
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void TxtPrecio_TextChanged(System.Object sender, TextChangedEventArgs e)
    {
        TxtCantidad.IsEnabled = TxtLitros.IsEnabled = true;

        if ((!String.IsNullOrEmpty(TxtCantidad.Text) && Convert.ToDecimal(TxtCantidad.Text) > 0) &&
           (!String.IsNullOrEmpty(TxtPrecio.Text) && Convert.ToDecimal(TxtPrecio.Text) > 0))
        {
            TxtLitros.IsEnabled = false;
        }
        else if ((!String.IsNullOrEmpty(TxtLitros.Text) && Convert.ToDecimal(TxtLitros.Text) > 0) &&
           (!String.IsNullOrEmpty(TxtPrecio.Text) && Convert.ToDecimal(TxtPrecio.Text) > 0))
        {
            TxtCantidad.IsEnabled = false;
        }
    }
    /// <summary>
    /// Inabilitar Campo Litros
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void TxtCantidad_TextChanged(System.Object sender, TextChangedEventArgs e)
    {
        TxtCantidad.IsEnabled = TxtLitros.IsEnabled = true;

        if ((!String.IsNullOrEmpty(TxtCantidad.Text) && Convert.ToDecimal(TxtCantidad.Text) > 0) &&
           (!String.IsNullOrEmpty(TxtPrecio.Text) && Convert.ToDecimal(TxtPrecio.Text) > 0))
        {
            TxtLitros.IsEnabled = false;
        }
    }
    /// <summary>
    /// Inabilitar campo Cantidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void TxtLitros_TextChanged(System.Object sender, TextChangedEventArgs e)
    {
        TxtCantidad.IsEnabled = TxtLitros.IsEnabled = true;

        if ((!String.IsNullOrEmpty(TxtLitros.Text) && Convert.ToDecimal(TxtLitros.Text) > 0) &&
           (!String.IsNullOrEmpty(TxtPrecio.Text) && Convert.ToDecimal(TxtPrecio.Text) > 0))
        {
            TxtCantidad.IsEnabled = false;
        }
    }
    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}
