using InntecMobileNetMaui.Services;
using InntecMobileNetMaui.Services.ReCaptcha;

namespace InntecMobileNetMaui
{
    public partial class App : Application
    {
        public static List<Models.CardModel> Cards;
        public App()
        {

            Cards = new List<Models.CardModel>();
            //BenefitLogin = false;
            //DependencyService.Register<SQLiteDataLogin>();
            //DependencyService.Register<CardsService>();
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
    }
}
