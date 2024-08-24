using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.ReCaptcha
{
    public interface IReCaptchaService 
    {
        Task<string> Verify(string siteKey, string domainUrl);
    }
}
