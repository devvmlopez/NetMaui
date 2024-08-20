using Mopups.Services;

namespace InntecMobileNetMaui.Views.Cards;

public partial class BlockCardOptionsPage 
{
	public BlockCardOptionsPage()
	{
		InitializeComponent();
	}

    private void Btn_Cancel_Pressed(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }
}