using InntecMobileNetMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.BenefitHub
{
    public class BenefitHubRegisterViewModel : BaseViewModel
    {
        private BenefitHubModel _requestResult;
        private BenefitHubModel _requestNewUserResult;

        public UserModel UserModel { get; private set; }

        public BenefitHubModel RequestResult
        {
            get => _requestResult; set => SetProperty(ref _requestResult, value);
        }
        public BenefitHubModel RequestNewResult
        {
            get => _requestNewUserResult; set => SetProperty(ref _requestNewUserResult, value);
        }

        private string email;
        public string Email { get => email; set => SetProperty(ref email, value); }

        private string name;
        public string Name { get => name; set => SetProperty(ref name, value); }

        private string lastName;
        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }

        private bool registerPass;
        public bool RegisterPass { get => registerPass; set => SetProperty(ref registerPass, value); }

        private string password;
        public string Password { get => password; set => SetProperty(ref password, value); }

        private string confirmPassword;
        public string ConfirmPassword { get => confirmPassword; set => SetProperty(ref confirmPassword, value); }

        private string codigoPostal;
        public string CodigoPostal { get => codigoPostal; set => SetProperty(ref codigoPostal, value); }

        private bool largo;
        public bool Largo { get => largo; set => SetProperty(ref largo, value); }
        private bool mayus;
        public bool Mayus { get => mayus; set => SetProperty(ref mayus, value); }
        private bool numero;
        public bool Numero { get => numero; set => SetProperty(ref numero, value); }
        private bool caracter;
        public bool Caracter { get => caracter; set => SetProperty(ref caracter, value); }

        private bool loginPage;
        public bool LoginPage { get => loginPage; set => SetProperty(ref loginPage, value); }
        private bool passwordPage;
        public bool PasswordPage { get => passwordPage; set => SetProperty(ref passwordPage, value); }

        public Command BenefitHubCommand { get; private set; }
        public Command GetUserDataCommand { get; private set; }

        Views.BenefitHub.BenefitHubRegisterPage _benefitHubRegisterPage;
        InntecMobileNetMaui.Views.Cards.CardsPage _cardsPage;
        /// <summary>
        /// Inicializacion de objetos
        /// </summary>
        /// <param name="benefitHubRegisterPage"></param>
        public BenefitHubRegisterViewModel(Views.BenefitHub.BenefitHubRegisterPage benefitHubRegisterPage,
            InntecMobileNetMaui.Views.Cards.CardsPage cardsPage)
        {
            RegisterPass = false;
            Title = "Registro para beneficios";
            _benefitHubRegisterPage = benefitHubRegisterPage;
            _cardsPage = cardsPage;
            Largo = true;
            Mayus = true;
            Numero = true;
            Caracter = true;
            loginPage = false;
            passwordPage = false;
            IsBusy = true;

            Password = string.Empty;

            GetUserDataCommand = new Command(async (webViewLogin) => await ExecuteGetUserData((RendererControls.WebViewRenderer)webViewLogin).ConfigureAwait(true));

            BenefitHubCommand = new Command(async (webViewLogin) => await ExecutBenefitHubCommand((RendererControls.WebViewRenderer)webViewLogin).ConfigureAwait(true));
        }

        /// <summary>
        /// Reglas para el password
        /// </summary>
        internal void rules()
        {
            Largo = true;
            Mayus = true;
            Numero = true;
            Caracter = true;

            if (Password.Length >= 8)
                Largo = false;

            System.Text.RegularExpressions.Regex expReg = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]+$");
            if (expReg.IsMatch(Password))
                Caracter = false;


            foreach (char i in Password)
            {
                string character = i.ToString();
                if (character == character.ToUpper())
                {
                    Mayus = false;
                    break;
                }

            }
            int indice = 0;
            foreach (char i in Password)
            {
                if (int.TryParse(i.ToString(), out indice))
                {
                    Numero = false;
                    break;
                }

            }
        }
        /// <summary>
        /// Pagina de BenefitHub
        /// </summary>
        /// <param name="webView">Visor de pagina</param>
        /// <returns></returns>
        private async Task ExecuteGetUserData(RendererControls.WebViewRenderer webView)
        {
            UserModel = await DataUser.GetUserDataAsync().ConfigureAwait(true);
            Email = UserModel.Email;
            Name = UserModel.Nombre + " " + UserModel.Paterno;

            RequestResult = await DataCard.RegisterBenefit(Email).ConfigureAwait(true);
            if (RequestResult.exist)
            {
                App.benefithub = true;
                await _cardsPage.Navigation.PopAsync();
                /*
                Title = "Pagina de Beneficios";
                LoginPage = true;
                App.BenefitLogin = true;
                webView.Source = new HtmlWebViewSource
                {
                    BaseUrl = @"https://inntecdescuentos.benefithub.com",
                    Html = RequestResult.HTML
                };
                */
            }
            else PasswordPage = true;
            RegisterPass = true;

            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(300).ConfigureAwait(true);

            IsBusy = false;
        }
        /// <summary>
        /// Pre-Registro
        /// </summary>
        /// <param name="webView">Visor para benefithub</param>
        /// <returns></returns>
        private async Task ExecutBenefitHubCommand(RendererControls.WebViewRenderer webView)
        {
            IsBusy = true;
            if (VerifyPass())
            {
                this.UserModel.Password = Password;
                this.UserModel.PasswordConfirm = Password;
                this.UserModel.CP = CodigoPostal;
                RequestNewResult = await DataCard.RegisterNewUserBenefit(this.UserModel, RequestResult).ConfigureAwait(true);

                if (RequestNewResult.uri != null)
                {
                    LoginPage = true;
                    Title = "Pagina de Beneficios";
                    LoginPage = true;
                    RequestNewResult.HTML = RequestNewResult.HTML.Replace("name=\"Password\"", "name=\"Password\" value=" + Password);
                    webView.Source = new HtmlWebViewSource
                    {
                        BaseUrl = @"https://inntecdescuentos.benefithub.com",
                        Html = RequestNewResult.HTML
                    };
                    PasswordPage = false;
                }
            }

            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(300).ConfigureAwait(true);
            IsBusy = false;
        }
        /// <summary>
        /// Validacion de password
        /// </summary>
        /// <returns>indicador de validacion correcta para password</returns>
        private bool VerifyPass()
        {
            bool mayus = false;
            foreach (char i in Password)
            {
                string character = i.ToString();
                if (character == character.ToUpper())
                {
                    mayus = true;
                    break;
                }
            }

            System.Text.RegularExpressions.Regex expReg = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]+$");
            if (Password != ConfirmPassword)
            {
                _benefitHubRegisterPage.DisplayAlert("Alerta", "La contraseña y la confirmación deben ser iguales.", "Aceptar");
                return false;
            }
            else if (!mayus)
            {
                _benefitHubRegisterPage.DisplayAlert("Alerta", "Necesitas por lo menos una mayúscula.", "Aceptar");
                return false;
            }
            else if (!expReg.IsMatch(Password))//, Enumeradores.EnumValidar.Contrasenia))
            {
                if (Password.Length < 8)
                {
                    _benefitHubRegisterPage.DisplayAlert("Alerta", "La contraseña debe tener minimo 8 caracteres.", "Aceptar");
                    return false;
                }

                _benefitHubRegisterPage.DisplayAlert("Alerta", "La contraseña no debe tener caracteres especiales.", "Aceptar");
                return false;
            }
            else
                return true;

        }
    }
}
