using InntecMobileNetMaui.Models.Viatics;
using InntecMobileNetMaui.ViewModels.Viatics;

namespace InntecMobileNetMaui.Views.Viatics;

public partial class ViaticDetailPage : ContentPage
{
    private ViaticDetailViewModel viewModel;
    /// <summary>
    /// Inicializar objetos
    /// </summary>
    /// <param name="Detail">Listado de detalles</param>
    public ViaticDetailPage(List<DetailsViaticsRequest> Detail)
    {
        InitializeComponent();
        BindingContext = viewModel = new ViaticDetailViewModel(Detail);
    }
    /// <summary>
    /// Muestra de detalles
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.LoadViaticsDetail.Execute(null);
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}