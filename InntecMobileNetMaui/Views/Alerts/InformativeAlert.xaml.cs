using Mopups.Services;
using InntecMobileNetMaui.ViewModels.Alerts;

namespace InntecMobileNetMaui.Views.Alerts;

public partial class InformativeAlert
{
    private static InformativeAlert instance = null;

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

    public InformativeAlert()
    {
        InitializeComponent();
        this.BindingContext = InformativeViewModel.Instance;
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        InformativeViewModel.Instance.Message = string.Empty;
        MopupService.Instance.PopAsync();
    }
}