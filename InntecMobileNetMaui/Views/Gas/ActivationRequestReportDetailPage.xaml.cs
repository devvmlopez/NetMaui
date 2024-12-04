using InntecMobileNetMaui.ViewModels.Gas;

namespace InntecMobileNetMaui.Views.Gas;


public partial class ActivationRequestReportDetailPage : ContentPage
{
    public ActivationRequestReportDetailPage(Models.Gas.ActivationRequestReportModel param)
    {
        InitializeComponent();
        BindingContext = new ActivationRequestReportDetailViewModel(param);
    }
    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}
