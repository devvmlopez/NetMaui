using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Assist;
using InntecMobileNetMaui.Views.Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Month = InntecMobileNetMaui.Models.Month;

namespace InntecMobileNetMaui.ViewModels.Cards
{
    public class CardDetailViewModel : BaseViewModel, IDisposable
    {
        private BalanceModel _cardBalanceResult = null;
        private MovementsModel _cardMovementsResult = null;
        private BackgroundWorker _backgroundWorker;

        public Command CardMovementsCommand { get; set; }
        public Command CardMovementsMonthCommand { get; set; }
        public Command VerifyAssistCommand { get; set; }

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

        private CardModel cardModel;
        private CardDetailPage _cardDetailPage;
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
            this.cardModel = cardModel;
            this._cardDetailPage = cardDetailPage;
            VerifyAssistCommand = new Command(async (assitModel) => await ExecuteVerifyAssistCommand((AssistModel)assitModel).ConfigureAwait(true));
            CardMovementsCommand = new Command(async () => await ExecutCardMovementsCommand().ConfigureAwait(true));
            CardMovementsMonthCommand = new Command(async (object obj) => await ExecutCardMovementsCommand(obj).ConfigureAwait(true));
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
                }
                else
                {
                    CardMovementsResult = await DataCard.GetMovementsMonthAsync(cardModel, year, month).ConfigureAwait(true);
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

        public void Dispose()
        {
            ((IDisposable)_backgroundWorker).Dispose();
        }
    }
}
