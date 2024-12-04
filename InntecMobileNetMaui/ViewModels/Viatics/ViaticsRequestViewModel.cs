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
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Viatics
{
    class ViaticsRequestViewModel : BaseViewModel
    {
        CardModel cardModel;
        private ViaticsRequestPage _viaticsRequestPage;
        private NewViaticsRequest _newViaticsRequest;
        public Command NewRequest { get; set; }
        public Command AddItem { get; internal set; }
        public Command DeleteItem { get; internal set; }
        public Command SaveRequest { get; internal set; }

        private int idCardViatic;
        public int IdCardViatic
        {
            get => idCardViatic;
            set => SetProperty(ref idCardViatic, value);
        }

        private InfoNewRequest _infoNewRequest;
        public InfoNewRequest InfoNewRequest
        {
            get => _infoNewRequest; set => SetProperty(ref _infoNewRequest, value);
        }
        public NewViaticsRequest NewViaticsRequest
        {
            get => _newViaticsRequest;
            set => SetProperty(ref _newViaticsRequest, value);
        }

        public ObservableCollection<DetailViatic> DetailViatic { get; set; }

        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="viaticsRequestPage">pagina de binding</param>
        public ViaticsRequestViewModel(ViaticsRequestPage viaticsRequestPage, CardModel cardModel)
        {
            Title = "Solicitud de viaticos";
            this._viaticsRequestPage = viaticsRequestPage;
            
            idCardViatic = cardModel.UsuarioCsmTarjetaId;
            
            this.InfoNewRequest = new InfoNewRequest();

            this.NewViaticsRequest = new NewViaticsRequest()
            {
                FechaIni = DateTime.UtcNow,
                FechaFin = DateTime.UtcNow
            };
            this.DetailViatic = new ObservableCollection<DetailViatic>();

            NewRequest = new Command((param) => ExecuteNewRequest(idCardViatic));
            AddItem = new Command((param) => ExecuteAddItem(param as List<object>));
            DeleteItem = new Command((param) => ExecuteDeleteItem(param as DetailViatic));
            SaveRequest = new Command(ExecuteSaveRequest);
            MessagingCenter.Unsubscribe<ViaticsRequestPage, List<object>>(this, App.ViaticoDetalle);
            MessagingCenter.Subscribe<ViaticsRequestPage, List<object>>(this, App.ViaticoDetalle, async (obj, item) =>
            {
                InfoCity ICity = (InfoCity)item[0];
                InfoItem IItem = ((InfoItem)item[1]);
                if (this.DetailViatic.Any(detail => detail.IDUnico == ICity.IdUnico && detail.RubroId == IItem.RubroId))
                {
                    //await _viaticsRequestPage.DisplayAlert("Mensaje", "Ya se agrego este rubro para la ciudad.", "Aceptar").ConfigureAwait(true);
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = "Ya se agrego este rubro para la ciudad.";
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                }
                else
                {
                    this.DetailViatic.Add(new DetailViatic
                    {
                        CiudadDesc = ICity.Nombre,
                        IDUnico = ICity.IdUnico,
                        MontoSolicitado = decimal.Parse(item[2].ToString()),
                        RubroId = IItem.RubroId,
                        RubroDesc = IItem.Descripcion
                    });
                }
            });
        }
        /// <summary>
        /// Crear solicitud de viaticos
        /// </summary>
        async void ExecuteSaveRequest()
        {
            IsBusy = true;
            if (DetailViatic.ToList().Count > 0)
            {
                NewViaticsRequest.ListadoDetalleViatico = DetailViatic.ToList();
                this.NewViaticsRequest.UsuarioCsmTarjetaId = this.InfoNewRequest.UsuarioCsmTarjetaId;
                this.NewViaticsRequest.EmpleadoId = this.InfoNewRequest.EmpleadoId;

                InfoNewRequest request = await DataViatics.SetViaticsRequestAsync(NewViaticsRequest).ConfigureAwait(true);
                if (request.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //await _viaticsRequestPage.DisplayAlert("Mensaje", request.Message, "Aceptar").ConfigureAwait(true);

                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Message;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = request.Message;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                    await _viaticsRequestPage.Navigation.PopToRootAsync().ConfigureAwait(true);

                }
                else
                {
                    //await _viaticsRequestPage.DisplayAlert("Alerta!", request.Message, "Aceptar").ConfigureAwait(true);
                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                    InformativeViewModel.Instance.Title = "Alerta!";
                    InformativeViewModel.Instance.Message = request.Message;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                }
            }
            else 
            {
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "Es necesario crear una solicitud con al menos un destino y un presupuesto valido ";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
            }
            IsBusy = false;

        }
        /// <summary>
        /// Eliminar detalle
        /// </summary>
        /// <param name="detailViatic">Detalle de solicitud</param>
        private void ExecuteDeleteItem(DetailViatic detailViatic)
        {
            DetailViatic.Remove(detailViatic);
        }
        /// <summary>
        /// Agregar rubros
        /// </summary>
        /// <param name="param"></param>
        void ExecuteAddItem(List<object> param)
        {
            MessagingCenter.Send(this._viaticsRequestPage, App.ViaticoDetalle, param);
        }
        /// <summary>
        /// Crear nueva solicitud
        /// </summary>
        async void ExecuteNewRequest(int idCardViatic)
        {

            this.InfoNewRequest = await DataViatics.GetViaticsRequestAsyncV2(idCardViatic).ConfigureAwait(true);

            IsBusy = false;
        }
    }
}
