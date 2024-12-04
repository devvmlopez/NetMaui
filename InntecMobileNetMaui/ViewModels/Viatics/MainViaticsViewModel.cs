using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using InntecMobileNetMaui.Views.Viatics;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Viatics
{
    class MainViaticsViewModel : BaseViewModel
    {
        CardsPage RootPage => Application.Current.MainPage as CardsPage;
        public Command LoadCardsCommand { get; set; }
        public Command ViaticRequestCommand { get; set; }
        public Command ViaticListCommand { get; set; }
        public Command ViaticCheckingCommand { get; set; }
        private ObservableCollection<CardModel> cards;
        public ObservableCollection<CardModel> Cards { get => cards; set => SetProperty(ref cards, value); }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing; set => SetProperty(ref isRefreshing, value);
        }

        private MainViewModel mainViewModel;

        private MainViaticsPage mainViaticsPage;
        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="mainViaticsPage">pagina principal de viaticos</param>
        public MainViaticsViewModel(MainViaticsPage mainViaticsPage)
        {
            Title = "Viaticos";
            this.mainViaticsPage = mainViaticsPage;
            this.mainViewModel = new MainViewModel(RootPage);

            Cards = new ObservableCollection<CardModel>();
            LoadCardsCommand = new Command(async () => await ExecuteLoadCardsCommand().ConfigureAwait(true));
            ViaticRequestCommand = new Command(async (args) => await ExecuteViaticRequestCommand((CardModel)args).ConfigureAwait(true));
            ViaticListCommand = new Command(async (args) => await ExecuteViaticListCommand((CardModel)args).ConfigureAwait(true));
            ViaticCheckingCommand = new Command(async (args) => await ExecuteViaticCheckingCommand((CardModel)args).ConfigureAwait(true));
        }
        /// <summary>
        /// Listado de solicitudes(Comprobacion)
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteViaticCheckingCommand(CardModel cardModel)
        {
            mainViewModel.VerifyToken();
            await mainViaticsPage.Navigation.PushAsync(new ViaticsCheckPage(cardModel)).ConfigureAwait(true);
        }
        /// <summary>
        ///  Listado de solicitudes
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteViaticListCommand(CardModel cardModel)
        {
            mainViewModel.VerifyToken();
            await mainViaticsPage.Navigation.PushAsync(new ViaticsListPage(cardModel)).ConfigureAwait(true);
        }
        /// <summary>
        /// Nueva solicitud de viaticos
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteViaticRequestCommand(CardModel cardModel)
        {
            mainViewModel.VerifyToken();
            await mainViaticsPage.Navigation.PushAsync(new ViaticsRequestPage(cardModel)).ConfigureAwait(true);
        }
        /// <summary>
        /// Lista de tarjetas de viaticos
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteLoadCardsCommand()
        {
            mainViewModel.VerifyToken();
            try
            {
                IsRefreshing = true;
                Cards.Clear();
                var cardsResult = App.Cards.Where(item => item.Complemento.ProductoGrupoId == (int)Enumeradores.enumProductoGrupo.VIATICOS);
                foreach (CardModel card in cardsResult)
                {
                    Cards.Add(card);
                }
            }
            catch (Exception)
            {
                IsRefreshing = false;
               // _ = mainViaticsPage.DisplayAlert("Error!", "No fue posible mostrar las tarjetas, intenta mas tarde", "Aceptar");
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Error!";
                InformativeViewModel.Instance.Message = "No fue posible mostrar las tarjetas, intenta mas tarde.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
            }
            await Task.Delay(100).ConfigureAwait(true);
            IsRefreshing = false;
        }
    }
}
