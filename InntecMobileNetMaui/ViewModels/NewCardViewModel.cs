using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.CustomView;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Cards
{
    /// <summary>
    /// Agregar nueva tarjeta
    /// </summary>
    public class NewCardViewModel : BaseViewModel
    {
        private NewCardPage _newCardPage;
        private CardModel _cardModel;
        bool AltaTarjeta = false;
        public CardModel CardModel
        {
            get => _cardModel;
            set => SetProperty(ref _cardModel, value);
        }

        public Command SaveNewCardCommand { get; set; }

        /// <summary>
        /// Inicializar objetos
        /// </summary>
        /// <param name="newCardPage"></param>
        /// <param name="_loginModel"></param>
        public NewCardViewModel(Views.CustomView.NewCardPage newCardPage)
        {
            Title = "Agregar tarjeta";
            this._newCardPage = newCardPage;
            this.CardModel = new CardModel { Anio = "Año", Mes = "Mes" };
            SaveNewCardCommand = new Command(() => ExecuteSaveNewCardCommand());
        }


        /// <summary>
        /// Guardar nueva tarjeta
        /// </summary>
        private async void ExecuteSaveNewCardCommand()
        {

            this.CardModel.Tarjeta = this.CardModel.Tarjeta.Replace("-", "");

            CardModel cardUpdate = await DataCard.AddItemV2Async(CardModel);
            if (cardUpdate.Message == null || cardUpdate.Message == "Tarjeta agregada con exito.")
            {
                InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Informative;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = "Tarjeta agregada con exito.";
                Task.Delay(3000).ConfigureAwait(true);
                await MopupService.Instance.PushAsync(InformativeAlert.Instance).ConfigureAwait(true);

            }
            else 
            { 
                InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = cardUpdate.Message;
                Task.Delay(3000).ConfigureAwait(true);
                await MopupService.Instance.PushAsync(InformativeAlert.Instance).ConfigureAwait(true);
            }
            Task.Delay(3000).ConfigureAwait(true);

        }
    }
}
