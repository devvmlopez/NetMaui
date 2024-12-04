using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.Services;
using InntecMobileNetMaui.Views.CustomView.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels
{
    public class CardNipViewModel : BaseViewModel
    {

        private CardNipPage _cardNipPage;
        private CardModel _cardModel;
        private CardNipModel _cardNipModel;
        bool _cambiarNip;

        public CardNipModel CardNipModel
        {
            get => _cardNipModel;
            set => SetProperty(ref _cardNipModel, value);
        }

        public string NipConfirm { get; set; }

        public Command ChangeNipCardCommand { get; internal set; }


        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="cardModel">datos de tarejta</param>
        /// <param name="cardNipPage">Pagina de binding</param>
        public CardNipViewModel(CardModel cardModel, Views.CustomView.Card.CardNipPage cardNipPage)
        {
            Title = "Nuevo NIP";
            this._cardModel = cardModel;
            this._cardNipPage = cardNipPage;

            CardNipModel = new CardNipModel
            {
                UsuarioCsmTarjetaId = cardModel.UsuarioCsmTarjetaId
            };

            ChangeNipCardCommand = new Command(() => ExecuteChangeNipCardCommand());
        }

        ///// <summary>
        ///// Cambiar NIP
        ///// </summary>
        private void ExecuteChangeNipCardCommand()
        {
            if (_cambiarNip)
            {
                return;
            }
            _cambiarNip = true;
            _cardNipModel.Nip = AesGcm.EncryptString(_cardNipModel.Nip, Constants.Token);
            MessagingCenter.Send(_cardNipPage, "Cambiar NIP", this._cardNipModel);
            _cambiarNip = false;
        }
    }
}
