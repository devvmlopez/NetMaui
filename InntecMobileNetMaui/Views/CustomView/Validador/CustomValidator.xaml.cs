using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.ViewModels.Alerts;
using InntecMobileNetMaui.Views.Alerts;
using Mopups.Services;
using System.Reflection.Metadata;

namespace InntecMobileNetMaui.Views.CustomView.Validador;

public partial class CustomValidator 
{
    TaskCompletionSource<string> _taskCompletionSource;
    public Task<string> PopupDismissedTask => _taskCompletionSource.Task;
    private static InformativeAlert instance = null;
    double meta;
    public string ReturnValue { get; set; }
    public static InformativeAlert Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InformativeAlert();
            }
            return instance;
        }
    }
    public CustomValidator()
	{
		InitializeComponent();
        InformativeViewModel.Instance.MessageType = InformativeViewModel.messageType.Message;
        this.BindingContext = InformativeViewModel.Instance;
        evaluarEstado.Value = 25;
        var random = new System.Random();

        bool randomBool = random.Next(2) == 0;
        if (randomBool == true)
        {
            FlechaEvaluar.Source = "left.png";
            meta = 0;
        }
        else 
        {
            FlechaEvaluar.Source = "right.png";
            meta = 50;
        }
    }

    private void evaluarEstado_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        double valorAlcanzado = evaluarEstado.Value;
        if (valorAlcanzado == 0 && valorAlcanzado == meta)
        {
            ReturnValue = Constants.TokenCapchatIZQ;
            MopupService.Instance.PopAsync();
        }
        else if(valorAlcanzado == 50 && valorAlcanzado == meta)
        {
            ReturnValue = Constants.TokenCapchatDER;
            MopupService.Instance.PopAsync();
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var random = new System.Random();

        bool randomBool = random.Next(2) == 0;
        if (randomBool == true)
        {
            FlechaEvaluar.Source = "left.png";
            meta = 0;
        }
        else
        {
            FlechaEvaluar.Source = "right.png";
            meta = 50;
        }

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
}