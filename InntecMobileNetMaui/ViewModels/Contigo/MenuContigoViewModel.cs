using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Services;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Contigo
{
    class MenuContigoViewModel : BaseViewModel
    {
        private UserModel _userModel;
        public UserModel UserModel
        {
            get => _userModel; set => SetProperty(ref _userModel, value);
        }
        private string _beneficios = null;
        public string beneficios { get => _beneficios; set => SetProperty(ref _beneficios, value); }

        private string _plan = "false";
        public string plan { get => _plan; set => SetProperty(ref _plan, value); }

        public Command suscribirContigoCommand { get; set; }
        public Command BeneficiosContigoCommand { get; set; }

        public Command LoadUserData { set; get; }
        public Command LoadUserDataTest { set; get; }
        public MenuContigoViewModel()
        {
            beneficios = "false";
            this.UserModel = new UserModel();


            if (plan == "false")
                loadBeneficios();

            suscribirContigoCommand = new Command(async () => await ExecuteSubcribirContigoCommand().ConfigureAwait(true));
            BeneficiosContigoCommand = new Command(async () => await ExecuteBeneficiosContigoCommand(UserModel).ConfigureAwait(true));
            LoadUserData = new Command(() => ExecuteLoadUserData());

        }
        private async void loadBeneficios()
        {

            IsBusy = true;
            var popup = new YesOrNotAlert();
            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Mensaje";
            InformativeViewModel.Instance.Message = "¿ Deseas suscribirte a INNTEC CONTIGO ?";

            await MopupService.Instance.PushAsync(popup);
            var rvalue = await popup.PopupDismissedTask;

            if (rvalue == "YES")
            {
                beneficios = "false";
                //Aqui mando a llamada el API y ejecuto el metodo Suscribirse
                var ListaDeBeneficios = await ContigoServices.SuscripcionBeneficionsContigoAsync(UserModel).ConfigureAwait(true); 
                plan = "true";
            }
            else 
            {
                beneficios = "true";
            }
            IsBusy = false;
        }

        private async Task ExecuteSubcribirContigoCommand()
        {
            IsBusy = true;
            var popup = new YesOrNotAlert();
            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Mensaje";
            InformativeViewModel.Instance.Message = "¿ Deseas suscribirte a INNTEC CONTIGO ?";

            await MopupService.Instance.PushAsync(popup);
            var rvalue = await popup.PopupDismissedTask;

            if (rvalue == "YES")
            {
                beneficios = "false";
                //Aqui mando a llamada el API y ejecuto el metodo Subcribirse
                var Suscripcion = await ContigoServices.SuscripcionBeneficionsContigoAsync(UserModel).ConfigureAwait(true);
                plan = "true";
            }
            else
            {

            }
            IsBusy = false;
        }
        private async Task ExecuteBeneficiosContigoCommand(UserModel userModel)
        {
            IsBusy = true;
            var popup = new YesOrNotAlert();
            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Mensaje";
            InformativeViewModel.Instance.Message = "¿ Deseas suscribirte a INNTEC CONTIGO ?";

            await MopupService.Instance.PushAsync(popup);
            var rvalue = await popup.PopupDismissedTask;

            if (rvalue == "YES")
            {
                beneficios = "false";
                //Aqui mando a llamada el API y ejecuto el metodo Suscribirse
                var listaBeneficios = await ContigoServices.CheckBeneficionsContigoAsync(UserModel).ConfigureAwait(true);
                
                plan = "true";
            }
            else
            {
                beneficios = "true";
            }
            IsBusy = false;
        }

        /// <summary>
        /// Carga de datos
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<UserModel> ExecuteLoadUserData(LoginModel loginModel)
        {
            this.UserModel = await DataUser.GetUserDataAsync().ConfigureAwait(true);
            return this.UserModel;
        }

        /// <summary>
        /// Datos del usuario
        /// </summary>
        private async void ExecuteLoadUserData()
        {
            IsBusy = true;
            this.UserModel = await DataUser.GetUserDataAsync().ConfigureAwait(true);
            IsBusy = false;
        }

    }
}
