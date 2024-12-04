using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Viatics;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Viatics;
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
    class ViaticsCheckViewModel : BaseViewModel
    {
        public ObservableCollection<ConsumptionDetail> viaticsChecList { get; set; }
        private ViaticsCheckPage _viaticsCheckPage;
        private CardModel cardModel;
        ViaticsListModel viaticsListModel;
        internal string status;

        public ViaticsListModel ViaticsListModel
        {
            get => viaticsListModel; set => SetProperty(ref viaticsListModel, value);
        }
        public Command ViaticsConsumptionDetail { get; internal set; }

        public Command FiltrerViaticsConsumptionDetail { get; internal set; }
        public Command ShowViaticsCheckDetail { get; internal set; }
        public List<string> LstStatus { get; set; }
        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="viaticsCheckPage">pagina del binding</param>
        public ViaticsCheckViewModel(ViaticsCheckPage viaticsCheckPage , CardModel args)
        {
            Title = "Comprobacion";
            this._viaticsCheckPage = viaticsCheckPage;
            this.viaticsChecList = new ObservableCollection<ConsumptionDetail>();
            cardModel = args;
            status = string.Empty;
            viaticsListModel = new ViaticsListModel() { folio = string.Empty, status = 0, fechaDesde = DateTime.UtcNow.AddDays(-7), fechaHasta = DateTime.UtcNow };

            ViaticsConsumptionDetail = new Command(async () => await ExecuteViaticsConsumptionDetail(cardModel).ConfigureAwait(true));
            FiltrerViaticsConsumptionDetail = new Command(async (status) => await ExecuteFilterViaticsConsumptionDetail(status , cardModel).ConfigureAwait(true));
            ShowViaticsCheckDetail = new Command((param) => ExecuteShowViaticsCheckDetail((ConsumptionDetail)param));

            LstStatus = new List<string>();
            foreach (Enumeradores.enumComprobacionEstatus item in Enum.GetValues(typeof(Enumeradores.enumComprobacionEstatus)))
            {
                LstStatus.Add(item.ToString().Replace('_', ' '));
            }
        }
        /// <summary>
        /// Detalles la comprobacion
        /// </summary>
        /// <param name="param">Detalles del consumo</param>
        private void ExecuteShowViaticsCheckDetail(ConsumptionDetail param)
        {
            this._viaticsCheckPage.Navigation.PushAsync(new ViaticsCheckDetailPage(param), true);
        }
        /// <summary>
        /// Agregar detalle al consumo
        /// </summary>
        /// <returns></returns>
        async Task ExecuteViaticsConsumptionDetail(CardModel args)
        {
            IsBusy = true;

            if (!string.IsNullOrEmpty(status))
                viaticsListModel.status = (int)(Enumeradores.enumComprobacionEstatus)Enum.Parse(typeof(Enumeradores.enumComprobacionEstatus), status.Replace(' ', '_'));

            IEnumerable<object> Result = await DataViatics.GetViaticsConsumptionDetailAsync(viaticsListModel, args.UsuarioCsmTarjetaId).ConfigureAwait(true);

            List<object> LstTmp = Result.ToList();
            this.viaticsChecList.Clear();
            if ((HttpStatusCode)LstTmp[1] == HttpStatusCode.OK)
            {
                foreach (ConsumptionDetail item in (IEnumerable<ConsumptionDetail>)LstTmp[0])
                {
                    this.viaticsChecList.Add(item);
                }

                if (viaticsChecList.Count <= 0)
                {
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = "No fue posible mostrar ninguna solicitud con ese filtro de búsqueda, intenta con otro criterio de búsqueda.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
                IsBusy = false;
            }
            else
            {
                //await _viaticsCheckPage.DisplayAlert("Alerta!", "Se a producido un error, intentalo mas tarde.", "Aceptar").ConfigureAwait(true);
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "Se a producido un error, intentalo mas tarde.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
            }

            await Task.Delay(100).ConfigureAwait(true);
            IsBusy = false;
        }


        async Task ExecuteFilterViaticsConsumptionDetail(object status, CardModel args)
        {
            IsBusy = true;

            //if (!string.IsNullOrEmpty(status))
            //    viaticsListModel.status = (int)(Enumeradores.enumComprobacionEstatus)Enum.Parse(typeof(Enumeradores.enumComprobacionEstatus), status.Replace(' ', '_'));

            IEnumerable<object> Result = await DataViatics.GetViaticsConsumptionDetailAsync(viaticsListModel, args.UsuarioCsmTarjetaId).ConfigureAwait(true);

            List<object> LstTmp = Result.ToList();
            this.viaticsChecList.Clear();
            if ((HttpStatusCode)LstTmp[1] == HttpStatusCode.OK)
            {
                foreach (ConsumptionDetail item in (IEnumerable<ConsumptionDetail>)LstTmp[0])
                {

                    if (item.Status == status.ToString())
                    {
                        this.viaticsChecList.Add(item);
                    }
                    else if (status.ToString() == "Todo")
                    {
                        this.viaticsChecList.Add(item);
                    }
                }

                if (viaticsChecList.Count <= 0)
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
                //await _viaticsCheckPage.DisplayAlert("Alerta!", "Se a producido un error, intentalo mas tarde.", "Aceptar").ConfigureAwait(true);
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "Se a producido un error, intentalo mas tarde.";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
            }

            await Task.Delay(100).ConfigureAwait(true);
            IsBusy = false;
        }
    }
}
