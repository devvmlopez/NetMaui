using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Gas;
using InntecMobileNetMaui.ViewModels.Gas;

namespace InntecMobileNetMaui.Views.Gas;

public partial class ActivationRequestReportPage : ContentPage
{
    private ActivationRequestReportViewModel viewModel;
    /// <summary>
    /// Inicializar objetos
    /// </summary>
    /// <param name="args">Datos de la tarjeta</param>
    public ActivationRequestReportPage(Models.CardModel args)
    {
        InitializeComponent();
        IsBusy = false;
        BindingContext = viewModel = new ActivationRequestReportViewModel(this, args);
    }

    /// <summary>
    /// Cargar datos de detalle
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (viewModel.RequestList.Count == 0)
        {
            viewModel.LoadRequestReportCommand.Execute(null);
        }
    }
    /// <summary>
    /// Solicitar detalle de solicitudes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        viewModel.ShowActivationRequestDetailsCommand.Execute(((Models.Gas.ActivationRequestReportModel)e.Item));
    }
    /// <summary>
    /// Filtro por estatus
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PkrStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker data = (Picker)sender;
        viewModel.FilterModel.statusId = (int)(Enumeradores.enumSolicitudActivacionEstatus)Enum.Parse(typeof(Enumeradores.enumSolicitudActivacionEstatus), data.SelectedItem.ToString());
    }
    /// <summary>
    /// Cargar reportes filtrados.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
        viewModel.LoadRequestReportCommand.Execute(null);
    }
    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //var item = e.CurrentSelection.FirstOrDefault() as ActivationRequestReportModel;
        viewModel.ShowActivationRequestDetailsCommand.Execute(e.CurrentSelection.FirstOrDefault() as ActivationRequestReportModel);
    }
}