using Acr.UserDialogs.Infrastructure;
using Mopups.Services;
using System.Net;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Login;
using Plugin.Fingerprint;
//using InntecMobileNetMaui.Droid;
using Plugin.Fingerprint.Abstractions;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Services;


namespace InntecMobileNetMaui.Views.Login;
public partial class LoginPage : ContentPage
{
    public LoginViewModel viewModel { get; set; }

    /// <summary>
    /// Inicializar objetos
    /// </summary>
    public LoginPage()
    {
        InitializeComponent();

        BindingContext = viewModel = new LoginViewModel();
        viewModel.LoginBiometrico = false;
        viewModel.LoginPass = false;
    }

    /// <summary>
    /// Tipos de biometricos
    /// </summary>
    private enum BiometricMap
    {
        facialrecognition, fingerprint
    }

    /// <summary>
    /// Identificacion de los biometricos
    /// </summary>
    private struct BiometricDeviceMap
    {
        public string model { set; get; }
        public BiometricMap biometricType { set; get; }
    }

    /// <summary>
    /// Identificacion de dispositivo iOS
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    private BiometricDeviceMap MapDeviceiOS(string identifier)
    {
        switch (identifier)
        {
            case "iPhone6,1":
            case "iPhone6,2":
                return new BiometricDeviceMap() { model = "iPhone 5s", biometricType = BiometricMap.fingerprint };
            case "iPhone7,2":
                return new BiometricDeviceMap() { model = "iPhone 6", biometricType = BiometricMap.fingerprint };
            case "iPhone7,1":
                return new BiometricDeviceMap() { model = "iPhone 6 Plus", biometricType = BiometricMap.fingerprint };
            case "iPhone8,1":
                return new BiometricDeviceMap() { model = "iPhone 6s ", biometricType = BiometricMap.fingerprint };
            case "iPhone8,2":
                return new BiometricDeviceMap() { model = "iPhone 6s Plus", biometricType = BiometricMap.fingerprint };
            case "iPhone9,1":
            case "iPhone9,3":
                return new BiometricDeviceMap() { model = "iPhone 7", biometricType = BiometricMap.fingerprint };
            case "iPhone9,2":
            case "iPhone9,4":
                return new BiometricDeviceMap() { model = "iPhone 7 Plus", biometricType = BiometricMap.fingerprint };
            case "iPhone8,4":
            case "iPhone12,8"://SE Second Edition
                return new BiometricDeviceMap() { model = "iPhone SE", biometricType = BiometricMap.fingerprint };
            case "iPhone10,1":
            case "iPhone10,4":
                return new BiometricDeviceMap() { model = "iPhone 8", biometricType = BiometricMap.fingerprint };
            case "iPhone10,2":
            case "iPhone10,5":
                return new BiometricDeviceMap() { model = "iPhone 8 Plus", biometricType = BiometricMap.fingerprint };
            case "iPhone10,3":
            case "iPhone10,6":
                return new BiometricDeviceMap() { model = "iPhone X", biometricType = BiometricMap.facialrecognition };
            case "iPhone11,2":
                return new BiometricDeviceMap() { model = "iPhone XS", biometricType = BiometricMap.facialrecognition };
            case "iPhone11,4":
            case "iPhone11,6":
                return new BiometricDeviceMap() { model = "iPhone XS MAX", biometricType = BiometricMap.facialrecognition };
            case "iPhone11,8":
                return new BiometricDeviceMap() { model = "iPhone XR", biometricType = BiometricMap.facialrecognition };
            case "iPhone12,1":
                return new BiometricDeviceMap() { model = "iPhone 11", biometricType = BiometricMap.facialrecognition };
            case "iPhone12,3":
                return new BiometricDeviceMap() { model = "iPhone 11 Pro", biometricType = BiometricMap.facialrecognition };
            case "iPhone12,5":
                return new BiometricDeviceMap() { model = "iPhone 11 Pro Max", biometricType = BiometricMap.facialrecognition };
            case "iPad5,3":
            case "iPad5,4":
                return new BiometricDeviceMap() { model = "iPad Air 2", biometricType = BiometricMap.fingerprint };
            case "iPad6,11":
            case "iPad6,12":
                return new BiometricDeviceMap() { model = "iPad 5", biometricType = BiometricMap.fingerprint };
            case "iPad7,5":
            case "iPad7,6":
                return new BiometricDeviceMap() { model = "iPad 6", biometricType = BiometricMap.fingerprint };
            case "iPad4,7":
            case "iPad4,8":
            case "iPad4,9":
                return new BiometricDeviceMap() { model = "iPad Mini 3", biometricType = BiometricMap.fingerprint };
            case "iPad5,1":
            case "iPad5,2":
                return new BiometricDeviceMap() { model = "iPad Mini 4", biometricType = BiometricMap.fingerprint };
            case "iPad6,3":
            case "iPad6,4":
                return new BiometricDeviceMap() { model = "iPad Pro (9.7-inch)", biometricType = BiometricMap.fingerprint };
            case "iPad6,7":
            case "iPad6,8":
                return new BiometricDeviceMap() { model = "iPad Pro (12.9-inch)", biometricType = BiometricMap.fingerprint };
            case "iPad7,1":
            case "iPad7,2":
                return new BiometricDeviceMap() { model = "iPad Pro (12.9-inch) (2nd generation)", biometricType = BiometricMap.fingerprint };
            case "iPad7,3":
            case "iPad7,4":
                return new BiometricDeviceMap() { model = "iPad Pro(10.5 - inch)", biometricType = BiometricMap.fingerprint };
            case "iPad8,1":
            case "iPad8,2":
            case "iPad8,3":
            case "iPad8,4":
                return new BiometricDeviceMap() { model = "iPad Pro (11-inch)", biometricType = BiometricMap.facialrecognition };
            case "iPad8,5":
            case "iPad8,6":
            case "iPad8,7":
            case "iPad8,8":
                return new BiometricDeviceMap() { model = "iPad Pro (12.9-inch) (3rd generation)", biometricType = BiometricMap.facialrecognition };
            case "iPad8,9":
            case "iPad8,10":
                return new BiometricDeviceMap() { model = "iPad Pro (11-inch) (2rd generation)", biometricType = BiometricMap.facialrecognition };
            case "iPad8,11":
            case "iPad8,12":
                return new BiometricDeviceMap() { model = "iPad Pro (12.9-inch) (4rd generation)", biometricType = BiometricMap.facialrecognition };
            case "i386":
            case "x86_64":
                return new BiometricDeviceMap() { model = "iPhone Simulador", biometricType = BiometricMap.fingerprint };//aqui puedo cambiar el biometrico por el que necesite.
            default:
                return new BiometricDeviceMap() { model = "Android", biometricType = BiometricMap.fingerprint };
        }
    }

