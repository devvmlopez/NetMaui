using InntecMobileNetMaui.Models.Viatics;
using InntecMobileNetMaui.ViewModels.Viatics;

namespace InntecMobileNetMaui.Views.Viatics;

public partial class ViaticsCheckPage : ContentPage
{
    private ViaticsCheckViewModel viewModel;
    /// <summary>
    /// Inicializacion de objetos
    /// </summary>
    public ViaticsCheckPage(Models.CardModel args)
    {
        InitializeComponent();
        BindingContext = viewModel = new ViaticsCheckViewModel(this , args);
    }
    /// <summary>
    /// Detalle de consumos
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (viewModel.viaticsChecList.Count == 0)
        {
            viewModel.ViaticsConsumptionDetail.Execute(null);
        }
    }
    /// <summary>
    /// Mostrar detalle de consumos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        viewModel.ShowViaticsCheckDetail.Execute(((Models.Viatics.ConsumptionDetail)e.Item));
    }
    /// <summary>
    /// Filtro de estatus
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        viewModel.FiltrerViaticsConsumptionDetail.Execute(((Picker)sender).SelectedItem);
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        viewModel.ShowViaticsCheckDetail.Execute(e.CurrentSelection.FirstOrDefault() as ConsumptionDetail);
    }
}
