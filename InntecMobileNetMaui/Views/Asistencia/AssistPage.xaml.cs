using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.ViewModels.Assistencia;

namespace InntecMobileNetMaui.Views.Asistencia;

public partial class AssistPage : ContentPage
{
    private AssistViewModel ViewModels = null;
    bool _Register = false;
    public UserModel userModel;
    /// <summary>
    /// Inicializar objetos
    /// </summary>
    /// <param name="Register">Identifica si ya tiene registrado el beneficio</param>
    /// <param name="userModel">Datos del usuario</param>
    /// <param name="assistModel">Datos para registrar el beneficio</param>
    /// <param name="loginModel">Datos del usuario que inicio sesion</param>
    //public AssistPage(bool Register, ref UserModel userModel, Models.Assist.AssistModel assistModel = null, LoginModel loginModel = null)
    //{
    //    InitializeComponent();
    //    _Register = Register;
    //    this.userModel = userModel;
    //    this.BindingContext = ViewModels = new AssistViewModel(loginModel, assistModel);
    //    BtnTerm.Pressed += new EventHandler(BtnTerm_Pressed);
    //    BtnActivate.Pressed += new EventHandler(BtnActivate_Pressed);
    //    BtnClose.Pressed += new EventHandler(BtnClose_Pressed); 
    //}
    /// <summary>
    /// Regreso a pantalla principal
    /// </summary>
    /// <param name="sender">Objeto con el que se trabaja</param>
    /// <param name="e">Parametros del evento</param>
    private async void BtnClose_Pressed(object sender, EventArgs e)
    {
        if (_Register)
            await Navigation.PopModalAsync().ConfigureAwait(true);
        else
            await Navigation.PopAsync().ConfigureAwait(true);
    }
    /// <summary>
    /// Activar Asistencia Inntec
    /// </summary>
    /// <param name="sender">Objeto con el que se trabaja</param>
    /// <param name="e">Parametros del evento</param>
    private async void BtnActivate_Pressed(object sender, EventArgs e)
    {
        if (_Register)
        {
            userModel.TipoAsistencia = "INNTEC_ASISTENCIA_ROJA";
            await Navigation.PopModalAsync().ConfigureAwait(true);
        }
        else
        {
            ViewModels.RegisterCommand.Execute(null);
            await Navigation.PopAsync().ConfigureAwait(true);
        }
    }
    /// <summary>
    /// Ver Terminos y condiciones
    /// </summary>
    /// <param name="sender">Objeto con el que se trabaja</param>
    /// <param name="e">Parametros del evento</param>
    private async void BtnTerm_Pressed(object sender, EventArgs e)
    {
        await Launcher.OpenAsync("https://www.inntecmp.com.mx/sitio/TermCondRoja.aspx").ConfigureAwait(true);
    }
}