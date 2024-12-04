using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Notify;
using InntecMobileNetMaui.Models.Promotions;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using InntecMobileNetMaui.Views.CustomView;
using InntecMobileNetMaui.Views.CustomView.Card;
using InntecMobileNetMaui.Views.Login;
using Mopups.Services;
using Plugin.LocalNotification;
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

        CardsPage RootPage => Application.Current.MainPage as CardsPage;
        internal MainViewModel mainViewModel;
        public ObservableCollection<CardModel> Cards { get; set; }
        public ObservableCollection<Models.Promotions.StorePromotion> Images { get; set; }
       
        private bool _ROL_COMBUSTIBLE;
        public bool ROL_COMBUSTIBLE { get => _ROL_COMBUSTIBLE; set => SetProperty(ref _ROL_COMBUSTIBLE, value); }
        private bool _ROL_VIATICOS;
        public bool ROL_VIATICOS { get => _ROL_VIATICOS; set => SetProperty(ref _ROL_VIATICOS, value); }
        private BenefitHubModel _requestResult;
        public BenefitHubModel RequestResult { get => _requestResult; set => SetProperty(ref _requestResult, value); }
        private string _notifyIco;
        public string notifyIco { get => _notifyIco; set => SetProperty(ref _notifyIco, value); }

        private bool carouselVisible = true;
        public bool CarouselVisible { get => carouselVisible; set => SetProperty(ref carouselVisible, value); }
        private string estadoMoreOptions;
        public string EstadoMoreOptions
        {
            get => estadoMoreOptions;
            set => SetProperty(ref estadoMoreOptions, value);
        }
        private string estadoReportar;
        public string EstadoReportar
        {
            get => estadoReportar;
            set => SetProperty(ref estadoReportar, value);
        }
        private string estadoRemover;
        public string EstadoRemover
        {
            get => estadoRemover;
            set => SetProperty(ref estadoRemover, value);
        }
        public string estadoMovimientos;
        public string EstadoMovimientos
        {
            get => estadoMovimientos;
            set => SetProperty(ref estadoMovimientos, value);
        }
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

        private string estadoLoop ;
        public string EstadoLoop
        {
            get => estadoLoop;
            set => SetProperty(ref estadoLoop, value);
        }
        private CardsPage cardsPage;
        private NewCardPage newCardPage;


        public ObservableCollection<NotifyUser> Notifications { get; set; }
        
        //private CardNipPage cardNipPage;

        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="cardsPage">Pagina de binding</param>
        public CardsViewModel(CardsPage cardsPage)
        {
            this.cardsPage = cardsPage;
            Notifications = new ObservableCollection<NotifyUser>();
            Inicialize();
        }

        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        private void Inicialize()
        {
            Title = "Mis Tarjetas";
            EstadoMoreOptions = "True";
            EstadoRemover = "True";
            EstadoReportar = "True";
            EstadoMovimientos = "True";
            //EstadoLoop = "False";
            Images = new ObservableCollection<Models.Promotions.StorePromotion>();
            Cards = new ObservableCollection<CardModel>();
            mainViewModel = new MainViewModel(RootPage);
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

            MessagingCenter.Unsubscribe<MoreOptionsPage, CardModel>(this, Constants.LoadCards);
            MessagingCenter.Subscribe<MoreOptionsPage, CardModel>(this, Constants.LoadCards, async (obj, item) =>
            {
                await ExecuteLoadCardsCommand();
            });
        }

        private async Task ExecuteMoreOptionsCommand(CardModel cardModel)
        {
            EstadoMoreOptions = "False";
            if (IsBusy == false)
            {
                IsBusy = true;
                await MopupService.Instance.PushAsync(new Views.Alerts.MoreOptionsPage(cardModel, true)).ConfigureAwait(true);
                EstadoMoreOptions = "True";
            }
            else
            {
                TimeSpan.FromSeconds(2);
                IsBusy = false;
            }
            
        }
        /// <summary>
        /// Detalle de la promocion cuando le das click en el listado
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private void ExecuteLoadPromoPageCommand(object args)
        {
            if (mainViewModel.VerifyToken())
            {
               MopupService.Instance.PushAsync(
               new Views.Promotions.PromotionsListPage(((Models.Promotions.StorePromotion)args).ComercioId));
            }
        }
        /// <summary>
        /// Listado de promociones
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private async Task ExecuteLoadStoredPromotionsCommand()
        {
            var stores = await DataPromos.getStorePromotion().ConfigureAwait(true);
            Images.Clear();
            if (stores.Count == 0)
            {
                CarouselVisible = false;
                return;
            }
            Thickness paddingCarousel;

            if (Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.iOS) // Device.iOS)
            {
                if (DeviceDisplay.MainDisplayInfo.Width < 828 ||
                     DeviceDisplay.MainDisplayInfo.Width >= 1125)
                {
                    paddingCarousel = new Thickness(5, 5);
                }
                else
                {
                    paddingCarousel = new Thickness(20, 5);
                }
            }
            else
            {
                paddingCarousel = ((DeviceDisplay.MainDisplayInfo.Width < 1080)) ? new Thickness(10, 5) : new Thickness(32, 5);
            }

            try
            {
                foreach (Models.Promotions.StorePromotion item in stores)
                {
                    item.paddinCarousel = paddingCarousel;
                    Images.Add(item);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private async Task ExecuteShowNotificationsCommand()
        {
            await this.cardsPage.Navigation.PushModalAsync(new NavigationPage(new Views.Notify.NotityPage())).ConfigureAwait(true);
        }

        /// <summary>
        /// Cambio de NIP
        /// </summary>
        /// <param name="args">Datos de la tarjeta</param>
        /// <returns></returns>
        private async Task ExecuteCardNipCommand(CardModel args)
        {
            if (mainViewModel.VerifyToken())
            {
                //cardNipPage = new CardNipPage(args);  Cambio de Nip opcion no disponible por el momento
                //// await cardsPage.Navigation.PushPopupAsync(cardNipPage, true);
                //await PopupNavigation.Instance.PushAsync(cardNipPage, true);
            }
            InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
            InformativeViewModel.Instance.Title = "Alerta del sistema";
            InformativeViewModel.Instance.Message = "Esta opcion no esta disponible por el momento, disculpe las molestias.";
            await MopupService.Instance.PushAsync(InformativeAlert.Instance);
            await Shell.Current.GoToAsync("//CardsPage");

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
                InformativeViewModel.Instance.Message = " ¿Deseas "+estadoNuevo+" tu tarjeta? ";

                await MopupService.Instance.PushAsync(popup);

                var rvalue = await popup.PopupDismissedTask;
                if (rvalue == "YES")
                {

                    CardModel card = Cards.FirstOrDefault(item => item.UsuarioCsmTarjetaId == cardModel.UsuarioCsmTarjetaId);
                    CardModel cardUpdate = await DataCard.BlockItemV2Async(cardModel).ConfigureAwait(true);
                    LoadCardsCommand.Execute(null);

                    // await cardsPage.DisplayAlert("Mensaje", cardUpdate.Message, "Aceptar").ConfigureAwait(true);

                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = cardUpdate.Message;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }

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
               // await PopupNavigation.Instance.PushAsync(newCardPage, true);
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
                EstadoReportar = "False";
                IsBusy = true;
                CardReport cardReport = new CardReport()
                {
                    Id = args.UsuarioCsmTarjetaId,

                    Plataforma = "app"

                };
                var Cancel = string.Empty;

                var popup = new AcceptCancelAlert();
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = $"Reportar tarjeta: {args.Tarjeta}" + '\n' +
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

                    switch (MultiCaso)
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
                EstadoReportar = "True";
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
                EstadoRemover = "False";
                var popup = new AcceptCancelAlert();
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = $"Remover tarjeta: {args.Tarjeta}" + '\n' +
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
                        string msg = await DataCard.DeleteItemV2Async(args, true).ConfigureAwait(true);
                        //await cardsPage.DisplayAlert("Mensaje", msg, "Aceptar").ConfigureAwait(true);
                        LoadCardsCommand.Execute(null);

                        string tipoMensaje = Preferences.Default.Get("TipoMensaje", string.Empty);
                        if (tipoMensaje == "Error")
                        {
                            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
                            InformativeViewModel.Instance.Title = "Se ha presentado un problema";
                        }
                        else
                        {
                            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
                            InformativeViewModel.Instance.Title = "Mensaje";
                        }

                        InformativeViewModel.Instance.Message = msg;  
                        await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                        
                    }
                    
                }
                EstadoRemover = "True";
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
            EstadoMovimientos = "False";
            if (mainViewModel.VerifyToken())
            {
                this.cardsPage.Navigation.PushAsync(new CardDetailPage(args));
                EstadoMovimientos = "True";
            }
        }

        /// <summary>
        /// Lisatdo de tarjetas
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadCardsCommand()
        {
            int contadorNoti = 0;
            bool primeraVez = false;

            if (IsRefreshing)
                return;

            IsBusy = true;

            try
            {

                IsRefreshing = true;
                //Carga de imagenes de empresas que tienen alguna promocion activa.
                LoadStoredPromotionsCommand.Execute(null);
                Cards.Clear();

                var cardsResult = await DataCard.GetItemsV2Async(true).ConfigureAwait(true);

                string ProductosId = string.Empty;
                if (DeviceInfo.Platform == DevicePlatform.Android)
                    notifyIco = "notification.svg";
                else
                    notifyIco = "pngnotification.png";


                foreach (CardModel card in cardsResult)
                {
                    card.ImgProd = Constants.Url_Img_Base + "miinntecmovil/tarjetas/default/" + card.ImgProd;
                    Cards.Add(card);
                    ProductosId += (!ProductosId.Contains(card.ProductoID.ToString())) ? card.ProductoID.ToString() + "," : "";
                    
                }
                if (Cards.Count() > 1)
                {
                    EstadoLoop = "True";
                }
                else
                {
                    EstadoLoop = "False";
                }

                var newNotify = await DataNotify.verifyNewNotify(ProductosId);
                if (newNotify)
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                        notifyIco = "notificationr.svg";
                    else
                        notifyIco = "pngnotificationr.png";
                }
                    
                    //|Preferences.Default.Set("IconNotificacion", notifyIco);


                if (Cards.Any(item => item.ProductoID == (int)Enumeradores.enumProductoID.VIATICOS))
                {
                    MessagingCenter.Send<object, bool>(this, App.Viaticos, true);
                }

                if (Cards.Any(item => item.Complemento.ProductoGrupoId == (int)Enumeradores.enumProductoGrupo.COMBUSTIBLES))
                {
                    ROL_COMBUSTIBLE = true;
                }
                if (Cards.Any(item => item.Complemento.ProductoGrupoId == (int)Enumeradores.enumProductoGrupo.VIATICOS))
                {
                    ROL_VIATICOS = true;
                }
                App.Cards = cardsResult.ToList();
            }
            catch (Exception)
            {
                // _ = cardsPage.DisplayAlert("Error!", "No fue posible mostrar las tarjetas, intenta mas tarde", "Aceptar");
                if (primeraVez == false)
                {
                    primeraVez = true;
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                    InformativeViewModel.Instance.Title = "Advertencia";
                    InformativeViewModel.Instance.Message = "Tu sesión ha expirado , favor de volver a ingresar tu cuenta.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    App.Current.MainPage = new LoginPage(); //Estado de salida 401 por timeout de credenciales 
                }
                
            }
            finally
            {
                IsRefreshing = false;

            }
            //Preferences.Default.Set("IconNotificacion", notifyIco);

            ///<sumary>
            /// Se implemento una notificacion Local la cual al darle click te mandara al Centro de notificaciones de la aplicacionn
            /// </sumary>
            string FirtsPush = Preferences.Default.Get("FirtsTimePush", string.Empty);
            if (FirtsPush == "" || FirtsPush == null)
            {
                //if (IsBusy)
                //    return;

               
                List<NotifyUser> getNotify = await DataNotify.getNotifications(Constants.Products);
                if (getNotify.Count <= 0)
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                        notifyIco = "notification.svg";
                    else
                        notifyIco = "pngnotification.png";
                }
                else
                {
                    foreach (NotifyUser item in getNotify)
                    {
                        
                        Notifications.Add(item);
                        if (item.Estatus == "Leida")
                        {
                            /// Este apartado es solo para cambiar el SVG de CustomMenu cuando encuentre notificaciones que ya se hayan leido 
                        }
                        else
                        {
                            contadorNoti++;
                            var request = new NotificationRequest
                            {
                                NotificationId = item.NotificacionId,
                                Title = item.Titulo,
                                Subtitle = " ",
                                Description = item.Descripcion,
                                BadgeNumber = contadorNoti,
                                Image = new Plugin.LocalNotification.NotificationImage { ResourceName = "Images/innteclogo.png", FilePath = "innteclogo.png", Binary = null }, //Aqui se personaliza el logo de la notificacion debe de ser un archivo png
                                Schedule = new NotificationRequestSchedule
                                {
                                    NotifyTime = DateTime.Now.AddSeconds(1),
                                    NotifyRepeatInterval = TimeSpan.FromDays(1),
                                }
                            };
                            
                             LocalNotificationCenter.Current.Show(request);
                        }
                        if (contadorNoti == 0)
                        {
                            if (DeviceInfo.Platform == DevicePlatform.Android)
                                notifyIco = "notification.svg";
                            else
                                notifyIco = "pngnotification.png";
                        }
                        else 
                        {
                            if (DeviceInfo.Platform == DevicePlatform.Android)
                                notifyIco = "notificationr.svg";
                            else
                                notifyIco = "pngnotificationr.png";
                        }
                        Preferences.Default.Set("FirtsTimePush", "1");
                        Preferences.Default.Set("IconNotificacion", notifyIco);
                    }
                }
            }
            ///<sumary>
            /// Se implemento una notificacion Local la cual al darle click te mandara al Centro de notificaciones de la aplicacionn
            /// </sumary>
            ///
            IsBusy = false;
            // Preferences.Default.Set("IconNotificacion", notifyIco);
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
