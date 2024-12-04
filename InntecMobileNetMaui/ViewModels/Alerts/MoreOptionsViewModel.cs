using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using InntecMobileNetMaui.Views.Login;
using Microsoft.Maui.Controls;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InntecMobileNetMaui.ViewModels.Alerts
{
    class MoreOptionsViewModel : BaseViewModel
    {
        CardsPage RootPage => Application.Current.MainPage as CardsPage;
        //internal MainViewModel mainViewModel;

        private CardModel _cardModel;
        public CardModel cardModel { get { return _cardModel; } set { SetProperty(ref _cardModel, value); } }

        //public ObservableCollection<CardModel> Cards { get; set; }
        public MoreOptionsPage moreOptionsPage { get; private set; }

        public string _estatusTarjeta;
        public string EstatusTarjeta
        {
            get => _estatusTarjeta;
            set => SetProperty(ref _estatusTarjeta, value);
        }
        public string _estatusEcommers;
        public string EstatusEcommers
        {
            get => _estatusEcommers;
            set => SetProperty(ref _estatusEcommers, value);
        }

        public string _iconestatusTarjeta;
        public string IconEstatusTarjeta
        {
            get => _iconestatusTarjeta;
            set => SetProperty(ref _iconestatusTarjeta, value);
        }
        public string _iconestatusEcommers;
        public string IconEstatusEcommers
        {
            get => _iconestatusEcommers;
            set => SetProperty(ref _iconestatusEcommers, value);
        }
        public Command BlockCardCommand { get; set; }
        public Command BlockEcommerceCommand { get; set; }
        public MoreOptionsViewModel(MoreOptionsPage moreOptionsPage, CardModel cardModel)
        {
            this.cardModel = cardModel;
            this.moreOptionsPage = moreOptionsPage;

            //Cards = new ObservableCollection<CardModel>();

            if (cardModel.EstatusEcommerce == true)
            {
                EstatusEcommers = "Habilitado";
                IconEstatusEcommers = "true";
            }
            else
            { 
                EstatusEcommers = "Deshabilitado";
                IconEstatusEcommers = "false";
            }

            if (cardModel.EstatusDescripcion == "Activa")
            { 
                EstatusTarjeta = "Estatus Activa";
                IconEstatusTarjeta = "true";
            }
            else 
            { 
                EstatusTarjeta = "Estatus Bloqueo Temporal";
                IconEstatusTarjeta = "false";
            }

            //mainViewModel = new MainViewModel(RootPage);
            BlockCardCommand = new Command(async () => await ExecuteBlockCardCommand().ConfigureAwait(true));
            BlockEcommerceCommand = new Command(async () => await ExecuteBlockEcommerceCommand().ConfigureAwait(true));
        }

        private async Task ExecuteBlockEcommerceCommand()
        {
            IsBusy = true;
                CardModel cardUpdate = await DataCard.BlockEcommerceItemAsync(cardModel).ConfigureAwait(true);

                if (cardUpdate.Message == "Ocurrio un problema con el cambio de estatus.")
                {
                    await Shell.Current.GoToAsync("//CardsPage");
                }
                else 
                { 
                    if (cardUpdate.EstatusEcommerce == true)
                    {
                        EstatusEcommers = "Habilitado";
                        IconEstatusEcommers = "true";
                    }
                    else
                    {
                        EstatusEcommers = "Deshabilitado";
                        IconEstatusEcommers = "false";
                    }
                }
                //await Shell.Current.GoToAsync("//CardsPage"); ;
                //MessagingCenter.Send(moreOptionsPage, Constants.LoadCards, cardModel);
                InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = cardUpdate.Message;
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                
                //await moreOptionsPage.DisplayAlert("Mensaje", cardUpdate.Message, "Aceptar").ConfigureAwait(true);
        
            IsBusy = false;
        }

        private async Task ExecuteBlockCardCommand()
        {
            IsBusy = true;
                
                CardModel cardUpdate = await DataCard.BlockItemV2Async(cardModel).ConfigureAwait(true);
                if (cardUpdate.Message != "El estatus de la tarjeta ha sido actualizado con exito.")
                {
                    await Shell.Current.GoToAsync("//CardsPage");
                }
                else
                {
                    if (EstatusTarjeta == "Estatus Activa")
                    {
                        EstatusTarjeta = "Estatus Bloqueo Temporal";
                        IconEstatusTarjeta = "false";
                    }
                    else
                    {
                        EstatusTarjeta = "Estatus Activa";
                        IconEstatusTarjeta = "true";
                    }
                }
                //await Shell.Current.GoToAsync("//CardsPage");
                //MessagingCenter.Send(moreOptionsPage, Constants.LoadCards, cardModel);
                InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = cardUpdate.Message;
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);

               
                //await moreOptionsPage.DisplayAlert("Mensaje", cardUpdate.Message, "Aceptar").ConfigureAwait(true);
            IsBusy = false;
        }

    }
}
