using InntecMobileNetMaui.ViewModels.Viatics;

namespace InntecMobileNetMaui.Views.Viatics;

public partial class MainViaticsPage : ContentPage
{
    private MainViaticsViewModel viewModel;
    /// <summary>
    /// Inicializacion de objetos
    /// </summary>
    /// <param name="mainViewModel">Datos de pagina principal</param>
    public MainViaticsPage(ref ViewModels.MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = viewModel = new MainViaticsViewModel(this);
    }
    /// <summary>
    /// Listado de tarjetas
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (viewModel.Cards.Count == 0)
        {
            viewModel.LoadCardsCommand.Execute(null);
        }
    }
    /// <summary>
    /// Llamar al recolector de basura al salir del Layout
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GC.Collect();
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}
