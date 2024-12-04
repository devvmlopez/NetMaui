using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Viatics;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.ViewModels.Viatics;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;


namespace InntecMobileNetMaui.Views.Viatics;

public partial class ViaticsRequestPage : ContentPage
{
    internal MainPage RootPage => Application.Current.MainPage as MainPage;
    private ViaticsRequestViewModel viewModel;
    const string ErrColor = "#FFEBEB";
    private bool filtro = false;
    /// <summary>
    /// Inicializar objetos
    /// </summary>
    public ViaticsRequestPage(CardModel cardModel)
    {
        InitializeComponent();

        BindingContext = viewModel = new ViaticsRequestViewModel(this , cardModel);

        MessagingCenter.Unsubscribe<Search.SearchCityPage, InfoCity>(this, App.SelectCity);
        MessagingCenter.Subscribe<Search.SearchCityPage, InfoCity>(this, App.SelectCity, (obj, item) =>
        {
            PkrCity.SelectedIndex = PkrCity.Items.IndexOf(item.Nombre);
            controlListCuidades.Unfocus();
            controlListCuidades.Text = item.Nombre;

        });
    }
    /// <summary>
    /// Formulario para nueva solicitud
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!filtro)
        {
            txtMonto.Text = string.Empty;
            viewModel.IsBusy = true;
            viewModel.DetailViatic?.Clear();
            if (!string.IsNullOrEmpty(viewModel.NewViaticsRequest.Comentarios))
            {
                TxtComments.RemoveBinding(Entry.TextProperty);
                viewModel.NewViaticsRequest.Comentarios = string.Empty;
                TxtComments.SetBinding(Entry.TextProperty, "NewViaticsRequest.Comentarios");
            }
            viewModel.NewRequest.Execute(null);
        }

    }
    /// <summary>
    /// Agregar solicitud de viaticos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Btn_Add_Clicked(object sender, EventArgs e)
    {

        string msg = string.Empty;
        if (string.IsNullOrEmpty(viewModel.NewViaticsRequest.Comentarios))
            {
                msg = msg + "*Introduce un comentario" + Environment.NewLine;
                TxtComments.BackgroundColor = Color.FromHex(ErrColor);
                TxtComments.TextColor = Color.FromRgb(0, 0, 0);
            }
        else 
            {
                TxtComments.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
                TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (PkrItem.SelectedItem == null)
            {
                msg = msg + "*No se ha seleccionado un rubro" + Environment.NewLine;
                PkrItem.BackgroundColor = Color.FromHex(ErrColor);
                PkrItem.TextColor = Color.FromRgb(0, 0, 0);
               
        }
        else 
            {
                PkrItem.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
                PkrItem.SetAppThemeColor(Picker.TextColorProperty, Colors.Black, Colors.White);
        }

        if (PkrCity.SelectedItem == null)
            {
                msg = msg + "*No se ha seleccionado una ciudad" + Environment.NewLine;
                PkrCity.BackgroundColor = Color.FromHex(ErrColor);
                PkrCity.TextColor = Color.FromRgb(0, 0, 0);
                
        }
        else 
            {
                PkrCity.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
                PkrCity.SetAppThemeColor(Picker.TextColorProperty, Colors.Black, Colors.White);

        }
        if (string.IsNullOrEmpty(txtMonto.Text))
            {
                msg = msg + "*No se ha agregado un monto." + Environment.NewLine;
                txtMonto.BackgroundColor = Color.FromHex(ErrColor);
                txtMonto.TextColor = Color.FromRgb(0, 0, 0);
            }
        else 
            {
                txtMonto.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
                txtMonto.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(msg))
        {
            List<object> param = new List<object>();
            param.Add(PkrCity.SelectedItem);
            param.Add(PkrItem.SelectedItem);
            param.Add(txtMonto.Text);
            viewModel.AddItem.Execute(param);
        }

        else
        {
            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
            InformativeViewModel.Instance.Title = "Alerta del sistema";
            InformativeViewModel.Instance.Message = msg;
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
        }  

     

    }
    /// <summary>
    /// Eliminar rubro del detalle
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuDelete_Clicked(object sender, EventArgs e)
    {
        var param = ((MenuItem)sender).CommandParameter;
        viewModel.DeleteItem.Execute(param);
    }



    private async void PkrCity_FocusedAsync(System.Object sender, FocusEventArgs e)
    {
        if (e.IsFocused)
        {
            ((Picker)sender).Unfocus();
            filtro = true;
            await Navigation.PushModalAsync(new InntecMobileNetMaui.Views.Viatics.Search.SearchCityPage(viewModel.InfoNewRequest.InfoCiudades)).ConfigureAwait(true);
        }
    }

    private async void Entry_Focused(object sender, FocusEventArgs e)
    {
        InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
        InformativeViewModel.Instance.Title = "Mensaje";
        InformativeViewModel.Instance.Message = "Por favor, espera un momento mientras se carga el listado de ciudades.";
        await MopupService.Instance.PushAsync(InformativeAlert.Instance);

        if (e.IsFocused)
        {
            filtro = true;
            await Navigation.PushModalAsync(new InntecMobileNetMaui.Views.Viatics.Search.SearchCityPage(viewModel.InfoNewRequest.InfoCiudades)).ConfigureAwait(true);
        }
    }
    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}