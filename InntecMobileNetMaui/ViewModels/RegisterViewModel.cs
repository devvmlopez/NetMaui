using Acr.UserDialogs;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Login;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private UserV2Model usr;
        private RegisterPage registerPage;

        public UserV2Model Usr { get => usr; set => SetProperty(ref usr, value); }
        public Command TermsCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public RegisterViewModel(Views.RegisterPage registerPage)
        {
            this.registerPage = registerPage;
            Usr = new UserV2Model();

            TermsCommand = new Command(() => ExcecuteTarmsCommand());
        }

        private async void ExcecuteTarmsCommand()
        {
           await registerPage.Navigation.PushAsync(new TermsAndConditionsPage(true));
        }

        internal async Task RegiterV2(string token)
        {

            var result = await DataUser.RegisterV2Async(usr, token, DeviceInfo.Platform == DevicePlatform.Android ? 1 : 2);

            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Informative;
                InformativeViewModel.Instance.Title = "Confirmación";
                InformativeViewModel.Instance.Message = "Hemos enviado un enlace a la dirección de correo electrónico registrada";
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                IsBusy = false;
                App.Current.MainPage = new LoginPage();
            }
            else
            {

                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "No se han superado las validaciones";
                InformativeViewModel.Instance.Message = result.Message;
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);
                IsBusy = false;

            }
        }
    }
}
