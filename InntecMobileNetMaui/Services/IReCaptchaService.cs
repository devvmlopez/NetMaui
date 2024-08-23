using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services
{
    public interface IReCaptchaService
    {
        Task<string> Verify(string siteKey, string domainUrl);

    }
}
