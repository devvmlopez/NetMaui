using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using InntecMobileNetMaui.Views.Gas;
using InntecMobileNetMaui.Views.Login;
using InntecMobileNetMaui.Views.QR;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Gas
{
    public class MainGasViewModel : BaseViewModel
    {
        CardsPage RootPage => Application.Current.MainPage as CardsPage;
        public Command LoadCardsCommand { get; set; }
        public Command LinkCardCommand { get; set; }
        public Command UnLinkCardCommand { get; set; }
        public Command ActivationRequestCommand { get; set; }
        public Command ActivationRequestReportCommand { get; set; }
        private ObservableCollection<CardModel> cards;
        public ObservableCollection<CardModel> Cards { get => cards; set => SetProperty(ref cards, value); }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing; set => SetProperty(ref isRefreshing, value);
        }
        Views.Gas.MainGasPage mainGasPage;
        private MainViewModel mainViewModel;
        /// <summary>
        /// Inicializar objetos para combustibles
        /// </summary>
        /// <param name="mainGasPage">pagina del binding</param>
        public MainGasViewModel(Views.Gas.MainGasPage mainGasPage)
        {
            Title = "Combustibles";
            this.mainGasPage = mainGasPage;
            this.mainViewModel = new MainViewModel(RootPage);
            Cards = new ObservableCollection<CardModel>();
            LoadCardsCommand = new Command(async () => await ExecuteLoadCardsCommand().ConfigureAwait(true));
            LinkCardCommand = new Command((args) => ExecuteLinkCardCommand((CardModel)args));
            UnLinkCardCommand = new Command(async (args) => await ExecuteUnLinkCardCommand((CardModel)args));
            ActivationRequestCommand = new Command((args) => ExecuteActivationRequestCommand((CardModel)args));
            ActivationRequestReportCommand = new Command((args) => ExecuteActivationRequestReportCommand((CardModel)args));
        }
        /// <summary >
        /// Reporte de activacion
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        private void ExecuteActivationRequestReportCommand(CardModel args)
        {
            mainViewModel.VerifyToken();
            mainGasPage.Navigation.PushAsync(new ActivationRequestReportPage(args));
        }
        /// <summary>
        /// Solicitud de activacion
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        private void ExecuteActivationRequestCommand(CardModel args)
        {
            mainViewModel.VerifyToken();
            mainGasPage.Navigation.PushModalAsync(new ActivateCardPage(args));
        }
        /// <summary>
        /// Eliminar QR a la tarjeta
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private async Task ExecuteUnLinkCardCommand(CardModel args)
        {
            IsBusy = true;

            
            if (args.Complemento.UnLinkQr)
            {
                mainViewModel.VerifyToken();

                var popup = new YesOrNotAlert();
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Advertencia!";
                InformativeViewModel.Instance.Message = " Esta seguro de querer eliminar el QR de esta tarjeta? ";

                await MopupService.Instance.PushAsync(popup);
                var rvalue = await popup.PopupDismissedTask;

                if (rvalue == "YES")
                {
                    mainViewModel.UnlinkUser.Execute(args);
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = "El Qr fue eliminado de la tarjeta con exito";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
               
            }
            IsBusy = false;
        }
        /// <summary>
        /// Enlazar QR a tarjeta
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async void ExecuteLinkCardCommand(CardModel args)
        {
            mainViewModel.VerifyToken();
            if (args.Complemento.LinkQr)
            {
                //mainViewModel.StartQrReader.Execute(args as CardModel);  //Se implemento la version 2 para Maui
                mainViewModel.StartQrReaderV2.Execute(args);
            }
                
        }
        /// <summary>
        /// Listado de tarjetas de combustible
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadCardsCommand()
        {
            mainViewModel.VerifyToken();
            try
            {

                IsRefreshing = true;
                Cards.Clear();
                var cardsResult = App.Cards.Where(item => item.Complemento.ProductoGrupoId == (int)Enumeradores.enumProductoGrupo.COMBUSTIBLES);
                foreach (CardModel card in cardsResult)
                {
                    Cards.Add(card);
                }
            }
            catch (Exception)
            {
                IsRefreshing = false;
               // _ = mainGasPage.DisplayAlert("Error!", "No fue posible mostrar las tarjetas, intenta mas tarde", "Aceptar");
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
