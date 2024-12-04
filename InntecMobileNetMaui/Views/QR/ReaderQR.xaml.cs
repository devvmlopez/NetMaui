using InntecMobileNetMaui.ViewModels.Alerts;
using Mopups.Services;
using ZXing.Net.Maui;

namespace InntecMobileNetMaui.Views.QR;

    public partial class ReaderQR
    {
        private static ReaderQR instance = null;
        bool oneTime = true;
        public static ReaderQR Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReaderQR();
                }
                return instance;
            }
        }
        TaskCompletionSource<string> _taskCompletionSource;
        public Task<string> PopupDismissedTask => _taskCompletionSource.Task;

        public string ReturnValue { get; set; }
        public ReaderQR()
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
            MopupService.Instance.PopAsync();
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            InformativeViewModel.Instance.Message = string.Empty;
            ReturnValue = "Cancelar";
            MopupService.Instance.PopAsync();
        }

        private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {

            if (oneTime == true) 
            {
                oneTime = false;
                var result = e.Results?.FirstOrDefault();
                if (result is null) return;

                Dispatcher.DispatchAsync(async () =>
                {
                    // await DisplayAlert("Barcode detected", result.Value, "OK");
                });
                ReturnValue = result.Value;

                if (ReturnValue == null || ReturnValue == "")
                {
                    ReturnValue = "0";
                }

                MopupService.Instance.PopAsync();
            }
            oneTime = false;    

       

        }
    }