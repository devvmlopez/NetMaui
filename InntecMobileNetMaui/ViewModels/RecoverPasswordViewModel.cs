using System.Threading.Tasks;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;

namespace InntecMobileNetMaui.ViewModels
{
    class RecoverPasswordViewModel : BaseViewModel
    {
        RecoverPasswordModel _recoverPasswordModel;
        private RecoverPasswordPage _recoverPasswordPage;

        public RecoverPasswordModel RecoverPasswordModel
        {
            get => _recoverPasswordModel; set => SetProperty(ref _recoverPasswordModel, value);
        }

        public Command CancelRecoveryPassword { get; set; }

        /// <summary>
        /// Inicializar objetos
        /// </summary>
        /// <param name="recoverPasswordPage"></param>
        public RecoverPasswordViewModel(Views.RecoverPasswordPage recoverPasswordPage)
        {
            this._recoverPasswordPage = recoverPasswordPage;
            RecoverPasswordModel = new RecoverPasswordModel();
            CancelRecoveryPassword = new Command(() => ExecuteCancelRecoveryPassword());
        }

        /// <summary>
        /// Cancelar recuparacion de password
        /// </summary>
        private void ExecuteCancelRecoveryPassword()
        {
            //_recoverPasswordPage.Navigation.PopModalAsync();
             Shell.Current.GoToAsync("//Login");
        }

        ///// <summary>
        ///// Solicitar recuperacion de password  V2
        ///// </summary>
        internal async Task SendRecorveryAsync(string token)
        {
            if (_recoverPasswordModel.Email != null && _recoverPasswordModel.UserName != null) 
            {
                var recoverPasswordModel =
                        await DataUser.RecoverUserPassV2Async(
                            _recoverPasswordModel,
                            token,
                            (DeviceInfo.Platform == DevicePlatform.Android) ? 1 : 2).ConfigureAwait(true);

                if (recoverPasswordModel.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                    InformativeViewModel.Instance.Title = "Mensaje";
                    InformativeViewModel.Instance.Message = recoverPasswordModel.Message;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                    CancelRecoveryPassword.Execute(null);
                    IsBusy = false;
                }
                else
                {

                    InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                    InformativeViewModel.Instance.Title = "Alerta!";
                    InformativeViewModel.Instance.Message = recoverPasswordModel.Message;
                    await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                    IsBusy = false;
                }
            }
            else
            {
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Alerta!";
                InformativeViewModel.Instance.Message = "Es necesario contar con el Nombre y Email para realizar la solicitud. ";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                IsBusy = false;
            }
        }
    }
}
