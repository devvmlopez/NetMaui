using InntecMobileNetMaui.Location;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Gas;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Gas;
using InntecMobileNetMaui.Views.QR;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace InntecMobileNetMaui.ViewModels.Gas
{
    /// <summary>
    /// Activacion de tardejas de combustibles
    /// </summary>
    public class ActivateCardViewModel : BaseViewModel
    {
        #region Variables y propiedades
        MainPage RootPage => Application.Current.MainPage as MainPage;

        private const string Formulario = "FomularioSolicitud";

        //ZXingScannerPage page; //Cambio por Net Maui
        private bool busy;
        double lat;
        double lng;
        List<string> restrict;
        public bool Busy { get => busy; set => SetProperty(ref busy, value); }

        private bool visible;
        public bool Visible { get => visible; set => SetProperty(ref visible, value); }

        private Models.Gas.ActivationRequestModel _requestActivation;
        public Models.Gas.ActivationRequestModel requestActivation { get => _requestActivation; set => SetProperty(ref _requestActivation, value); }

        private CardModel cardModel;

        private string noCard;
        public string NoCard { get => noCard; set => SetProperty(ref noCard, value); }

        public int CsmId { get; set; }
        private decimal LastKM;

        public Command ActivateQrReader { get; internal set; }
        public Command ActivateCard { get; internal set; }
        public int QR;

        private BalanceModel _cardBalanceResult = null;
        public BalanceModel CardBalanceResult
        {
            get => _cardBalanceResult; set => SetProperty(ref _cardBalanceResult, value);
        }

        #endregion

        ActivateCardPage _activateCardPage;
        ILocation location;
        private ResponseActivationRequest _resultActivation;
        public ResponseActivationRequest resultActivation { get => _resultActivation; set => SetProperty(ref _resultActivation, value); }
        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="activateCardPage">Objeto para el binding</param>
        /// <param name="cardModel">Datos de la tarejta</param>
        public ActivateCardViewModel(ActivateCardPage activateCardPage, CardModel cardModel)
        {
            Title = "Activacion para combustible";
            _activateCardPage = activateCardPage;
            QR = 1;
            requestActivation = new Models.Gas.ActivationRequestModel();
            this.cardModel = cardModel;
            NoCard = cardModel.Tarjeta;
            CsmId = cardModel.UsuarioCsmTarjetaId;
            CardBalanceResult = new BalanceModel();



            ActivateQrReader = new Command(async (args) => await ExecuteActivateQrReader((CardModel)args).ConfigureAwait(true));
            ActivateCard = new Command(async () => await ExecuteActivateCard().ConfigureAwait(true));

            Command KMCommand = new Command(async () => await ExecutKMCommand().ConfigureAwait(true));
            KMCommand.Execute(null);

        }
        /// <summary>
        /// Verificar el kilometraje anterior
        /// </summary>
        /// <returns></returns>
        private async Task ExecutKMCommand()
        {
            LastKM = await DataGas.GetKM(cardModel.Complemento.TarjetaId).ConfigureAwait(true);
        }
        /// <summary>
        /// Activar tarjeta
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteActivateCard()
        {
            requestActivation.GpsLat = lat;
            requestActivation.GpsLng = lng;
            requestActivation.CsmUsuarioId = CsmId;
            requestActivation.EscaneoQR = 1;
            Busy = true;
            Visible = false;

            if (LastKM == -1)
            {
              //  _ = _activateCardPage.DisplayAlert("Alerta!", "Error al verificar el Kilometraje.", "Aceptar");
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "Error al verificar el Kilometraje.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                Busy = false;
                Visible = true;
            }
            else
            if (LastKM >= requestActivation.KM && restrict.Any(item => item == Formulario))
            {
               // _ = _activateCardPage.DisplayAlert("Alerta!", "Tu kilometraje no puede ser menor al anterior", "Aceptar");
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "Tu kilometraje no puede ser menor al anterior.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                Busy = false;
                Visible = true;
            }
            else if (requestActivation.Mount > CardBalanceResult.SaldoDisponible)

            {
               // _ = _activateCardPage.DisplayAlert("Alerta!", "El monto que estas solicitando es mayor a tu saldo disponible", "Aceptar");
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "El monto que estas solicitando es mayor a tu saldo disponible.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                Busy = false;
                Visible = true;
            }
            else
            {
                resultActivation = await DataGas.ActivateCard(requestActivation).ConfigureAwait(true);
                if (resultActivation.Aprobada)
                {
                   // _ = _activateCardPage.DisplayAlert("Mensaje", "Tarjeta activada con folio: " + resultActivation.Folio, "Aceptar");
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = "Tarjeta activada con folio: " + resultActivation.Folio;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    cardModel.Estatus = 10;
                    cardModel.EstatusDescripcion = "Activa";
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    //Device.BeginInvokeOnMainThread(() =>
                    //{
                    //    Application.Current.MainPage.Navigation.PopModalAsync();
                    //    //RootPage.NavigationFromMenu((int)Enumeradores.enumMenuItemType.Cards).ConfigureAwait(true);
                    //    // Se tiene que regresar al listado de tarjetas revisar el flujo
                    //});
                }
                else
                {
                    Busy = false;
                    Visible = true;
                }
            }
        }
        /// <summary>
        /// Verificar restricciones en tarjeta
        /// </summary>
        /// <returns>indicador del proceso</returns>
        internal async Task<bool> CheckRestrictions()
        {
            try
            {
                restrict = await DataGas.CheckRestrictions(CsmId).ConfigureAwait(true);
                if (restrict.Any(item => item == "GpsEncendido"))
                {
                    //location = DependencyService.Get<ILocation>();
                    await location.ObtainLocation();
                    location.locationObtained += new EventHandler<ILocationEventArgs>(Location_locationObtained);

                    try
                    {
                        var location = await Geolocation.GetLastKnownLocationAsync();
                        if (location == null)
                        {
                            location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                            {
                                DesiredAccuracy = GeolocationAccuracy.High,
                                Timeout = TimeSpan.FromSeconds(30)
                            });
                        }
                        if (location == null)
                        {
                            //  Estado de error 
                            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                            InformativeViewModel.Instance.Title = "Alerta!";
                            InformativeViewModel.Instance.Message = "Problema al validar la ubicacion, intente mas tarde, .";
                            await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                        }
                        else
                        {
                            //labelLatLong.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                            lat = location.Latitude;
                            lng = location.Longitude;
                            restrict.Add("EscaneoCodigoQR");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }

                }
                CardBalanceResult = await DataCard.GetBalanceAsync(cardModel).ConfigureAwait(true);
                return (restrict != null) ? restrict.Any(item => item == "EscaneoCodigoQR") : false;
            }
            catch (Exception)
            {
               // _ = _activateCardPage.DisplayAlert("Alerta!", "Intente mas tarde, problema al validar permisos.", "Aceptar");
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "Intente mas tarde, problema al validar permisos.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                return true;
            }
        }
        /// <summary>
        /// Obtener coordenadas
        /// </summary>
        /// <param name="sender">Objeto con el que se trabaja</param>
        /// <param name="e">parametros del evento</param>
        private void Location_locationObtained(object sender, ILocationEventArgs e)
        {
            lat = e.lat;
            lng = e.lng;
        }
        /// <summary>
        /// Ejetuar validador de QR
        /// </summary>
        /// <returns></returns>
        async Task ExecuteActivateQrReader(CardModel args)
        {
            //CAMBIO por NET Maui
            var popupQR = new YesOrNotAlert();
            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Mensaje";
            InformativeViewModel.Instance.Message = "Deseas vincular un codigo QR para activar esta tarjeta y asi poder realizar la solicitud";

            await MopupService.Instance.PushAsync(popupQR);

            var vincularQR = await popupQR.PopupDismissedTask;

            if (vincularQR == "YES")
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
                    await _activateCardPage.Navigation.PopModalAsync();

                    //await _mainPage.DisplayAlert("Alerta!", "QR no valido", "Aceptar").ConfigureAwait(true);
                }

                else if (rvalue != "Cancelar" && rvalue != "0")
                {
                    Guid guid;
                    _ = Guid.TryParse(rvalue, out guid);
                    if (!guid.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                    {
                        if (await DataGas.LinkedUser(guid).ConfigureAwait(true))
                        {
                            //await _mainPage.DisplayAlert("Mensaje", "QR enlazado correctamente", "Aceptar").ConfigureAwait(true);

                            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                            InformativeViewModel.Instance.Title = "Mensaje";
                            InformativeViewModel.Instance.Message = "QR enlazado correctamente.";
                            await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                            args.Complemento.QrId = -1;
                        }
                        else
                        {
                            //await _mainPage.DisplayAlert("Alerta!", "Este QR ya esta en uso o ya fue cancelado", "Aceptar").ConfigureAwait(true);
                            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                            InformativeViewModel.Instance.Title = "Alerta!";
                            InformativeViewModel.Instance.Message = "Este QR ya esta en uso o ya fue cancelado.";
                            await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                            await _activateCardPage.Navigation.PopModalAsync();
                        }

                    }
                    else
                    {
                        //await _mainPage.DisplayAlert("Alerta!", "QR no valido", "Aceptar").ConfigureAwait(true);

                        InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                        InformativeViewModel.Instance.Title = "Alerta!";
                        InformativeViewModel.Instance.Message = "QR no valido.";
                        await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                        await _activateCardPage.Navigation.PopModalAsync();
                    }

                }
                else
                {
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = "Proceso cancelado.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    await _activateCardPage.Navigation.PopModalAsync();
                }
            }
            else
            {
               await _activateCardPage.Navigation.PopModalAsync();
            }
        }
        /// <summary>
        /// Evitar que se brinquen validacion de QR
        /// </summary>
        /// <param name="sender">Objeto con el que se trabaja</param>
        /// <param name="e">parametros del evento</param>
        private void Page_Disappearing(object sender, EventArgs e)
        {
            if (IsBusy)
            {
               // page.IsBusy = IsBusy = false; Cambio por Net Maui
                _activateCardPage.Navigation.PopModalAsync();
            }
        }
        
        /// <summary>
        /// Regreso a pantalla principal
        /// </summary>
        public void CardsPage()
        {
            _activateCardPage.Navigation.PopModalAsync();
        }

    }
}
