using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using InntecMobileNetMaui.Views.Login;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels
{
    /// <summary>
    /// ViewModel de la pagina principal
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public Command StartQrReader;
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
               // App.Current.MainPage.DisplayAlert("Error", Constants.Error_Descipcion, "Aceptar");

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Error";
                InformativeViewModel.Instance.Message = Constants.Error_Descipcion;
                MopupService.Instance.PushAsync(InformativeAlert.Instance);

                return false;
            }
            int result = DateTime.Compare(Constants.Token_Expires, DateTime.Now);

            if (result < 0)
            {
                //PopupNavigation.Instance.PopAllAsync();
                //App.Current.MainPage.DisplayAlert("Sesión", "La sesión ha terminado, ingresa de nuevo.", "Aceptar");

                //App.Current.MainPage = new LoginPage();

                //Mensajes de error generados por la session 
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Sesión";
                InformativeViewModel.Instance.Message = "La sesión ha terminado, ingresa de nuevo.";
                MopupService.Instance.PushAsync(InformativeAlert.Instance);
                Shell.Current.GoToAsync("//LoginPage");


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
            //_mainPage = mainPage;
            //StartQrReader = new Command(async (args) => await ExecuteStartQrReader((CardModel)args).ConfigureAwait(true));
            //UnlinkUser = new Command(async (args) => await ExecuteUnlinkUser((CardModel)args).ConfigureAwait(true));

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
            //if (await DataGas.UnLinkedUser().ConfigureAwait(true))
            //{
            //    await _mainPage.DisplayAlert("Mensaje", "Se a quitado el QR correctamente.", "Aceptar").ConfigureAwait(true);
            //    args.Complemento.QrId = 0;
            //    return true;
            //}
            //else
            //{
            //    await _mainPage.DisplayAlert("Alerta!", "Ocurrio un error al eliminar el QR, intenta mas tarde.", "Aceptar").ConfigureAwait(true);
            //    return false;
            //}
            //FALTA implementar logica de las tarjetas de gas 
            return false;

        }
        /// <summary>
        /// Inicializar lectura de QR
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        async Task ExecuteStartQrReader(CardModel args)
        {
            //var options = new MobileBarcodeScanningOptions();
            //options.PossibleFormats = new List<BarcodeFormat>
            //{
            //    BarcodeFormat.QR_CODE,
            //};
            //page = new ZXingScannerPage(options) { Title = "Scanner" };
            //var closeItem = new ToolbarItem { Text = "Cerrar" };
            //closeItem.Clicked += new EventHandler(CloseItem_Clicked);
            //page.ToolbarItems.Add(closeItem);
            //page.OnScanResult += (result) =>
            //{
            //    page.IsScanning = false;

            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await Application.Current.MainPage.Navigation.PopModalAsync().ConfigureAwait(true);
            //        if (string.IsNullOrEmpty(result.Text))
            //        {
            //            await _mainPage.DisplayAlert("Alerta!", "QR no valido", "Aceptar").ConfigureAwait(true);
            //        }
            //        else
            //        {
            //            Guid guid;
            //            _ = Guid.TryParse(result.Text, out guid);
            //            if (!guid.ToString().Equals("00000000-0000-0000-0000-000000000000"))
            //            {
            //                if (await DataGas.LinkedUser(guid).ConfigureAwait(true))
            //                {
            //                    await _mainPage.DisplayAlert("Mensaje", "QR enlazado correctamente", "Aceptar").ConfigureAwait(true);
            //                    args.Complemento.QrId = -1;
            //                }
            //                else
            //                {
            //                    await _mainPage.DisplayAlert("Alerta!", "Este QR ya esta en uso o ya fue cancelado", "Aceptar").ConfigureAwait(true);
            //                }

            //            }
            //            else
            //            {
            //                await _mainPage.DisplayAlert("Alerta!", "QR no valido", "Aceptar").ConfigureAwait(true);
            //            }
            //            CloseItem_Clicked(page, new EventArgs());
            //        }
            //    });
            //};
            //await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(page) { BarTextColor = Color.White, BarBackgroundColor = Color.FromHex("#3487CB") }, true).ConfigureAwait(true);
            // FALTA implementar logica para los codigos de barra en NET MAUI
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
