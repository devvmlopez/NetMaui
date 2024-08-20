using Mopups.Services;
namespace InntecMobileNetMaui.Views.Cards;

public partial class DeleteCard 
{
	public DeleteCard()
	{
		InitializeComponent();
	}
    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

    }

    private void Btn_Cancel_Pressed(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }
}