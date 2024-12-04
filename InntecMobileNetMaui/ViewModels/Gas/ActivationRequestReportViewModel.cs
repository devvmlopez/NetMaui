using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Gas;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Gas;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Gas
{
    class ActivationRequestReportViewModel : BaseViewModel
    {
        /// <summary>
        /// Lista de activaciones
        /// </summary>

        //IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        public ObservableCollection<ActivationRequestReportModel> RequestList { get; set; }
        public Command LoadRequestReportCommand { get; set; }
        public Command ShowActivationRequestDetailsCommand { get; set; }
        private CardModel cardModel;
        ActivationRequestReportPage activationRequestReportPage;
        private List<string> lstStatus;
        public List<string> LstStatus
        {
            get => lstStatus; set => SetProperty(ref lstStatus, value);
        }

        private RequestActivationFilterModel filterModel;
        public RequestActivationFilterModel FilterModel { get => filterModel; set => SetProperty(ref filterModel, value); }

        /// <summary>
        /// Inicializacion de objetos para el reporte de activacion
        /// </summary>
        /// <param name="activationRequestReportPage">pagina del Binding</param>
        /// <param name="args">Datos de la tarjeta</param>
        public ActivationRequestReportViewModel(ActivationRequestReportPage activationRequestReportPage, CardModel args)
        {
            Title = "Solicitudes de combustible";
            cardModel = args;
            this.activationRequestReportPage = activationRequestReportPage;
            RequestList = new ObservableCollection<ActivationRequestReportModel>();
            FilterModel = new RequestActivationFilterModel();
            FilterModel.dateStart= DateTime.UtcNow.AddDays(-7);
            FilterModel.dateEnd = DateTime.UtcNow;
            FilterModel.statusId = 0;

            LoadRequestReportCommand = new Command(async () => await ExecuteLoadRequestReportCommand(cardModel).ConfigureAwait(true));

            ShowActivationRequestDetailsCommand = new Command((param) => ExecuteShowActivationRequestDetailsCommand((ActivationRequestReportModel)param));

            LstStatus = new List<string>();
            foreach (Enumeradores.enumSolicitudActivacionEstatus item in Enum.GetValues(typeof(Enumeradores.enumSolicitudActivacionEstatus)))
            {
                LstStatus.Add(item.ToString());
            }
        }
        /// <summary>
        /// navegacion al detalle de activacion del item seleccionado
        /// </summary>
        /// <param name="param">Reporte de activacion</param>
        private void ExecuteShowActivationRequestDetailsCommand(ActivationRequestReportModel param)
        {
            this.activationRequestReportPage.Navigation.PushAsync(new ActivationRequestReportDetailPage(param), true);
        }
        /// <summary>
        /// Listado de activaciones
        /// </summary>
        /// <param name="cardmodel">Datos de la tarjeta</param>
        /// <returns></returns>
        async Task ExecuteLoadRequestReportCommand(CardModel cardmodel)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                RequestList.Clear();
                var requestResult = await DataGas.GetActivationRequest(cardmodel.UsuarioCsmTarjetaId, FilterModel, true).ConfigureAwait(true);
                foreach (ActivationRequestReportModel itemRequest in requestResult)
                {
                    RequestList.Add(itemRequest);
                }
                if (RequestList.Count <= 0)
                {
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = "No fue posible mostrar ninguna solicitud con ese filtro de busqueda, intente con otros criterios de busqueda.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
                IsBusy = false;
            }
            catch (Exception)
            {
                //_ = activationRequestReportPage.DisplayAlert("Error!", "No fue posible mostrar el listado de solicitudes, intente mas tarde.", "Aceptar");
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Error!";
                InformativeViewModel.Instance.Message = "No fue posible mostrar el listado de solicitudes, intente mas tarde.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                IsBusy = false;
            }
        }
    }
}
