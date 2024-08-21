using InntecMobileNetMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services
{
    public interface IUserService<T>
    {
        Task<T> GetUserDataAsync();
        Task<T> SetUserDataAsync(T userModel);
        //Task<RegisterResultModel> RegisterAsync(T userModel);
        Task<RegisterResultModel> RegisterV2Async(UserV2Model userModel, string token, int platform);
        Task<LoginModel> LoginAsync(LoginModel login, string token, int plataforma);
        //Task<RecoverPasswordModel> RecoverUserPassAsync(RecoverPasswordModel recoverPasswordModel);
        Task<RecoverPasswordModel> RecoverUserPassV2Async(RecoverPasswordModel recoverPasswordModel, string token, int platform);
    }
}