    /// <summary>
    /// Inicializacion de Inicio de sesion
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (Constants.Token_Expires != DateTime.FromOADate(0))
            Constants.Token_Expires = DateTime.FromOADate(0);

        viewModel.FingerPrint = await CrossFingerprint.Current.IsAvailableAsync().ConfigureAwait(true);

        if (viewModel.FingerPrint && Constants.rememberPSW)
        {
            viewModel.LoginBiometrico = true;
            viewModel.LoginPass = false;
            viewModel.Usuario = Constants.UserName;
            if (MapDeviceiOS(DeviceInfo.Model).biometricType == BiometricMap.facialrecognition)
            {
                BtnBiometrico.Source = "faceid";
            }
            else
            {
                BtnBiometrico.Source = "touchId";
                BiometricLogin_Pressed(BtnPorPass, new EventArgs());
            }
        }
        else
        {
            viewModel.LoginBiometrico = false;
            viewModel.LoginPass = true;
        }
        viewModel.IsBusy = false;
    }

    /// <summary>
    /// Registro de usuario nuevo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Btn_Registro_Clicked(object sender, EventArgs e)
    {
        // await Navigation.PushModalAsync(new NavigationPage(new RegisterPage())).ConfigureAwait(false);
        await Shell.Current.GoToAsync("//CardsPage");
    }

    /// <summary>
    /// Recuperacion de password
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Btn_RecuperarContrasena_Clicked(object sender, EventArgs e)
    {

        //try
        //{
        //    var response = await SafetyNetClass.GetClient(this.context).VerifyWithRecaptchaAsync(Constants.SiteKey);
        //    if (!string.IsNullOrEmpty(response.TokenResult))
        //    {
        //        // Validate the user response token using the
        //        // reCAPTCHA siteverify API.
        //    }
        //}
        //catch (Exception ex)
        //{
        //    // Handle exception
        //    throw ex;
        //}

        //bool isValidCaptchaToken = await _reCaptcha.Validate(captchaToken);
        //if (!isValidCaptchaToken)
        //    throw new Exception("reCaptcha token validation failed.");
        // await Navigation.PushModalAsync(new NavigationPage(new RecoverPasswordPage())).ConfigureAwait(false);
    }

    /// <summary>
    /// Inicializacion de sesion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Btn_IniciarSesion_Clicked(object sender, EventArgs e)
    {
        viewModel.IsBusy = true;

        if (HayConexion())
        {
            var captchaToken = "";
            if (!viewModel.EsPrimerIntento)
            {
                captchaToken = await viewModel.reCaptchaService.Verify(Constants.SiteKey, Constants.BaseApiUrl);

                //var evaluacion = await InntecMobileNetMaui.Droid.Services.ReCaptchaService.Verify(Constants.SiteKey, Constants.BaseApiUrl);

                if (captchaToken == null)
                    return;
            }

            Login(captchaToken, (DeviceInfo.Platform == DevicePlatform.Android) ? 1 : 2);
        }
        else
        {
            viewModel.ShowError = true;
            viewModel.Error_description = Constants.ERROR_INTERNET_CONECTION;
            Constants.Error_Descipcion = viewModel.Error_description;

            InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
            InformativeViewModel.Instance.Title = "Ha ocurrido un problema!";
            InformativeViewModel.Instance.Message = Constants.Error_Descipcion;
            await MopupService.Instance.PushAsync(InformativeAlert.Instance);


            viewModel.IsBusy = false;
        }
    }

    private bool HayConexion()
    {
        try
        {
            viewModel.IsBusy = true;
            using (var client = new WebClient())
            using (client.OpenRead(Constants.Url_sitio))
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Log.Error("error", ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Guardar datos e iniciar sesion
    /// </summary>
    /// <param name="token">Token del ReCaptcha</param>
    /// <param name="Plataforma">Plataforma que lo ejecuta</param>
    /// <remarks>
    /// Android = 1
    /// iOS = 2
    /// </remarks>
    private async void Login(string token, int Plataforma)
    {
        try
        {
            viewModel.ShowError = false;
            await viewModel.Login(token, Plataforma).ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Aceptar").ConfigureAwait(true);
        }
    }

    /// <summary>
    /// Inicio de sesion por biometrico
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void BiometricLogin_Pressed(System.Object sender, System.EventArgs e)
    {

        if (!HayConexion()) { _ = DisplayAlert("Alerta!", Constants.ERROR_INTERNET_CONECTION, "Aceptar"); return; }

        AuthenticationRequestConfiguration authenticationRequestConfiguration = new AuthenticationRequestConfiguration("Inicio Biometrico", "Login")
        {
            CancelTitle = "Cancelar"
        };

        try
        {
            viewModel.IsBusy = false;
            FingerprintAuthenticationResult FResult =
                    await CrossFingerprint.Current.AuthenticateAsync(authenticationRequestConfiguration).ConfigureAwait(true);
            if (FResult.Authenticated)
            {
                Btn_IniciarSesion_Clicked(this, new EventArgs());
            }

            if (FResult.Status == FingerprintAuthenticationResultStatus.FallbackRequested)
            {
                TxtContrasena.Text = "";
                viewModel.LoginBiometrico = false;
                viewModel.LoginPass = true;
            }
        }
        catch (Exception)
        {
            _ = DisplayAlert("Alerta!", "Lector digital no disponible.", "Aceptar");
        }
    }

    /// <summary>
    /// Regresar a ingreso por password
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void BtnPorPass_Pressed(System.Object sender, System.EventArgs e)
    {
        TxtContrasena.Text = "";
        viewModel.LoginBiometrico = false;
        viewModel.LoginPass = true;
    }
}
//private void Btn_Entrar_Clicked(object sender, EventArgs e)
//{
//    Shell.Current.GoToAsync("//CardPageList");
//}

//private void Btn_Recuperar_Clicked(object sender, EventArgs e)
//{
//    // Shell.Current.GoToAsync("//NewCardPage");

//}