using InntecMobileNetMaui.Models.Aclaraciones;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.Services;

namespace InntecMobileNetMaui
{
    public partial class App : Application
    {
        public const string Viaticos = "Viaticos";
        public const string ViaticoDetalle = "DetalleViatico";
        public const string SelectCity = "ViaticosFiltroCiudad";
        public static List<Models.CardModel> Cards;
        public static bool BenefitLogin;
        public static bool benefithub = false;
        public static Dictionary<string, string> URLAclaracionDocument = new Dictionary<string, string>();
        //public MainViewModel viewModel;

        public static AclaracionModel RegistroAclaracion { get; internal set; }
        public static byte[] LogoArray { get; set; }
        public static Stream BiteArraySignature { get; internal set; }
        public App()
        {

            Cards = new List<Models.CardModel>();
            BenefitLogin = false;
            //DependencyService.Register<SQLiteDataLogin>();
            DependencyService.Register<CardsService>();
            DependencyService.Register<UserService>();
            DependencyService.Get<IReCaptchaService>();
            //DependencyService.Register<Services.Viatics.ViaticsService>();
            //DependencyService.Register<Services.Gas.GasService>();
            //DependencyService.Register<Services.notificacion.NotifyServices>();
            //DependencyService.Register<Services.Promociones.PromotionSercies>();
            //DependencyService.Register<Services.Aclaracion.AclaracionService>();

            InitializeComponent();

            MainPage = new AppShell();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            if (Constants.Token_Expires != System.DateTime.FromOADate(0))
            {
                var token = new ViewModels.MainViewModel();
                token.VerifyToken();
            }
        }
    }
}
