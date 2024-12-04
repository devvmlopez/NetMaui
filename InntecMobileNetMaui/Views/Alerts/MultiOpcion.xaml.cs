using InntecMobileNetMaui.ViewModels.Alerts;
using Mopups.Services;

namespace InntecMobileNetMaui.Views.Alerts;

public partial class MultiOpcion 
{
    private static MultiOpcion instance = null;
    public static MultiOpcion Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MultiOpcion();
            }
            return instance;
        }
    }
    TaskCompletionSource<string> _taskCompletionSource;
    public Task<string> PopupDismissedTask => _taskCompletionSource.Task;

    public string ReturnValue { get; set; }
    public MultiOpcion()
    {
        InitializeComponent();
        this.BindingContext = InformativeViewModel.Instance;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _taskCompletionSource = new TaskCompletionSource<string>();
    }
    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        _taskCompletionSource.SetResult(ReturnValue);
    }
    private void ButtonRobo_Clicked(object sender, EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        ReturnValue = "Robo";
        MopupService.Instance.PopAsync();
    }

    private void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        ReturnValue = "Cancelar";
        MopupService.Instance.PopAsync();
    }

    private void ButtonExtravio_Clicked(object sender, EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        ReturnValue = "Extravio";
        MopupService.Instance.PopAsync();
    }

    private void ButtonTarjetaFail_Clicked(object sender, EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        ReturnValue = "Tarjeta dañada";
        MopupService.Instance.PopAsync();
    }
}