using InntecMobileNetMaui.Models.Viatics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Viatics
{
    class ViaticDetailViewModel : BaseViewModel
    {
        private List<DetailsViaticsRequest> _detail;

        public ObservableCollection<DetailsViaticsRequest> ItemDetail { get; set; }
        public Command LoadViaticsDetail { get; set; }
        /// <summary>
        ///  Inicializacion de objetos
        /// </summary>
        /// <param name="Detail">Listado de detalles de solicitud</param>
        public ViaticDetailViewModel(List<DetailsViaticsRequest> Detail)
        {
            Title = "Detalle";
            this._detail = Detail;
            ItemDetail = new ObservableCollection<DetailsViaticsRequest>();
            LoadViaticsDetail = new Command(() => ExecuteLoadViaticsDetail());
        }
        /// <summary>
        /// Destalle de solicitud
        /// </summary>
        private void ExecuteLoadViaticsDetail()
        {
            foreach (DetailsViaticsRequest item in this._detail)
            {
                ItemDetail.Add(item);
            }
        }
    }
}
