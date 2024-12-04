using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Viatics;
using InntecMobileNetMaui.ViewModels.Viatics;
using Org.BouncyCastle.Asn1.Cmp;

namespace InntecMobileNetMaui.Views.Viatics;

public partial class ViaticsListPage : ContentPage
{
    private ViaticsListViewModel viewModel;
    /// <summary>
    /// Inicializacion de objetos
    /// </summary>
    public ViaticsListPage(Models.CardModel args)
    {
        InitializeComponent();
        BindingContext = viewModel = new ViaticsListViewModel(this , args);
    }
    /// <summary>
    /// Mostrar listado de solicitudes
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (viewModel.ListViaticsRequest.Count == 0)
        {
            viewModel.LoadViaticsRequestListRequest.Execute(null);
            PkrStatus.SelectedIndex = 0;
        }

    }
    /// <summary>
    /// Mostrar detalles de la solicitud
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        viewModel.ShowViaticsDetail.Execute(((Models.Viatics.ViaticsRequest)e.Item).ListDetalleSolicitud);
    }
    /// <summary>
    /// Filtrado por estatus
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PkrStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    private void BtnBuscar_Clicked(object sender, EventArgs e)
    {
        string Categoria = Convert.ToString(PkrStatus.SelectedItem);
        viewModel.DetailViaticsRequestListRequest.Execute(Categoria);
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        viewModel.ShowViaticsDetail.Execute((e.CurrentSelection.FirstOrDefault() as ViaticsRequest).ListDetalleSolicitud);
    }
}