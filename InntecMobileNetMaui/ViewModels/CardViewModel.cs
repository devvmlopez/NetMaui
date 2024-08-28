using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.Services;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.ViewModels.Messages;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InntecMobileNetMaui.ViewModels
{
    public partial class CardViewModel : BaseAlertViewModel
    {
        #region Properties
        [ObservableProperty]    
        private bool _loadingBalance;

        [ObservableProperty]
        private MovementsModel _cardMovementsResult;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private ObservableCollection<CardModel> _cards;

        [ObservableProperty]
        private int _position;

        [ObservableProperty]
        private bool _cerrado;

        #endregion

        private int year, month;
        public List<Menu> MyMenu { get; set; }
        private ICardsService<CardModel> cardService;

        private List<Menu> GetMenus()
        {
            return new List<Menu>
            {
                new Menu{ Name = "Home", Icon = "home.png"},
                new Menu{ Name = "Profile", Icon = "user.png"},
                new Menu{ Name = "Notifications", Icon = "bell.png"},
                new Menu{ Name = "Messages", Icon = "envelope.png"},
                new Menu{ Name = "My Tasks", Icon = "tasks.png"},
            };
        }

        public CardViewModel(ICardsService<CardModel> cardService)
        {

            Cerrado = true;
            Position = 0;

            MyMenu = GetMenus();
            Cards = new ObservableCollection<CardModel>();
            this.cardService = cardService;

            if (!WeakReferenceMessenger.Default.IsRegistered<NewCardMessage>(this))
            {
                WeakReferenceMessenger.Default.Register<NewCardMessage>(this, (r, m) =>//Recipient, Message
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsBusy = true;

                        CardModel newCard = m.Value;
                        newCard.EstatusDescripcion = "";
                        newCard = await cardService.AddItemV2Async(newCard).ConfigureAwait(true);
                        if (newCard.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Cards.Add(newCard);
                            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                            InformativeViewModel.Instance.Title = "Correcto";
                            InformativeViewModel.Instance.Message = "Tarjeta agregada correctamente";
                            await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                        }
                        else
                        {
                            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                            InformativeViewModel.Instance.Title = "Alerta";
                            InformativeViewModel.Instance.Message = newCard.Message;
                            await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                        }
                        IsBusy = false;
                    });

                });
            }

            if (!WeakReferenceMessenger.Default.IsRegistered<BlockCardMessage>(this))
            {
                WeakReferenceMessenger.Default.Register<BlockCardMessage>(this, (r, m) =>
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await ExecuteLoadCards();
                    });
                });
            }

        }
        /// <summary>
        /// Saldo de la tarjeta
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [RelayCommand]
        private async Task ExecuteBalanceCard(object Card)
        {
            CardModel CurrentCard = (CardModel)Card;
            LoadingBalance = false;
            if (!CurrentCard.Balance.Equals("Mostrar saldo")) return;

            var result = await cardService.GetBalanceAsync(CurrentCard);
            CurrentCard.Balance = result.SaldoDisponible.ToString("$ #,##0.00");

            int index = Cards.IndexOf(CurrentCard);
            Cards.RemoveAt(index);
            Cards.Insert(index, CurrentCard);
            Position = index;

        }


        /// <summary>
        /// Lisatdo de tarjetas
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task ExecuteLoadCards()
        {

            if (IsRefreshing)
                return;

            try
            {
                IsRefreshing = true;
                Cards.Clear();

                var cardResult = await cardService.GetItemsV2Async();

                foreach (CardModel card in cardResult)
                {
                    card.ImgProd = Constants.Url_Img_Base + "miinntecmovil/tarjetas/default/" + card.ImgProd;
                    card.Balance = "Mostrar saldo";
                    Cards.Add(card);

                }


            }
            catch (Exception ex)
            {
                //_ = cardsPage.DisplayAlert("Error!", "No fue posible mostrar las tarjetas, intenta mas tarde", "Aceptar");
                //App.Current.MainPage = new LoginPage();
            }
            finally
            {
                IsRefreshing = false;
            }

        }


        /// <summary>
        /// movimientos de tarjeta por mes seleccionado
        /// </summary>
        /// <param name="obj">Fecha que se necesita filtrar</param>
        /// <returns></returns>
        [RelayCommand]
        async Task ExecutCardMovements(object obj = null)
        {
            if (IsBusy)
                return;

            var items = (List<object>)obj;

            if (obj != null)
            {
                month = ((DateTime)items[0]).Month;
            }
            IsBusy = true;
            try
            {
                year = (month > DateTime.Now.Month) ? DateTime.Now.Year - 1 : DateTime.Now.Year;
                CardMovementsResult = await cardService.GetMovementsMonthAsync(items[1] as CardModel, year, month).ConfigureAwait(true);
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

    }
    public class Menu
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
