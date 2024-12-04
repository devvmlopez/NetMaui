using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.ViewModels.Cards;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using Mopups.Pages;
using Mopups.Services;

namespace InntecMobileNetMaui.Views.Alerts;

public partial class MoreOptionsPage : PopupPage
{
    MoreOptionsViewModel viewModel;
    Models.CardModel cardModel;
    bool firstTimeSw1 = false;
    bool firstTimeSw2 = false;
    bool temporalBool = false;
    bool returnvalue = false;
    public MoreOptionsPage(CardModel cardModel, bool PrimeraVez)
    {
        InitializeComponent();
        
        this.BindingContext = viewModel = new MoreOptionsViewModel( this, cardModel);
        this.cardModel = cardModel;
        temporalBool = PrimeraVez;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        bool mostrarEcommerce = true;

        if (this.cardModel.PuedeCambiarEcommerce != null)
            mostrarEcommerce = Convert.ToBoolean(this.cardModel.PuedeCambiarEcommerce);

        Stk_ecommerce.IsVisible = mostrarEcommerce;

        firstTimeSw1 = temporalBool;
        firstTimeSw2 = temporalBool;
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        IsBusy = true;
        await Navigation.PushAsync(new CardsPage());//.ConfigureAwait(true);
        IsBusy = false;
    }
 
    private async void Button_Pressed(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }

   
    private async void SwitchInntec_Toggled(object sender, ToggledEventArgs e)
    {
       

        if (firstTimeSw1 == false)
        {
            if (IsBusy)
                return;
        }
        else
        {
            try
            {
                IsBusy = true;

                string mensaje = String.Empty;

                if (!returnvalue)
                {
                    mensaje = e.Value ? "Estas seguro que quieres ACTIVAR la tarjeta?" : "Estas seguro que quieres DESACTIVAR la tarjeta?";

                    var popup = new YesOrNotAlert();
                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = mensaje;

                    await MopupService.Instance.PushAsync(popup);

                    var rvalue = await popup.PopupDismissedTask;

                    if (rvalue == "YES")
                    {
                        viewModel.BlockCardCommand.Execute(null);
                        
                    }
                    else
                    {
                        returnvalue = true;

                        sw_bloqueotmp.IsToggled = !e.Value;  // Refactorizar
                        return;
                    }

                }
                IsBusy = false;
                returnvalue = false;
            }
            catch
            {
                sw_bloqueotmp.IsToggled = !e.Value; // Refactorizar
                returnvalue = false;
            }
        }
        firstTimeSw1 = true;
    }

    private async void SwitchInntec_BlockEcommerce_Toggled(object sender, ToggledEventArgs e)
    {
        //if (IsBusy) return;

        //IsBusy = true;

        if (firstTimeSw2 == false)
        {
            
        }
        else
        {
            try
            {
                IsBusy = true;

                string mensaje = String.Empty;

                if (!returnvalue)
                {
                    mensaje = e.Value ? "Estas seguro de ACTIVAR las compras en línea." : "Estas seguro de DESACTIVAR las compras en línea.";

                    var popup = new YesOrNotAlert();
                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = mensaje;

                    await MopupService.Instance.PushAsync(popup);

                    var rvalue = await popup.PopupDismissedTask;

                    if (rvalue == "YES")
                    {
                        viewModel.BlockEcommerceCommand.Execute(null);
                    }
                    else
                    {
                        returnvalue = true;
                        sw_bloqueoecom.IsToggled = !e.Value; // Refactorizar
                        return;
                    }

                }
                IsBusy = false;
                returnvalue = false;
            }
            catch
            {
                IsBusy = false;
                sw_bloqueoecom.IsToggled = !e.Value; // Refactorizar
                returnvalue = false;
            }
        }
        firstTimeSw2 = true;
    }

}