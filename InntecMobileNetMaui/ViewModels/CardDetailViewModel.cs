using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Assist;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Asistencia;
using InntecMobileNetMaui.Views.Cards;
using Mopups.Pages;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Month = InntecMobileNetMaui.Models.Month;

namespace InntecMobileNetMaui.ViewModels
{
    public class CardDetailViewModel : BaseViewModel, IDisposable
    {
        private BalanceModel _cardBalanceResult = null;
        private MovementsModel _cardMovementsResult = null;
        private BackgroundWorker _backgroundWorker;

        public Command CardMovementsCommand { get; set; }
        public Command CardMovementsMonthCommand { get; set; }
        public Command VerifyAssistCommand { get; set; }
        public Command CancelCardCommand { get; set; }
        public Command DeleteCardCommand { get; set; }
        public Command BlockCardCommand { get; set; }

        public Command MoreOptionsCommand { get; set; }

        public BalanceModel CardBalanceResult
        {
            get => _cardBalanceResult;
            set => SetProperty(ref _cardBalanceResult, value);
        }

        public MovementsModel CardMovementsResult
        {
            get => _cardMovementsResult;
            set => SetProperty(ref _cardMovementsResult, value);
        }

        private string img;
        public string Img
        {
            get => img;
            set => SetProperty(ref img, value);
        }

        private string estadoTarjeta;
        public string EstadoTarjeta
        {
            get => estadoTarjeta;
            set => SetProperty(ref estadoTarjeta, value);
        }

        private string colorTarjeta;
        public string ColorTarjeta
        {
            get => colorTarjeta;
            set => SetProperty(ref colorTarjeta, value);
        }
        private CardModel cardModel;
        private CardDetailPage _cardDetailPage;
        internal MainViewModel mainViewModel;
        private CardsPage cardsPage;
        public List<Month> MonthList { get; set; }
        public int ItemSelect { get; set; }
        private int year, month;

        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="cardModel">Datos de la tarjeta</param>
        /// <param name="login">Datos del usuario que inicio sesion</param>
        /// <param name="cardDetailPage">Pagina del binding</param>
        public CardDetailViewModel(CardModel cardModel, CardDetailPage cardDetailPage)
        {
            Title = "Movimientos";
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            Months _monthsItems = new Months();
            DateTime Hoy = DateTime.Now;
            int primero = Hoy.Month - 1;
            int segundo = Hoy.AddMonths(-2).Month;
            int tercero = Hoy.AddMonths(-3).Month;

            if (segundo == 12)
            {
                segundo = 0;
                tercero = 11;
            }
            if (tercero == 12)
            {
                tercero = 0;
            }

            MonthList = new List<Month>()
            {
                _monthsItems._months.Find(m => m.Id == 99),
                _monthsItems._months[primero],
                _monthsItems._months[segundo],
                _monthsItems._months[tercero]
            };
            ItemSelect = 0;

            Card = cardModel.Tarjeta.Substring(cardModel.Tarjeta.Length - 4, 4).Replace('X', '*');
            Img = cardModel.ImgProd;
            EstadoTarjeta = cardModel.EstatusDescripcion;
            ColorTarjeta = cardModel.Color;
            this.cardModel = cardModel;
            this._cardDetailPage = cardDetailPage;
            VerifyAssistCommand = new Command(async (assitModel) => await ExecuteVerifyAssistCommand((AssistModel)assitModel).ConfigureAwait(true));
            CardMovementsCommand = new Command(async () => await ExecutCardMovementsCommand().ConfigureAwait(true));
            CardMovementsMonthCommand = new Command(async (object obj) => await ExecutCardMovementsCommand(obj).ConfigureAwait(true));
            BlockCardCommand = new Command(async (cardModel) => await ExecuteBlockCardCommand((CardModel)cardModel).ConfigureAwait(true));
            CancelCardCommand = new Command(async (cardModel) => await ExecuteCancelCardCommand((CardModel)cardModel).ConfigureAwait(true));
            DeleteCardCommand = new Command(async (cardModel) => await ExecuteDeleCardCommand((CardModel)cardModel).ConfigureAwait(true));
            MoreOptionsCommand = new Command(async (args) => await ExecuteMoreOptionsCommand((CardModel)args).ConfigureAwait(true));
            _backgroundWorker = new BackgroundWorker();

        }

