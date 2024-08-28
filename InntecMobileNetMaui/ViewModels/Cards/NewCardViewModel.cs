using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Views.CustomView;
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
        private void ExecuteSaveNewCardCommand()
        {
            if (AltaTarjeta) return;
            AltaTarjeta = true;

            this.CardModel.Tarjeta = this.CardModel.Tarjeta.Replace("-", "");
            MessagingCenter.Send(_newCardPage, "Nueva tarjeta", this.CardModel);

            AltaTarjeta = false;
        }
    }
}
