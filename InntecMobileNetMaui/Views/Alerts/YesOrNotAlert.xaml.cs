using InntecMobileNetMaui.ViewModels.Alerts;
using Mopups.Services;

namespace InntecMobileNetMaui.Views.Alerts;

public partial class YesOrNotAlert 
{
    private static YesOrNotAlert instance = null;
    public static YesOrNotAlert Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new YesOrNotAlert();
            }
            return instance;
        }
    }
    TaskCompletionSource<string> _taskCompletionSource;
    public Task<string> PopupDismissedTask => _taskCompletionSource.Task;

    public string ReturnValue { get; set; }
    public YesOrNotAlert()
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
    private void ButtonNot_Clicked(object sender, EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        ReturnValue = "NO";
        MopupService.Instance.PopAsync();
    }
    private void ButtonYes_Clicked(object sender, EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        ReturnValue = "YES";
        MopupService.Instance.PopAsync();
    }
}