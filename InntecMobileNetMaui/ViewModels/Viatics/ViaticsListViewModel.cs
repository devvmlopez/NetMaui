using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Viatics;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Viatics;
using Microsoft.Maui;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Viatics
{
    public class ViaticsListViewModel : BaseViewModel
    {
        private CardModel cardModel;
        private IEnumerable<object> ListResult = new List<object>();
        private ViaticsListPage _viaticsListPage;
        private ViaticsFilterModel filterModel;
        public ViaticsFilterModel FilterModel { get => filterModel; set => SetProperty(ref filterModel, value); }
        public ObservableCollection<ViaticsRequest> ListViaticsRequest { get; set; }
        public Command LoadViaticsRequestListRequest { get; set; }

        public Command DetailViaticsRequestListRequest { get; set; }
        public Command ShowViaticsDetail { get; set; }
        private List<string> lstStatus;
        public List<string> LstStatus
        {
            get => lstStatus; set => SetProperty(ref lstStatus, value);
        }
        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="viaticsListPage">Pagina del binding</param>
        public ViaticsListViewModel(ViaticsListPage viaticsListPage , CardModel args)
        {
            Title = "Solicitudes";
            this._viaticsListPage = viaticsListPage;
            this.ListViaticsRequest = new ObservableCollection<ViaticsRequest>();
            cardModel = args;

            FilterModel = new ViaticsFilterModel();
            FilterModel.dateStart = DateTime.UtcNow.AddDays(-7);
            FilterModel.dateEnd = DateTime.UtcNow;
            FilterModel.statusId = 0;

            LoadViaticsRequestListRequest = new Command(async () => await ExecuteGetViaticsListRequest(cardModel).ConfigureAwait(true));
            DetailViaticsRequestListRequest = new Command(async (status) => await ExecuteGetDetailViaticsListRequest(status, cardModel).ConfigureAwait(true));
            ShowViaticsDetail = new Command((param) => ExecuteShowViaticsDetail((List<DetailsViaticsRequest>)param));

            LstStatus = new List<string>();
            foreach (Enumeradores.enumStatusSolicitud item in Enum.GetValues(typeof(Enumeradores.enumStatusSolicitud)))
            {
                LstStatus.Add(item.ToString());
            }
        }
        /// <summary>
        /// Mostrar detalles de solicitud
        /// </summary>
        /// <param name="detailsViatics">Datos del detalle</param>
        private void ExecuteShowViaticsDetail(List<DetailsViaticsRequest> detailsViatics)
        {

            this._viaticsListPage.Navigation.PushAsync(new ViaticDetailPage(detailsViatics), true);
        }
        /// <summary>
        /// Listado de solicitudes
        /// </summary>
        /// <param name="status">Estatus de la solicitud a filtrar</param>
        /// <returns></returns>
        async Task ExecuteGetViaticsListRequest(CardModel args)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            //bool oneTime = false;
            //Enumeradores.enumStatusSolicitud statusSolicitud = Enumeradores.enumStatusSolicitud.NUEVA;
            //if (status != null)
            //    statusSolicitud = (Enumeradores.enumStatusSolicitud)Enum.Parse(typeof(Enumeradores.enumStatusSolicitud), status.ToString());
 
                ListResult = await DataViatics.GetViaticsListAsync(FilterModel, args.UsuarioCsmTarjetaId).ConfigureAwait(true);
                List<object> LstTmp = ListResult.ToList();
                this.ListViaticsRequest.Clear();
                if ((HttpStatusCode)LstTmp[1] == HttpStatusCode.OK)
                {
                    foreach (ViaticsRequest item in (IEnumerable<ViaticsRequest>)LstTmp[0])
                    {
                        this.ListViaticsRequest.Add(item);
                    }
                    IsBusy = false;
                }
                else
                {
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = "No fue posible mostrar ninguna solicitud con ese filtro de busqueda, intente con otros criterios de busqueda.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                    IsBusy = false;
                }
                
        
        }

        async Task ExecuteGetDetailViaticsListRequest(object status, CardModel args)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ListResult = await DataViatics.GetViaticsListAsync(FilterModel, args.UsuarioCsmTarjetaId).ConfigureAwait(true);
            List<object> LstTmp = ListResult.ToList();
            this.ListViaticsRequest.Clear();
            if ((HttpStatusCode)LstTmp[1] == HttpStatusCode.OK)
            {
                foreach (ViaticsRequest item in (IEnumerable<ViaticsRequest>)LstTmp[0])
                {
                    if (item.EstatusSolicitud == status.ToString())
                    {
                        this.ListViaticsRequest.Add(item);
                    }
                    else if (status.ToString() == "TODO")
                    {
                        this.ListViaticsRequest.Add(item);
                    }
                }

                if (ListViaticsRequest.Count <= 0)
                {
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = "No fue posible mostrar ninguna solicitud con ese filtro de busqueda, intente con otros criterios de busqueda.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
                IsBusy = false;
            }
            else
            {
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = "No fue posible mostrar ninguna solicitud con ese filtro de busqueda, intente con otros criterios de busqueda.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                IsBusy = false;
            }
        }
    }
}
