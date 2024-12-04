using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.CustomView.Validador;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Mopups.Services;

namespace InntecMobileNetMaui.Views;

public partial class RecoverPasswordPage : ContentPage
{
    private RecoverPasswordViewModel viewModel;
    /// <summary>
    /// Inicializar objetos
    /// </summary>
    public RecoverPasswordPage()
    {
        InitializeComponent();
        this.BindingContext = viewModel = new RecoverPasswordViewModel(this);
    }

    private async void btnRecuperar_Clicked(object sender, System.EventArgs e)
    {
        viewModel.IsBusy = true;

        var popupValidador = new CustomValidator();
        InformativeViewModel.Instance.Title = "Mensaje";
        InformativeViewModel.Instance.Message = "Siga las instrucciones para continuar.";
        await MopupService.Instance.PushAsync(popupValidador);
        var captchaToken = await popupValidador.PopupDismissedTask;

        if (captchaToken == null)
        return;
        await viewModel.SendRecorveryAsync(captchaToken).ConfigureAwait(true); 
        //SendRecorveryAsync se le agrego una validacion para que no manden campos vacios , solo queda la refactorizacion del Recapchat

    }

    void Btn_Cancel_Clicked(System.Object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//Login");
    }
}