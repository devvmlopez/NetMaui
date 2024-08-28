using Acr.UserDialogs;
//using Android.OS;
//using Android.OS.Strictmode;
//using Android.Telephony.Euicc;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.ViewModels.Cards;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;
namespace InntecMobileNetMaui.Views.CustomView;

public partial class NewCardPage 
{
    public NewCardViewModel viewModel { get; set; }
    public NewCardPage()
	{
		InitializeComponent();
        this.BindingContext = viewModel = new NewCardViewModel(this);
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

    }

    private void Btn_Cancel_Pressed(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }

    private void Btn_NewCard_Clicked(object sender, System.EventArgs e)
    {
        if (IsBusy) return;
        IsBusy = true;

        //AlertConfig alertConfig = new AlertConfig();
        //alertConfig.SetTitle("Se han detectado errores");
        //alertConfig.SetOkText("Aceptar");

        //InformativeViewModel.Instance.MessageType = ;
        InformativeViewModel.Instance.Title = "Se han detectado errores";
        InformativeViewModel.Instance.Message = "La sesión ha terminado, ingresa de nuevo.";
        //MopupService.Instance.PushAsync(InformativeAlert.Instance);
        //Shell.Current.GoToAsync("//LoginPage");


        if (string.IsNullOrEmpty(Txt_CardNumber.Text))
        {
            //alertConfig.SetMessage("Ingresa el número de tarjeta");
            //UserDialogs.Instance.Alert(alertConfig);

            InformativeViewModel.Instance.Message = "Ingresa el número de tarjeta.";
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
        }
        else if (string.IsNullOrEmpty(TxtToken.Text))
        {
            //alertConfig.SetMessage("Ingresa el token de seguridad");
            //UserDialogs.Instance.Alert(alertConfig);

            InformativeViewModel.Instance.Message = "Ingresa el token de seguridad.";
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
        }
        else
        {
            Btn_NewCard.IsEnabled = false;
            viewModel.IsBusy = true;
            viewModel.SaveNewCardCommand.Execute(null);
            //PopupNavigation.Instance.PopAsync(true);
            MopupService.Instance.PopAsync();
        }
        IsBusy = false;
    }
}