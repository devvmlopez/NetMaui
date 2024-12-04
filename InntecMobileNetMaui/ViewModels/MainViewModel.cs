using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using InntecMobileNetMaui.Views.Login;
using InntecMobileNetMaui.Views.QR;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace InntecMobileNetMaui.ViewModels
{
    /// <summary>
    /// ViewModel de la pagina principal
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public Command StartQrReaderV2 { get; set; }
        public Command UnlinkUser;
       // private ZXingScannerPage page;
        private CardsPage _mainPage { get; set; }
        /// <summary>
        /// Verificar TOKEN de sesion activa
        /// </summary>
        public bool VerifyToken()
        {
            if (!string.IsNullOrEmpty(Constants.Error_Descipcion))
            {

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Error";
                InformativeViewModel.Instance.Message = Constants.Error_Descipcion;
                MopupService.Instance.PushAsync(InformativeAlert.Instance);

                return false;
            }
            int result = DateTime.Compare(Constants.Token_Expires, DateTime.Now);

            if (result < 0)
            {

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Sesión";
                InformativeViewModel.Instance.Message = "La sesión ha terminado, ingresa de nuevo.";
                MopupService.Instance.PushAsync(InformativeAlert.Instance);
                Shell.Current.GoToAsync("//Login");


                return false;
            }
            return true;
        }
        /// <summary>
        /// Inicializar objetos
        /// </summary>
        /// <param name="mainPage">Pagina de binding</param>
        public MainViewModel(CardsPage mainPage)
        {
            _mainPage = mainPage;
            UnlinkUser = new Command(async (args) => await ExecuteUnlinkUser((CardModel)args).ConfigureAwait(true));
            StartQrReaderV2 = new Command(async (args) => await ExecuteStartQrReaderV2((CardModel)args).ConfigureAwait(true));

        }
        public MainViewModel()
        {

        }

        /// <summary>
        /// Des Enlazar QR a Tarjeta
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        async Task<bool> ExecuteUnlinkUser(CardModel args)
        {
           

            if (await DataGas.UnLinkedUser().ConfigureAwait(true))
            {

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = "Se a quitado el QR correctamente.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                args.Complemento.QrId = 0;
                return true;
            }
            else
            {

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "Ocurrio un error al eliminar el QR, intenta mas tarde.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                return false;
            }

        }
      
        /// <summary>
        /// Inicializar lectura de QR Refactorizada por NetMaui
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        async Task ExecuteStartQrReaderV2(CardModel args)
        {
            var popup = new ReaderQR();
            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Mensaje";
            InformativeViewModel.Instance.Message = "Escanea tu codigo QR";

            await MopupService.Instance.PushAsync(popup);

            var rvalue = await popup.PopupDismissedTask;

            if (string.IsNullOrEmpty(rvalue))
            {
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = " " + rvalue + " QR no valido ";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);

            }

            if (rvalue != "Cancelar")
            {
                Guid guid;
                _ = Guid.TryParse(rvalue, out guid);
                if (!guid.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    if (await DataGas.LinkedUser(guid).ConfigureAwait(true))
                    {

                        InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                        InformativeViewModel.Instance.Title = "Mensaje";
                        InformativeViewModel.Instance.Message = "QR enlazado correctamente.";
                        await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                        args.Complemento.QrId = -1;
                    }
                    else
                    {

                        InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                        InformativeViewModel.Instance.Title = "Alerta!";
                        InformativeViewModel.Instance.Message = "Este QR ya esta en uso o ya fue cancelado.";
                        await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    }

                }
                else
                {

                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                    InformativeViewModel.Instance.Title = "Alerta!";
                    InformativeViewModel.Instance.Message = "QR no valido.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
       
            }
            else 
            {
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = "Proceso cancelado.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
            }

        }
            /// <summary>
            /// Regreso a pagina principal
            /// </summary>
            /// <param name="sender">Objeto con el que se trabaja</param>
            /// <param name="e">Paramtros del evento</param>
            private void CloseItem_Clicked(object sender, EventArgs e)
        {
            //page.IsScanning = false;
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    Application.Current.MainPage.Navigation.PopModalAsync();
            //});
            // FALTA implementar logica para los codigos de barras en MAUI
        }
    }
}
