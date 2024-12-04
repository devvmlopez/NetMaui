using InntecMobileNetMaui.Models.Viatics;
using InntecMobileNetMaui.ViewModels.Viatics.Search;


namespace InntecMobileNetMaui.Views.Viatics.Search;

public partial class SearchCityPage 
{

    public SearchCityViewModel viewModel { get; private set; }

    public SearchCityPage(List<InfoCity> infoCiudades)
    {
        InitializeComponent();
        BindingContext = viewModel = new SearchCityViewModel(infoCiudades);
    }

    protected override void OnAppearing()
    {

        base.OnAppearing();
        viewModel.FillListCommand.Execute(null);
    }

    void Entry_TextChanged(System.Object sender, TextChangedEventArgs e)
    {
        viewModel.FillListCommand.Execute(null);
    }

    void ListView_ItemSelected(System.Object sender, SelectedItemChangedEventArgs e)
    {
        MessagingCenter.Send(this, App.SelectCity, (InfoCity)e.SelectedItem);
        Navigation.PopModalAsync();
    }

    void BtnCancelar_Pressed(System.Object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}