        /// <summary>
        /// Verificacion de Asistencia
        /// </summary>
        /// <param name="assistModel">Datos para la asistencia</param>
        /// <returns></returns>
        private async Task ExecuteVerifyAssistCommand(AssistModel assistModel)
        {
            if (await DataCard.VerifyAssist(cardModel.UsuarioCsmTarjetaId).ConfigureAwait(true) == false)
            {
                UserModel userModel = new UserModel();
                //await _cardDetailPage.Navigation.PushAsync(new AssistPage(false, ref userModel, assistModel), true).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Movimientos de la tarejta
        /// </summary>
        /// <returns></returns>
        private async Task<bool> LoadDetailCard()
        {
            await ExecutCardMovementsCommand().ConfigureAwait(true);
            return IsBusy;
        }

        /// <summary>
        /// movimientos de tarjeta por mes seleccionado
        /// </summary>
        /// <param name="obj">Fecha que se necesita filtrar</param>
        /// <returns></returns>
        async Task ExecutCardMovementsCommand(object obj = null)
        {
            if (IsBusy)
                return;
            if (obj != null)
            {
                month = ((Month)obj).Id;
            }
            IsBusy = true;
            try
            {
                CardBalanceResult = await DataCard.GetBalanceAsync(cardModel).ConfigureAwait(true);
                year = (month > DateTime.Now.Month) ? DateTime.Now.Year - 1 : DateTime.Now.Year;

                if (month == 99)
                {
                    CardMovementsResult = await DataCard.GetMovementsAsync(cardModel).ConfigureAwait(true);
                    if (CardMovementsResult.ListMovimientos.Count <= 0)
                    {
                        InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                        InformativeViewModel.Instance.Title = "Mensaje";
                        InformativeViewModel.Instance.Message = CardMovementsResult.Detalle;
                        await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    }
                    
                }
                else
                {
                    CardMovementsResult = await DataCard.GetMovementsMonthAsync(cardModel, year, month).ConfigureAwait(true);
                    if (CardMovementsResult.ListMovimientos.Count <= 0)
                    {
                        InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                        InformativeViewModel.Instance.Title = "Mensaje";
                        InformativeViewModel.Instance.Message = CardMovementsResult.Detalle;
                        await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Cancelar tarjeta
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        /// <summary>
        /// Bloquear tarjeta
        /// </summary>
        /// <param name="cardModel"></param>
        /// <returns></returns>
        private async Task ExecuteBlockCardCommand(CardModel cardModel)
        {
            IsBusy = true;

            string estadoActual = cardModel.EstatusDescripcion;
            string estadoNuevo;
            if (estadoActual == "Activa")
            {
                estadoNuevo = "Bloquear";
            }
            else
            {
                estadoNuevo = "Activar";
            }
            var popup = new YesOrNotAlert();
            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Mensaje";
            InformativeViewModel.Instance.Message = " ¿Deseas " + estadoNuevo + " tu tarjeta? ";

            await MopupService.Instance.PushAsync(popup);

            var rvalue = await popup.PopupDismissedTask;

            if (rvalue == "YES")
            {
                CardModel cardUpdate = await DataCard.BlockItemV2Async(cardModel).ConfigureAwait(true);

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = cardUpdate.Message;
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                await Shell.Current.GoToAsync("//CardsPage");
            }

            IsBusy = false;
        }

        /// <summary>
        /// Cancelar tarjeta
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private async Task ExecuteCancelCardCommand(CardModel cardModel)
        {
            IsBusy = true;
            CardReport cardReport = new CardReport()
            {
                Id = cardModel.UsuarioCsmTarjetaId,

                Plataforma = "app"

            };
            var Cancel = string.Empty;

            var popup = new AcceptCancelAlert();
            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Mensaje";
            InformativeViewModel.Instance.Message = $"Reportar tarjeta: {cardModel.Tarjeta}" + '\n' +
                    $"Al reportar tu tarjeta por robo, extravío o tarjeta dañada, la tarjeta quedara bloqueada y no podrás realizar consultas de saldo y movimientos.";
            await MopupService.Instance.PushAsync(popup);

            var rvalue = await popup.PopupDismissedTask;

            if (rvalue == "Continuar")
            {
                var popupMulti = new MultiOpcion();
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = "Selecciona el motivo del reporte";
                await MopupService.Instance.PushAsync(popupMulti);

                var MultiCaso = await popupMulti.PopupDismissedTask;

                switch(MultiCaso)
                { 
                    case "Robo":
                        cardReport.Motivo = "1";
                        break;
                    case "Extravio":
                        cardReport.Motivo = "2";
                        break;
                    case "Tarjeta dañada":
                        cardReport.Motivo = "3";
                        break;
                    default:
                        cardReport.Motivo = string.Empty;
                        break;
                }
                if (!string.IsNullOrEmpty(cardReport.Motivo))
                {
                    await _cardDetailPage.DisplayAlert("Mensaje", await DataCard.ReportarItemV2Async(cardReport).ConfigureAwait(true), "Aceptar").ConfigureAwait(true);
                    await Shell.Current.GoToAsync("//CardsPage");
                }
                
            }
            else 
            { 
            
            }
                IsBusy = false;
           
        }

        /// <summary>
        /// Eliminar tarjeta
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private async Task ExecuteDeleCardCommand(CardModel cardModel)
        {
            IsBusy = true;

            var popup = new AcceptCancelAlert();
            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Mensaje";
            InformativeViewModel.Instance.Message = $"Remover tarjeta: {cardModel.Tarjeta}" + '\n' +
                    $"Al remover tu tarjeta ya no será posible realizar consultas de saldo y movimientos.\nNo podrás realizar aclaraciones y ya no será posible agregarla de nuevo.";
            await MopupService.Instance.PushAsync(popup);

            var rvalue = await popup.PopupDismissedTask;

            if (rvalue == "Continuar")
            {
                var popupConfirm = new YesOrNotAlert();
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = "Confirma que estás de acuerdo con remover la tarjeta";
                await MopupService.Instance.PushAsync(popupConfirm);
                var Confirmacion = await popupConfirm.PopupDismissedTask;

                if (Confirmacion == "YES")
                {
                     string msg = await DataCard.DeleteItemV2Async(cardModel, true).ConfigureAwait(true);  
                    // await _cardDetailPage.DisplayAlert("Mensaje", msg, "Aceptar").ConfigureAwait(true); 
                    //LoadCardsCommand.Execute(null);
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = msg;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    await Shell.Current.GoToAsync("//CardsPage");
                }
            }
            IsBusy = false;
            
        }
        public void Dispose()
        {
            ((IDisposable)_backgroundWorker).Dispose();
        }

        private async Task ExecuteMoreOptionsCommand(CardModel cardModel )
        {
            //Falta refactorizar
            //IsBusy = true;
            //await MopupService.Instance.PushAsync(new Views.Alerts.MoreOptionsPage(cardModel, false)).ConfigureAwait(true);
            //IsBusy = false;
            // 
        }
    }
}
