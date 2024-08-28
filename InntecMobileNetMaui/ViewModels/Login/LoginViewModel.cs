using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.Cards;
using InntecMobileNetMaui.Views.Login;
using Mopups.Services;

namespace InntecMobileNetMaui.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private LoginPage _loginPage;
        private string _usuario;
        public string Usuario { get => _usuario; set => SetProperty(ref _usuario, value); }

        private string _contrasena;
        public string Contrasena { get => _contrasena; set => SetProperty(ref _contrasena, value); }

        public LoginPage LoginPage { get => _loginPage; set => _loginPage = value; }

        private bool _rememberPWS;
        public bool RememberPWS { get => _rememberPWS; set => SetProperty(ref _rememberPWS, value); }

        private bool _loginPass;
        public bool LoginPass
        {
            get => _loginPass; set => SetProperty(ref _loginPass, value);
        }

        private bool loginBiometrico;
        public bool LoginBiometrico
        {
            get => loginBiometrico; set => SetProperty(ref loginBiometrico, value);
        }

        private bool _showError;
        public bool ShowError
        {
            get => _showError; set => SetProperty(ref _showError, value);
        }

        private bool _fingerPrint;
        public bool FingerPrint
        {
            get => _fingerPrint; set => SetProperty(ref _fingerPrint, value);
        }
        public string User { get; internal set; }

        private string _error_description;
        public string Error_description { get => _error_description; set => SetProperty(ref _error_description, value); }

        private bool _esPrimerIntento;
        public bool EsPrimerIntento
        {
            get => _esPrimerIntento; set => SetProperty(ref _esPrimerIntento, value);
        }

        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        public LoginViewModel()
        {
            ShowError = false;
            EsPrimerIntento = true;
        }

        /// <summary>
        /// Inicio de sesion
        /// </summary>
        /// <returns></returns>
       // [Obsolete] // Se agrego para testiar pero se ocupa refactorizar a Maui
        internal async Task Login(string token, int dispocitivo)
        {
            string psw;
            Error_description = string.Empty;
            if (!LoginBiometrico)
            {
                Constants.UserName = _usuario;
                psw = Constants.Psw = _contrasena;
                Constants.rememberPSW = _rememberPWS;
            }
            else
            {
                if (string.IsNullOrEmpty(Constants.Psw) || string.IsNullOrEmpty(Constants.Token))
                {
                    // TODO Revisar si es viable 
                    //Constants.UserName = "";
                    //psw = Constants.Psw = "";
                    //LoginBiometrico = false;

                    throw new System.Exception("Es necesario ingresar nuevamente usuario y contraseña");
                }
                else
                {
                    psw = Services.AesGcm.DecryptString(Constants.Psw, Constants.Token);
                }

            }
            LoginModel result = new LoginModel
            {
                Usuario = Constants.UserName,
                Password = psw,
                rememberPWS = Constants.rememberPSW
            };

            result = await DataUser.LoginAsync(result, token, dispocitivo).ConfigureAwait(true);
        
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                // Estado de aceptacion
                Error_description = Constants.Error_Descipcion;
                IsBusy = false;
                ShowError = true;
                //App.Current.MainPage = new CardPageList();
                
                await Shell.Current.GoToAsync("//CardsPage");

            }
            else
            {
                //Mensajes de error generados por la session 
                InformativeViewModel.Instance.MessageType = Alerts.InformativeViewModel.messageType.Error;
                InformativeViewModel.Instance.Title = "Ha ocurrido un problema!";
                InformativeViewModel.Instance.Message = Constants.Error_Descipcion;
                await MopupService.Instance.PushAsync(InformativeAlert.Instance);

                Error_description = Constants.Error_Descipcion;
                IsBusy = false;
                ShowError = true;
                EsPrimerIntento = false;

            }
        }
    }
}
