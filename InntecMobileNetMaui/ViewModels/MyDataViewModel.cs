using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels
{
    /// <summary>
    /// Datos del usaurio
    /// </summary>
    class MyDataViewModel : BaseViewModel
    {
        private UserModel _userModel;
        public UserModel UserModel
        {
            get => _userModel; set => SetProperty(ref _userModel, value);
        }
        private MyDataPage myDataPage;
        public Command LoadUserData { set; get; }
        public Command SaveUserData { set; get; }

        public MyDataViewModel()
        {

        }

        /// <summary>
        /// Inicializar objetos
        /// </summary>
        /// <param name="myDataPage">Pagina de Binding</param>
        public MyDataViewModel(MyDataPage myDataPage)
        {
            Title = "Mis datos";

            this.UserModel = new UserModel();
            this.myDataPage = myDataPage;

            LoadUserData = new Command(() => ExecuteLoadUserData());
            SaveUserData = new Command(() => ExecuteSaveUserData());
        }

        /// <summary>
        /// Actualizacion de datos
        /// </summary>
        private async void ExecuteSaveUserData()
        {
            this.UserModel = await DataUser.SetUserDataAsync(UserModel).ConfigureAwait(true);
            //await myDataPage.DisplayAlert("Mensaje", this.UserModel.Message, "Aceptar").ConfigureAwait(true);

            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Message;
            InformativeViewModel.Instance.Title = "Ha ocurrido un problema!";
            InformativeViewModel.Instance.Message = this.UserModel.Message;
            await MopupService.Instance.PushAsync(InformativeAlert.Instance);

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
