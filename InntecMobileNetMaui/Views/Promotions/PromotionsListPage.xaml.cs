using InntecMobileNetMaui.ViewModels.Promotions;
using Mopups.Pages;
using Mopups.Services;

namespace InntecMobileNetMaui.Views.Promotions;

public partial class PromotionsListPage
{

    PromotionsListViewModel viewModel;
    public PromotionsListPage(int comercioId)
    {
        InitializeComponent();
        this.BindingContext = viewModel = new PromotionsListViewModel(comercioId);

        Task.Delay(1000);
        Device.StartTimer(TimeSpan.FromSeconds(3), () =>
        {
            if (viewModel.promociones.Count <= 1)
                return true;

            LstCards.Position = (LstCards.Position + 1) % viewModel.promociones.Count;

            return true;
        });
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.LoadPromotionsCommand.Execute(null);
    }

    async void Button_Pressed(System.Object sender, System.EventArgs e)
    {
        await MopupService.Instance.PopAsync(true);
    }

    void LstCards_ItemTapped(System.Object sender, ItemTappedEventArgs e)
    {
         Launcher.Default.OpenAsync(((Models.Promotions.PromotionModel)e.Item).UrlPromo);
    }
}