using InntecMobileNetMaui.Views.FAQ;
using Mopups.Services;
using static InntecMobileNetMaui.Models.Enumeradores;

namespace InntecMobileNetMaui.ViewModels
{
    class FAQmenuViewModel : BaseViewModel
    {
        private string btncombustible = "True";
        public string btnCombustible
        {
            get => btncombustible;
            set => SetProperty(ref btncombustible, value);
        }
        private string btntoken = "True";
        public string btnToken
        {
            get => btntoken;
            set => SetProperty(ref btntoken, value);
        }
        private string btndespensa = "True";
        public string btnDespensa
        {
            get => btndespensa;
            set => SetProperty(ref btndespensa, value);
        }
        private string btnpremium = "True";
        public string btnPremium
        {
            get => btnpremium;
            set => SetProperty(ref btnpremium, value);
        }
        private string btnviaticos = "True";
        public string btnViaticos
        {
            get => btnviaticos;
            set => SetProperty(ref btnviaticos, value);
        }
        public Command CombustibleCommand { get; set; }
        public Command TokenCommand { get; set; }
        public Command DespensaCommand { get; set; }
        public Command PremiumCommand { get; set; }
        public Command ViaticosCommand { get; set; }

        public FAQmenuViewModel()
        {
            CombustibleCommand = new Command(async () => await CombustibleCommandCommand().ConfigureAwait(true));
            TokenCommand = new Command(async () => await TokenCommandCommand().ConfigureAwait(true));
            DespensaCommand = new Command(async () => await DespensaCommandCommand().ConfigureAwait(true));
            PremiumCommand = new Command(async () => await PremiumCommandCommand().ConfigureAwait(true));
            ViaticosCommand = new Command(async () => await ViaticosCommandCommand().ConfigureAwait(true));
        }

        public async Task CombustibleCommandCommand()
        {
            btnCombustible = "False";
            var popup = new FAQsPage(enumFAQs.COMBUSTIBLE);
            await MopupService.Instance.PushAsync(popup);
            var rvalue = await popup.PopupDismissedTask;
            btnCombustible = rvalue.ToString();
        }
        public async Task TokenCommandCommand()
        {
            btnToken = "False";
            var popup = new FAQsPage(enumFAQs.TOKEN);
            await MopupService.Instance.PushAsync(popup);
            var rvalue = await popup.PopupDismissedTask;
            btnToken = rvalue.ToString();
        }

        public async Task DespensaCommandCommand()
        {
            btnDespensa = "False";
            var popup = new FAQsPage(enumFAQs.DESPENSA);
            await MopupService.Instance.PushAsync(popup);
            var rvalue = await popup.PopupDismissedTask;
            btnDespensa = rvalue.ToString();
        }
        public async Task PremiumCommandCommand()
        {
            btnPremium = "False";
            var popup = new FAQsPage(enumFAQs.PREMIUM);
            await MopupService.Instance.PushAsync(popup);
            var rvalue = await popup.PopupDismissedTask;
            btnPremium = rvalue.ToString();
        }
        public async Task ViaticosCommandCommand()
        {
            btnViaticos = "False";
            var popup = new FAQsPage(enumFAQs.VIATICOS);
            await MopupService.Instance.PushAsync(popup);
            var rvalue = await popup.PopupDismissedTask;
            btnViaticos = rvalue.ToString();
        }
    }
}
