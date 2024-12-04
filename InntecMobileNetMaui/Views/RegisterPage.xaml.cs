using Acr.UserDialogs;
using Acr.UserDialogs.Infrastructure;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using InntecMobileNetMaui.Views.CustomView.Validador;
using InntecMobileNetMaui.Views.Login;
using Mopups.Services;
using System.Drawing;
using Color = Microsoft.Maui.Graphics.Color;

namespace InntecMobileNetMaui.Views;

public partial class RegisterPage : ContentPage
{
    RegisterViewModel registerViewModel;

    /// <summary>
    /// Inicializar objetos
    /// </summary>
    public RegisterPage()
    {
        InitializeComponent();
        this.BindingContext = registerViewModel = new RegisterViewModel(this);
        //BtnSiguiente.Clicked += new EventHandler(BtnSiguiente_Clicked);
    }

    /// <summary>
    /// Siguiente paso 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void BtnRegistro_Clicked(object sender, EventArgs e)
    {
        try
        {
            registerViewModel.IsBusy = true;

            IniciarColores();
            if (ValidarCampos())
            {
                var captchaToken = "";
                var popupValidador = new CustomValidator();
                InformativeViewModel.Instance.Title = "Mensaje";
                InformativeViewModel.Instance.Message = "Siga las instrucciones para continuar.";
                await MopupService.Instance.PushAsync(popupValidador);
                captchaToken = await popupValidador.PopupDismissedTask;

                if (captchaToken == null)
                    return;

                await registerViewModel.RegiterV2(captchaToken).ConfigureAwait(true);
            }

            registerViewModel.IsBusy = false;
        }
        catch (Exception ex)
        {
            Log.Error("error", ex.Message);

        }
    }

    /// <summary>
    /// Inicializar colores de controles de texto
    /// </summary>
    private void IniciarColores()
    {

    }

    /// <summary>
    /// Validar datos obligatorios
    /// </summary>
    /// <returns>indicador de campos correctos</returns>
    private bool ValidarCampos()
    {
        bool result = true;

        if (string.IsNullOrEmpty(TxtNombre.Text))
        {
            TxtNombre.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else 
        {
            TxtNombre.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtApellidoPaterno.Text))
        {
            TxtApellidoPaterno.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtApellidoPaterno.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtApellidoMaterno.Text))
        {
            TxtApellidoMaterno.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtApellidoMaterno.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtEmail.Text))
        {
            TxtEmail.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtEmail.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtCelular.Text))
        {
            TxtCelular.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtCelular.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtTarjeta.Text))
        {
            TxtTarjeta.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtTarjeta.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtUser.Text))
        {
            TxtUser.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtUser.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtContrasena.Text))
        {
            TxtContrasena.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtContrasena.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtConfirmarContrasena.Text))
        {
            TxtConfirmarContrasena.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtConfirmarContrasena.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }
        if (string.IsNullOrEmpty(TxtTokenUnico.Text))
        {
            TxtTokenUnico.BackgroundColor = Color.FromHex("#FF0000");
            result = false;
        }
        else
        {
            TxtTokenUnico.BackgroundColor = Color.FromRgba(0, 0, 0, 0.01);
            //TxtComments.SetAppThemeColor(Entry.TextColorProperty, Colors.Black, Colors.White);
        }

        InformativeViewModel.Instance.MessageType = InntecMobileNetMaui.ViewModels.Alerts.InformativeViewModel.messageType.Error;
        InformativeViewModel.Instance.Title = "Alerta del sistema";



        if (!result)
        {
            InformativeViewModel.Instance.Message = "No has capturado todos los datos";
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
            return false;
        }

        if (!Chk_Terms.IsChecked)
        {
            InformativeViewModel.Instance.Message = "Debes aceptar los terminos y condiciones";
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
            return false;
        }

        if (!string.IsNullOrEmpty(TxtTarjeta.Text) && TxtTarjeta.Text.Length != 16)
        {
            TxtTarjeta.BackgroundColor = Color.FromHex("#FF0000");

            InformativeViewModel.Instance.Message = "Verifica que la tarjeta sea correcta";
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
            return false;
        }

        Services.ExpReg expReg = new Services.ExpReg();

        if (!string.IsNullOrEmpty(TxtEmail.Text) && result && !expReg.ValidarCadena(TxtEmail.Text.Trim().ToLower(), Enumeradores.EnumValidar.Correo))
        {
            TxtEmail.BackgroundColor = Color.FromHex("#FF0000");

            InformativeViewModel.Instance.Message = "Introduce un correo valido";
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
            return false;
        }

        if (!string.IsNullOrEmpty(TxtContrasena.Text) && !string.IsNullOrEmpty(TxtConfirmarContrasena.Text) && !TxtContrasena.Text.Equals(TxtConfirmarContrasena.Text))
        {
            if (result)
            {
                TxtContrasena.BackgroundColor = Color.FromHex("#FF0000");
                TxtConfirmarContrasena.BackgroundColor = Color.FromHex("#FF0000");

                InformativeViewModel.Instance.Message = "La confirmación debe igual a la contraseña";
                MopupService.Instance.PushAsync(InformativeAlert.Instance);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(TxtCelular.Text) && result && !expReg.ValidarCadena(TxtCelular.Text, Enumeradores.EnumValidar.Telefono_Celular))
        {
            TxtCelular.BackgroundColor = Color.FromHex("#FF0000");

            InformativeViewModel.Instance.Message = "Celular debe estar a 10 digitos.";
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
            
            return false;
        }

        if (result && !expReg.ValidarCadena(TxtContrasena.Text, Enumeradores.EnumValidar.Contrasenia))
        {
            TxtContrasena.BackgroundColor = Color.FromHex("#FF0000");
            TxtConfirmarContrasena.BackgroundColor = Color.FromHex("#FF0000");

            if (TxtContrasena.Text.Length < 10)
            {

                InformativeViewModel.Instance.Message = "La contraseña debe tener minimo 10 caracteres.";
                MopupService.Instance.PushAsync(InformativeAlert.Instance);
                return false;
            }

            InformativeViewModel.Instance.Message = "La contraseña no es valida. al menos una letra mayuscula, al menos una letra minuscula\" +\r\n \"un numero y un caracterer especial (!,@,#,$,%,^,&,*,~,[,],{,},/,\\\\, ,>,+,-,_,=,:,;).";
            MopupService.Instance.PushAsync(InformativeAlert.Instance);
            return false;
        }

        return result;
    }

    /// <summary>
    /// Cancelar registro de usuario nuevo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }

    private async void Btn_Cancel_Clicked(object sender, EventArgs e)
    {
       await Shell.Current.GoToAsync("//Login");
    }
}