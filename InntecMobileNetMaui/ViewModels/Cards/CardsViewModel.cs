using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using InntecMobileNetMaui.Views.CustomView;
using InntecMobileNetMaui.Views.Login;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Cards
{
    class CardsViewModel : BaseViewModel, IDisposable
    {

        //CardsPage RootPage => Application.Current.MainPage as CardsPage;
        internal MainViewModel mainViewModel;
        public ObservableCollection<CardModel> Cards { get; set; }
        //public ObservableCollection<Models.Promotions.StorePromotion> Images { get; set; }
        //private bool _ROL_COMBUSTIBLE;
        //public bool ROL_COMBUSTIBLE { get => _ROL_COMBUSTIBLE; set => SetProperty(ref _ROL_COMBUSTIBLE, value); }
        //private bool _ROL_VIATICOS;
        //public bool ROL_VIATICOS { get => _ROL_VIATICOS; set => SetProperty(ref _ROL_VIATICOS, value); }
        //private BenefitHubModel _requestResult;
        //public BenefitHubModel RequestResult { get => _requestResult; set => SetProperty(ref _requestResult, value); }
        private string _notifyIco;
        public string notifyIco { get => _notifyIco; set => SetProperty(ref _notifyIco, value); }

        private bool carouselVisible = true;
        public bool CarouselVisible { get => carouselVisible; set => SetProperty(ref carouselVisible, value); }

        public Command LoadCardsCommand { get; set; }
        public Command LoadStoredPromotionsCommand { get; set; }
        public Command AddNewCardCommand { get; set; }
        public Command CardMovementsCommand { get; set; }
        public Command LoadPromoPageCommand { get; set; }
        public Command DeleteCardCommand { get; set; }
        public Command CancelCardCommand { get; set; }
        public Command CardNipCommand { get; set; }
        public Command BenefitHubCommand { get; private set; }
        public Command BlockCardCommand { get; set; }
        public Command ShowNotificationsCommand { get; set; }
        public Command MoreOptionsCommand { get; set; }

        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set => SetProperty(ref isRefreshing, value); }

        private CardsPage cardsPage;
        private NewCardPage newCardPage;
       // private CardNipPage cardNipPage;

        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="cardsPage">Pagina de binding</param>
        public CardsViewModel(CardsPage cardsPage)
        {
            this.cardsPage = cardsPage;
            Inicialize();
        }

        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        private void Inicialize()
        {
            Title = "Mis Tarjetas";
            Cards = new ObservableCollection<CardModel>();
            //mainViewModel = new MainViewModel(RootPage);
            notifyIco = "Notification.png";
            LoadCardsCommand = new Command(async () => await ExecuteLoadCardsCommand().ConfigureAwait(true));
            CardMovementsCommand = new Command((args) => ExecuteCardMovementsCommand((CardModel)args));
            DeleteCardCommand = new Command(async (args) => await ExecuteDeleCardCommand((CardModel)args).ConfigureAwait(true));
            CancelCardCommand = new Command(async (args) => await ExecuteCancelCardCommand((CardModel)args).ConfigureAwait(true));
            AddNewCardCommand = new Command(async (args) => await ExecuteAddNewCardCommand(args).ConfigureAwait(true));
            BlockCardCommand = new Command(async (args) => await ExecuteBlockCardCommand((CardModel)args).ConfigureAwait(true));
            CardNipCommand = new Command(async (args) => await ExecuteCardNipCommand((CardModel)args).ConfigureAwait(true));
            ShowNotificationsCommand = new Command(async () => await ExecuteShowNotificationsCommand().ConfigureAwait(true));
            LoadStoredPromotionsCommand = new Command(async () => await ExecuteLoadStoredPromotionsCommand().ConfigureAwait(true));
            MoreOptionsCommand = new Command(async (args) => await ExecuteMoreOptionsCommand((CardModel)args).ConfigureAwait(true));
            LoadPromoPageCommand = new Command((args) => ExecuteLoadPromoPageCommand(args));

            MessagingCenter.Unsubscribe<NewCardPage, CardModel>(this, "Nueva tarjeta");
            MessagingCenter.Subscribe<NewCardPage, CardModel>(this, "Nueva tarjeta", async (obj, item) =>
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                try
                {
                    obj.IsBusy = true;
                    CardModel newCard = item;
                    newCard.EstatusDescripcion = "";
                    newCard = await DataCard.AddItemV2Async(newCard).ConfigureAwait(true);
                    if (newCard == null) { IsBusy = false; return; }
                    if (newCard.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Cards.Add(newCard);
                        await cardsPage.DisplayAlert("Mensaje", "La tarjeta se ha agregado correctamente", "Aceptar").ConfigureAwait(true);

                    }
                    else
                    {
                        await cardsPage.DisplayAlert("Alerta!", newCard.Message, "Aceptar").ConfigureAwait(true);
                    }
                    obj.Btn_NewCard.IsEnabled = true;
                    obj.viewModel.IsBusy = false;
                    IsBusy = false;
                }
                catch
                {
                    await cardsPage.DisplayAlert("Alerta!", Constants.ERROR_EXCEPTION_SERVICE, "Aceptar").ConfigureAwait(true);
                    obj.Btn_NewCard.IsEnabled = true;
                    obj.viewModel.IsBusy = false;
                    IsBusy = false;
                }
            });


            //MessagingCenter.Unsubscribe<CardNipPage, CardNipModel>(this, "Cambiar NIP");
            //MessagingCenter.Subscribe<CardNipPage, CardNipModel>(this, "Cambiar NIP", async (obj, item) =>
            //{
            //    if (IsBusy)
            //    {
            //        return;
            //    }

            //    IsBusy = true;

            //    try
            //    {
            //        obj.IsBusy = true;
            //        var resp = await DataCard.ChangeNipV2Async(item).ConfigureAwait(true);
            //        await cardsPage.DisplayAlert(
            //            resp.StatusCode == System.Net.HttpStatusCode.OK ? "Mensaje" : "Alerta!",
            //            resp.Message,
            //            "Aceptar")
            //            .ConfigureAwait(true);
            //        obj.Btn_ChangeNip.IsEnabled = true;
            //        obj.viewModel.IsBusy = false;
            //        IsBusy = false;
            //    }
            //    catch
            //    {
            //        await cardsPage.DisplayAlert("Alerta!", Constants.ERROR_EXCEPTION_SERVICE, "Aceptar").ConfigureAwait(true);

            //        obj.Btn_ChangeNip.IsEnabled = true;
            //        obj.viewModel.IsBusy = false;
            //        IsBusy = false;
            //    }
            //});


            //MessagingCenter.Unsubscribe<MoreOptionsPage, CardModel>(this, Constants.LoadCards);
            //MessagingCenter.Subscribe<MoreOptionsPage, CardModel>(this, Constants.LoadCards, async (obj, item) =>
            //{
            //    await ExecuteLoadCardsCommand();
            //});
            //Images = new ObservableCollection<Models.Promotions.StorePromotion>();
            // Refactorizar la logica del cambio del NIP y el tema de las promociones
        }

        private async Task ExecuteMoreOptionsCommand(CardModel cardModel)
        {
            // await cardsPage.Navigation.PushPopupAsync(new Views.Popups.CardPage.MoreOptionsPage(cardModel), true);
          //  await PopupNavigation.Instance.PushAsync(new Views.Popups.CardPage.MoreOptionsPage(cardModel), true); Refactorizar esta logica
        }

        private void ExecuteLoadPromoPageCommand(object args)
        {
            //if (mainViewModel.VerifyToken())
            //{
            //    // cardsPage.Navigation.PushPopupAsync(new Views.Pomotions.PromotionsListPage(((Models.Promotions.StorePromotion)args).ComercioId));
            //    PopupNavigation.Instance.PushAsync(
            //       new Views.Pomotions.PromotionsListPage(((Models.Promotions.StorePromotion)args).ComercioId));
            //} Refactorizar la logica de las promociones y como se muestran en el menu principal
        }

        private async Task ExecuteLoadStoredPromotionsCommand()
        {
            //var stores = await DataPromos.getStorePromotion().ConfigureAwait(true);
            //Images.Clear();
            //if (stores.Count == 0)
            //{
            //    CarouselVisible = false;
            //    return;
            //}
            //Thickness paddingCarousel;

            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    if (Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width < 828 ||
            //         Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width >= 1125)
            //    {
            //        paddingCarousel = new Thickness(5, 5);
            //    }
            //    else
            //    {
            //        paddingCarousel = new Thickness(20, 5);
            //    }
            //}
            //else
            //{
            //    paddingCarousel = ((Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width < 1080)) ? new Thickness(10, 5) : new Thickness(32, 5);
            //}

            //try
            //{
            //    foreach (var item in stores)
            //    {
            //        item.paddinCarousel = paddingCarousel;
            //        Images.Add(item);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Refactorizar logica de como carga del api las promociones 

        }

        private async Task ExecuteShowNotificationsCommand()
        {
            //notifyIco = "Notification.png";
            //await this.cardsPage.Navigation.PushModalAsync(new NavigationPage(new Views.Notify.NotityPage())).ConfigureAwait(true);
            //Refactorizar la logica de como muestra las NOTIFICACIONES 

        }

        /// <summary>
        /// Cambio de NIP
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private async Task ExecuteCardNipCommand(CardModel args)
        {
            //if (mainViewModel.VerifyToken())
            //{
            //    cardNipPage = new CardNipPage(args);
            //    // await cardsPage.Navigation.PushPopupAsync(cardNipPage, true);

            //    await PopupNavigation.Instance.PushAsync(cardNipPage, true);
            //}
            // Esta funcion es para el cambio de NIP dicha funcion no esta disponible por el momento
        }

        /// <summary>
        /// Bloquear tarjeta
        /// </summary>
        /// <param name="cardModel"></param>
        /// <returns></returns>
        private async Task ExecuteBlockCardCommand(CardModel cardModel)
        {
            IsBusy = true;
            if (mainViewModel.VerifyToken())
            {
                CardModel card = Cards.FirstOrDefault(item => item.UsuarioCsmTarjetaId == cardModel.UsuarioCsmTarjetaId);
                CardModel cardUpdate = await DataCard.BlockItemV2Async(cardModel).ConfigureAwait(true);
                LoadCardsCommand.Execute(null);
                //await cardsPage.DisplayAlert("Mensaje", cardUpdate.Message, "Aceptar").ConfigureAwait(true);

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = cardUpdate.Message;
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
               // Shell.Current.GoToAsync("//LoginPage");


            }
            IsBusy = false;
        }

        /// <summary>
        /// Agregar nueva tarjeta
        /// </summary>
        /// <param name="args">Obsoleto</param>
        /// <returns></returns>
        private async Task ExecuteAddNewCardCommand(object args)
        {
            if (mainViewModel.VerifyToken())
            {
                newCardPage = new NewCardPage();
                // await cardsPage.Navigation.PushPopupAsync(newCardPage, true);
                //await PopupNavigation.Instance.PushAsync(newCardPage, true);
                await MopupService.Instance.PushAsync(new NewCardPage());
            }
        }

        /// <summary>
        /// Cancelar tarjeta
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private async Task ExecuteCancelCardCommand(CardModel args)
        {
            if (mainViewModel.VerifyToken())
            {
                IsBusy = true;
                CardReport cardReport = new CardReport()
                {
                    Id = args.UsuarioCsmTarjetaId,

                    Plataforma = "app"

                };
                var Cancel = string.Empty;
                if (await cardsPage.DisplayAlert(
                        $"Reportar tarjeta: {args.Tarjeta}",
                        $"Al reportar tu tarjeta por robo, extravío o tarjeta dañada, a tarjeta quedara bloqueada y no podrás realizar consultas de saldo y movimientos.",
                        "Continuar",
                        "Cancelar").ConfigureAwait(true))
                {
                    switch (await cardsPage.DisplayActionSheet(
                                "Selecciona el motivo del reporte",
                                "Cancelar",
                                null,
                                new string[] { "Robo", "Extravio", "Tarjeta dañada" }).ConfigureAwait(true))
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
                        await cardsPage.DisplayAlert("Mensaje", await DataCard.ReportarItemV2Async(cardReport).ConfigureAwait(true), "Aceptar").ConfigureAwait(true);
                    }
                }
                IsBusy = false;
            }
        }

        /// <summary>
        /// Eliminar tarjeta
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private async Task ExecuteDeleCardCommand(CardModel args)
        {
            if (mainViewModel.VerifyToken())
            {
                IsBusy = true;

                if (await cardsPage.DisplayAlert(
                        $"Remover tarjeta: {args.Tarjeta}",
                        $"Al remover tu tarjeta ya no será posible realizar consultas de saldo y movimientos.\nNo podrás realizar aclaraciones y ya no será posible agregarla de nuevo.",
                        "Continuar",
                        "Cancelar").ConfigureAwait(true))
                {
                    if (await cardsPage.DisplayAlert(
                            "Confirma que estás de acuerdo con remover la tarjeta",
                                "",
                                "Estoy de acuerdo",
                                "Cancelar").ConfigureAwait(true))
                    {
                        string msg = await DataCard.DeleteItemV2Async(args, true).ConfigureAwait(true);
                        await cardsPage.DisplayAlert("Mensaje", msg, "Aceptar").ConfigureAwait(true);
                        LoadCardsCommand.Execute(null);
                    }
                }

                IsBusy = false;
            }
        }

        /// <summary>
        /// Movimientos de la tarjeta
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private void ExecuteCardMovementsCommand(CardModel args)
        {
            if (mainViewModel.VerifyToken())
            {
                this.cardsPage.Navigation.PushAsync(new CardDetailPage(args)); //
                //await Shell.Current.GoToAsync("//LoginPage");
            }


        }

        /// <summary>
        /// Lisatdo de tarjetas
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadCardsCommand()
        {
            if (IsRefreshing)
                return;

            try
            {

                IsRefreshing = true;
                //Carga de imagenes de empresas que tienen alguna promocion activa.
                LoadStoredPromotionsCommand.Execute(null);
                Cards.Clear();

                var cardsResult = await DataCard.GetItemsV2Async(true).ConfigureAwait(true);

                string ProductosId = string.Empty;
                notifyIco = "Notification.png";


                foreach (CardModel card in cardsResult)
                {
                    card.ImgProd = Constants.Url_Img_Base + "miinntecmovil/tarjetas/default/" + card.ImgProd;
                    Cards.Add(card);
                    ProductosId += (!ProductosId.Contains(card.ProductoID.ToString())) ? card.ProductoID.ToString() + "," : "";
                }

                //var newNotify = await DataNotify.verifyNewNotify(ProductosId);
                //if (newNotify)
                //    notifyIco = "NotificationR.png";

                //if (Cards.Any(item => item.ProductoID == (int)Enumeradores.enumProductoID.VIATICOS))
                //{
                //    MessagingCenter.Send<object, bool>(this, App.Viaticos, true);
                //}

                //if (Cards.Any(item => item.Complemento.ProductoGrupoId == (int)Enumeradores.enumProductoGrupo.COMBUSTIBLES))
                //{
                //    ROL_COMBUSTIBLE = true;
                //}
                //if (Cards.Any(item => item.Complemento.ProductoGrupoId == (int)Enumeradores.enumProductoGrupo.VIATICOS))
                //{
                //    ROL_VIATICOS = true;
                //}  VALES , Viaticos que aun no estan implementados
                App.Cards = cardsResult.ToList();
            }
            catch (Exception)
            {
                //_ = cardsPage.DisplayAlert("Error!", "No fue posible mostrar las tarjetas, intenta mas tarde", "Aceptar");
                //App.Current.MainPage = new LoginPage();

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Error!";
                InformativeViewModel.Instance.Message = "No fue posible mostrar las tarjetas, intenta mas tarde";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                //await Shell.Current.GoToAsync("//LoginPage");
            }
            finally
            {
                IsRefreshing = false;

            }
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
