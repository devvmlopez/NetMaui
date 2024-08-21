using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InntecMobileNetMaui.Resources
{
    public static class Constants
    {

        #region ReCaptcha

        public static string SiteKey
        {
#if DEBUG
            // DEV
            get => Preferences.Get("SiteKey", DeviceInfo.Platform == DevicePlatform.Android ? "6LfcVjIUAAAAAOxoGhbhavwp_M30VKJxzBIAOSlY" : "6Le4058lAAAAAEacrgmzDf4ndMLZSYxWPTzosbtV");
#else
            // QA
            // get => Preferences.Get("SiteKey", DeviceInfo.Platform == DevicePlatform.Android ? "6LfcVjIUAAAAAOxoGhbhavwp_M30VKJxzBIAOSlY" : "6Le4058lAAAAAEacrgmzDf4ndMLZSYxWPTzosbtV");

            // PROD
            get => Preferences.Get("SiteKey", DeviceInfo.Platform == DevicePlatform.Android ? "6LcNWMAnAAAAAOmBPVk9o1CN5WqEMDRVZeUl5gdj" : "6LdrWsAnAAAAANA2n8pN7Bs_xamPRqBrG5GZ6r2_");
#endif
        }

        public static string BaseApiUrl
        {
            get => Preferences.Get("BaseApiUrl", "https://localhost");
        }
        #endregion

        #region Login
        public const string Token_Type = "Bearer";

        public const string Url_Img_Base = "https://cnd.inntecmp.com.mx/";

        public const string Url_Img_Base_Debug = "https://inntecstorage.blob.core.windows.net/";

        public static string Token
        {
            get => Preferences.Get("Token", null);
            set => Preferences.Set("Token", value);
        }

        public static bool RecordatorioClickMovimiento
        {
            get => Preferences.Get("RecordatorioClickMovimiento", false);
            set => Preferences.Set("RecordatorioClickMovimiento", value);
        }

        public static bool RecordatorioBloqueos
        {
            get => Preferences.Get("RecordatorioBloqueos", false);
            set => Preferences.Set("RecordatorioBloqueos", value);
        }

        public static string Rol
        {
            get => Preferences.Get("Rol", null);
            set => Preferences.Set("Rol", value);
        }

        public static MainPage MAINPAGE { get; internal set; }

        public static string Error
        {
            get => Preferences.Get("Error", null);
            set => Preferences.Set("Error", value);
        }
        public static string Error_Descipcion
        {
            get => Preferences.Get("Error_Desc", null);
            set => Preferences.Set("Error_Desc", value);
        }
        public static DateTime Token_Expires
        {
            get => Preferences.Get("Expires", DateTime.FromOADate(0));
            set => Preferences.Set("Expires", value);
        }

        public static DateTime Local_Time
        {
            get => Preferences.Get("Local_Time", DateTime.FromOADate(0));
            set => Preferences.Set("Local_Time", value);
        }

        public static string UserName
        {
            get => Preferences.Get("UsrName", null);
            set => Preferences.Set("UsrName", value);
        }


        public static string User
        {
            get => Preferences.Get("Usr", null);
            set => Preferences.Set("Usr", value);
        }

        public static string Psw
        {
            get => Preferences.Get("Psw", null);
            set => Preferences.Set("Psw", value);
        }

        public static bool rememberPSW
        {
            get => Preferences.Get("rememberPSW", false);
            set => Preferences.Set("rememberPSW", value);
        }

        public static string Products
        {
            get => Preferences.Get("Products", null);
            set => Preferences.Set("Products", value);
        }


        public const string ERROR_INTERNET_CONECTION = "Revisa tu conexión de internet e inténtalo nuevamente.";
        public const string ERROR_EXCEPTION_SERVICE = "Se ha presentado un error de conexión con el servidor, por favor inténtelo de nuevo 5 minutos.";


        #endregion

        #region Messaging Center

        public const string BloqueoTemporal = "BloqueoTemporal";
        public const string BloqueoEcommerce = "BloqueoEcommerce";
        public const string LoadCards = "CargarTarjetas";
        #endregion

        #region Conection
        public const string Url_sitio = "https://www.inntecmp.com.mx/";

#if DEBUG

        // LOCAL
        // public const string Url_Base = "http://192.168.1.70:65266";
        // public const string Url_Base = "http://localhost:65266";

        // DESARROLLO
        public const string Url_Base = "https://ambientedesarrolloapi.azurewebsites.net/csmAPIv2";

        // QA
        // public const string Url_Base = "https://qa.inntecmp.com.mx/csmAPIv2";

#else
        // QA
        // public const string Url_Base = "https://qa.inntecmp.com.mx/csmAPIv2";

        // PROD
        public const string Url_Base = "https://www.inntecmp.com.mx/csmAPIv2";

#endif
        #endregion
    }
}
