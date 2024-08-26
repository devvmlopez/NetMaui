using Acr.UserDialogs;
using InntecMobileNetMaui.ViewModels;

namespace InntecMobileNetMaui.Views;

public partial class MyDataPage : ContentPage
{
    private MyDataViewModel _viewModel;
    bool _exit;
    public MyDataPage()
	{
		InitializeComponent();
        this.BindingContext = _viewModel = new MyDataViewModel(this);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _exit = false;
        _viewModel.LoadUserData.Execute(null);
    }
    /// <summary>
    /// Actualizar datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
        _viewModel.IsBusy = true;
        _viewModel.SaveUserData.Execute(null);
    }
    /// <summary>
    /// Regresar a pagina principal
    /// </summary>
    /// <returns></returns>
    protected override bool OnBackButtonPressed()
    {
        //if (!_exit)
        //{
        //    ToastConfig toastConfig = new ToastConfig("Presiona nuevamente para salir");
        //    toastConfig.SetDuration(3000);
        //    toastConfig.SetBackgroundColor(Color.DimGray);
        //    UserDialogs.Instance.Toast(toastConfig);
        //    _exit = true;
        //    return true;
        //}
        //return base.OnBackButtonPressed();  Refactorizar con el control de las ALERTS
        return false;
    }
}