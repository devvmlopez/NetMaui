using Acr.UserDialogs;
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
        Btn_NewCard.Clicked -= Btn_NewCard_Clicked;
        Btn_NewCard.Clicked += new System.EventHandler(Btn_NewCard_Clicked);


        Btn_Cancel.Clicked -= Btn_Cancel_Pressed;
        Btn_Cancel.Clicked += new System.EventHandler(Btn_Cancel_Pressed);
    }

    /// <summary>
    /// Evento que controla la navegacion hacia atras al cerrar el control
    /// </summary>
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
    /// <summary>
    /// Agregar una nueva tarjeta
    /// </summary>
    private async void Btn_NewCard_Clicked(object sender, System.EventArgs e)
    {
        if (IsBusy) return;
        IsBusy = true;

        if (string.IsNullOrEmpty(Txt_CardNumber.Text))
        {

            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
            InformativeViewModel.Instance.Title = "Se han detectado errores";
            InformativeViewModel.Instance.Message = "Ingresa el número de tarjeta.";
            await MopupService.Instance.PushAsync(InformativeAlert.Instance);
        }
        else if (string.IsNullOrEmpty(TxtToken.Text))
        {

            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
            InformativeViewModel.Instance.Title = "Se han detectado errores";
            InformativeViewModel.Instance.Message = "Ingresa el token de seguridad.";
            await MopupService.Instance.PushAsync(InformativeAlert.Instance);
        }
        else
        {
            Btn_NewCard.IsEnabled = false;
            viewModel.IsBusy = true;
            viewModel.SaveNewCardCommand.Execute(null);
            viewModel.IsBusy = false;
            Shell.Current.GoToAsync("//CardsPage");
        }
        MopupService.Instance.PopAsync();
        IsBusy = false;
        
    }
}