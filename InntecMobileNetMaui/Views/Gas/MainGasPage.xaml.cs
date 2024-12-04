using InntecMobileNetMaui.ViewModels.Gas;

namespace InntecMobileNetMaui.Views.Gas;

public partial class MainGasPage : ContentPage
{

    private MainGasViewModel viewModel;
    /// <summary>
    /// Inicializacion de objetos
    /// </summary>
    /// <param name="mainViewModel">Datos de la pagina principal</param>
    public MainGasPage(ref ViewModels.MainViewModel mainViewModel)
    {
        InitializeComponent();

        BindingContext = viewModel = new MainGasViewModel(this);
    }
    /// <summary>
    /// Carga de tarjetas
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (viewModel.Cards.Count == 0)
        {
            viewModel.LoadCardsCommand.Execute(null);
        }
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}