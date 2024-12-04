using Acr.UserDialogs;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Pages;
using Mopups.Services;

namespace InntecMobileNetMaui.Views.CustomView.Card;

public partial class CardNipPage 
{
        Models.CardModel cardModel;
        public CardNipViewModel viewModel { get; set; }

        /// <summary>
        /// Inicializacion de NIP
        /// </summary>
        /// <param name="cardModel">Datos de tarjeta</param>
        public CardNipPage(Models.CardModel cardModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new CardNipViewModel(cardModel, this);
            this.cardModel = cardModel;

            Btn_ChangeNip.Clicked -= Btn_ChangeNip_Clicked;
            Btn_ChangeNip.Clicked += new System.EventHandler(Btn_ChangeNip_Clicked);

            Btn_Cancel.Clicked -= Btn_Cancel_Clicked;
            Btn_Cancel.Clicked += new System.EventHandler(Btn_Cancel_Clicked);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (this.cardModel.PuedeCambiarNip != null)
            {
                if (!(bool)this.cardModel.PuedeCambiarNip)
                {
                    Btn_ChangeNip.IsEnabled = false;

                    await MopupService.Instance.PopAsync(true);

                    //var alertConfig = new AlertConfig();
                    //alertConfig.SetTitle("No disponible");
                    //alertConfig.SetMessage("Esta opción no está disponible para la tarjeta.");
                    //alertConfig.SetOkText("Regresar");

                    //UserDialogs.Instance.Alert(alertConfig);


                    InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "No disponible";
                    InformativeViewModel.Instance.Message = "Esta opción no está disponible para la tarjeta.";
                    MopupService.Instance.PushAsync(InformativeAlert.Instance);
            }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<CardNipPage, CardNipModel>(this, "Cambiar NIP");
        }


        /// <summary>
        /// Cancelar cambio de NIP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Btn_Cancel_Clicked(object sender, System.EventArgs e)
        {
            await MopupService.Instance.PopAsync(true);
        }

        /// <summary>
        /// Solicitar cambio de NIP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ChangeNip_Clicked(object sender, System.EventArgs e)
        {
            if (IsBusy) return;
            IsBusy = true;

            var alertConfig = new AlertConfig();
            alertConfig.SetTitle("Se han detectado errores");
            alertConfig.SetOkText("Aceptar");


        InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
        InformativeViewModel.Instance.Title = "Se han detectado errores";


        var digitosConsecutivos = new[]
            {
                "012","123","234","345","456","567","678","789","890","901",
                "098","987","876","765","654","543","432","321","210","109",
                "000","111","222","333","444","555","666","777","888","999",
                "5064", "5359", "5286",
            };

            if (string.IsNullOrEmpty(viewModel.NipConfirm) || string.IsNullOrEmpty(viewModel.CardNipModel.Nip))
            {
                //alertConfig.SetMessage("Ingresa el nuevo NIP y su confirmación.");
                //UserDialogs.Instance.Alert(alertConfig); Refactorizar cuando este activa esta parte
                InformativeViewModel.Instance.Message = "Ingresa el nuevo NIP y su confirmación.";
                MopupService.Instance.PushAsync(InformativeAlert.Instance);
        }
            else if (viewModel.NipConfirm != viewModel.CardNipModel.Nip)
            {
                //alertConfig.SetMessage("La confirmación debe ser es igual al NIP.");
                //UserDialogs.Instance.Alert(alertConfig);   Refactorizar cuando este activa esta parte
                InformativeViewModel.Instance.Message = "La confirmación debe ser es igual al NIP.";
                MopupService.Instance.PushAsync(InformativeAlert.Instance);
            }
            else if (!viewModel.CardNipModel.Nip.All(char.IsDigit) || viewModel.CardNipModel.Nip.Length != 4)
            {
                //alertConfig.SetMessage("El valor para el campo NIP debe ser de 4 dígitos.");
                //UserDialogs.Instance.Alert(alertConfig);    Refactorizar cuando este activa esta parte
                InformativeViewModel.Instance.Message = "El valor para el campo NIP debe ser de 4 dígitos.";
                MopupService.Instance.PushAsync(InformativeAlert.Instance);
        }
            else if (digitosConsecutivos.Any(dc => viewModel.CardNipModel.Nip.Contains(dc)))
            {
                //alertConfig.SetMessage("El NIP no puede contener 3 o más números consecutivos o el mismo número repetido más de 3 veces.");
                //UserDialogs.Instance.Alert(alertConfig);   Refactorizar cuando este activa esta parte
                InformativeViewModel.Instance.Message = "El NIP no puede contener 3 o más números consecutivos o el mismo número repetido más de 3 veces.";
                MopupService.Instance.PushAsync(InformativeAlert.Instance);
        }
            else
            {
                Btn_ChangeNip.IsEnabled = false;
                viewModel.IsBusy = true;
                viewModel.ChangeNipCardCommand.Execute(null);
                MopupService.Instance.PopAsync(true);
            }

            IsBusy = false;
        }
}