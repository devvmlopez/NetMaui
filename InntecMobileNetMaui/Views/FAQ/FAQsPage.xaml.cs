using InntecMobileNetMaui.ViewModels;
using Mopups.Services;
using System.Threading.Tasks;
using static InntecMobileNetMaui.Models.Enumeradores;

namespace InntecMobileNetMaui.Views.FAQ;

public partial class FAQsPage 
{
    private FAQsViewModel viewModel;
    TaskCompletionSource<string> _taskCompletionSource;
    public Task<string> PopupDismissedTask => _taskCompletionSource.Task;

    public string ReturnValue { get; set; }
    public FAQsPage(enumFAQs tipo)
    {
        InitializeComponent();
        BindingContext = viewModel = new FAQsViewModel(tipo);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.LoadFaqCommand.Execute(enumFAQs.COMBUSTIBLE);
        _taskCompletionSource = new TaskCompletionSource<string>();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        _taskCompletionSource.SetResult(ReturnValue);
    }

    void TapGestureRecognizer_Tapped(Object sender, System.EventArgs e)
    {

        List<object> parameter = new List<object>();

        parameter.Add(sender);
        parameter.Add(((Label)((TappedEventArgs)e).Parameter).Text);

        viewModel.AccordingCommand.Execute(parameter);
    }

    async void Button_Pressed(System.Object sender, System.EventArgs e)
    {
        ReturnValue = "true";
        MopupService.Instance.PopAsync();
    }
}