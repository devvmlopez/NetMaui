using InntecMobileNetMaui.ViewModels.Alerts;
using Mopups.Services;

namespace InntecMobileNetMaui.Views.Alerts;

public partial class AcceptCancelAlert
{
    private static AcceptCancelAlert instance = null;
    public static AcceptCancelAlert Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AcceptCancelAlert();
            }
            return instance;
        }
    }
    TaskCompletionSource<string> _taskCompletionSource;
    public Task<string> PopupDismissedTask => _taskCompletionSource.Task;

    public string ReturnValue { get; set; }
    public AcceptCancelAlert()
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
    private void ButtonConfirm_Clicked(object sender, EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        ReturnValue = "Continuar";
        MopupService.Instance.PopAsync();
    }

    private void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        ReturnValue = "Cancelar";
        MopupService.Instance.PopAsync();
    }
}