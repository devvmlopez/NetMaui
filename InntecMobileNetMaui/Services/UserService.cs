using Acr.UserDialogs.Infrastructure;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services
{
    public class UserService : IUserService<UserModel>
    {

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">Datos de usuario que quiere hacer login</param>
        /// <param name="token">token proveniente de ReCaptcha</param>
        /// <param name="plataforma">indica cual es la plataforma que esta ejecutando el login
        /// <list type="number">
        /// <item name="Android">1</item>
        /// <item name="iOS">2</item>
        /// </list>
        /// </param>
        /// <returns>Datos completos del usuario que hizo login(Con token)</returns>
        /// Android = 1, iOS = 2
        /// 
        //public async Task<LoginModel> LoginAsync(LoginModel login, string token, int plataforma)
        public async Task<LoginModel> LoginAsync(LoginModel login, string token, int plataforma)
        {
            LoginModel loginModel;
            try
            {
                var keyValues = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("username", login.Usuario),
                    new KeyValuePair<string, string>("password", login.Password),
                    new KeyValuePair<string, string>("grant_type","password")
                };

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/token");
                request.Content = new FormUrlEncodedContent(keyValues);
                request.Headers.Add("tokenRecaptcha", token);
                request.Headers.Add("origen", plataforma.ToString());
                var client = new HttpClient();
                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                JObject resultRequest = JsonConvert.DeserializeObject(content) as JObject;
                loginModel = JsonConvert.DeserializeObject<LoginModel>(content);
                loginModel.HttpStatusCode = response.StatusCode;
                loginModel.issued = resultRequest.Value<DateTime>(".issued");
                loginModel.expires = resultRequest.Value<DateTime>(".expires");
                loginModel.Usuario = login.Usuario;
                loginModel.Password = login.Password;
                loginModel.rememberPWS = login.rememberPWS;
                Constants.Error_Descipcion = loginModel.error_description;
                Constants.Error = loginModel.error;
                #region CONSTANTES
                if (response.IsSuccessStatusCode)
                {
                    Constants.Token = loginModel.access_token;
                    Constants.Token_Expires = loginModel.expires;
                    Constants.UserName = loginModel.userName;
                    Constants.Rol = loginModel.rol;
                    Constants.Error_Descipcion = loginModel.error_description;
                    Constants.Error = loginModel.error;
                    Constants.User = loginModel.Usuario;
                    Constants.Psw = (login.rememberPWS) ? AesGcm.EncryptString(loginModel.Password, loginModel.access_token) : "";
                    Constants.rememberPSW = loginModel.rememberPWS;

                }
                #endregion
            }
            catch (Exception ex)
            {
                loginModel = new LoginModel();
                loginModel.error_description = Constants.ERROR_EXCEPTION_SERVICE;
                loginModel.error = Constants.ERROR_EXCEPTION_SERVICE;
                loginModel.Usuario = login.Usuario;
                loginModel.Password = login.Password;
                loginModel.rememberPWS = login.rememberPWS;
                Constants.Error_Descipcion = loginModel.error_description;
                Constants.Error = loginModel.error;

                Log.Error("login", ex.ToString());
            }
            return loginModel;
        }

        ///// <summary>
        ///// Registro de usuario
        ///// </summary>
        ///// <param name="userModel">Datos del usuario a registrar</param>
        ///// <returns>Datos que indican el siguiente paso</returns>
        //public async Task<RegisterResultModel> RegisterAsync(UserModel userModel)
        //{
        //    RegisterResultModel Result;

        //    userModel.UsuarioNombre = userModel.UsuarioNombre.Trim();
        //    userModel.Email = userModel.Email.Trim();

        //    try
        //    {
        //        Result = new RegisterResultModel();

        //        var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Account/Register");
        //        request.Content = new StringContent(JsonConvert.SerializeObject(userModel),
        //                                Encoding.UTF8,
        //                                "application/json");
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders
        //              .Accept
        //              .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        var response = await client.SendAsync(request).ConfigureAwait(true);
        //        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        //        JObject jwtDynamic = JsonConvert.DeserializeObject(content) as JObject;
        //        Result.HttpStatusCode = response.StatusCode;
        //        if (jwtDynamic != null)
        //            Result.Message = jwtDynamic.Value<string>("Message");
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            Result.Message = "Revisa el correo enviado a tu correo para validar la cuenta.";
        //            MailConfirmModel mailConfirm = new MailConfirmModel { Email = userModel.Email, UserName = userModel.UsuarioNombre };
        //            _ = await sendConfirmEmail(mailConfirm).ConfigureAwait(true);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Result = new RegisterResultModel();
        //        Result.Message = Constants.ERROR_EXCEPTION_SERVICE;
        //    }
        //    return Result;
        //}

        /// <summary>
        /// Nuevo Registro de usuario
        /// </summary>
        /// <param name="userModel">Datos del usuario a registrar</param>
        /// <returns>Datos que indican el siguiente paso</returns>
        public async Task<RegisterResultModel> RegisterV2Async(UserV2Model userModel, string token, int platform)
        {
            var Result = new RegisterResultModel();
            userModel.UsuarioNombre = userModel.UsuarioNombre.Trim();
            userModel.Email = userModel.Email.Trim();
            userModel.Tarjeta = userModel.Tarjeta.Replace("-", "");

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Account/v2/Register");
                request.Content = new StringContent(JsonConvert.SerializeObject(userModel),
                                        Encoding.UTF8, "application/json");

                request.Headers.Add("tokenRecaptcha", token);
                request.Headers.Add("origen", platform.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                JObject jwtDynamic = JsonConvert.DeserializeObject(content) as JObject;
                Result.HttpStatusCode = response.StatusCode;
                if (jwtDynamic != null)
                    Result.Message = jwtDynamic.Value<string>("Message");
            }
            catch (Exception ex)
            {
                Result.Message = Constants.ERROR_EXCEPTION_SERVICE;
                Log.Error("registro", ex.ToString());
            }
            return Result;
        }

        /// <summary>
        /// Envio de correo para la confirmacion
        /// </summary>
        /// <param name="mailConfirm">Datos con correo</param>
        /// <returns>Datos del registro indicando si fue satisfactorio o no</returns>
        private async Task<RegisterResultModel> sendConfirmEmail(MailConfirmModel mailConfirm)
        {
            RegisterResultModel Result;
            try
            {
                Result = new RegisterResultModel();

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Account/SendConfirmation");
                request.Content = new StringContent(JsonConvert.SerializeObject(mailConfirm),
                                        Encoding.UTF8,
                                        "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                JObject jwtDynamic = JsonConvert.DeserializeObject(content) as JObject;
                Result.HttpStatusCode = response.StatusCode;
                if (jwtDynamic != null)
                    Result.Message = jwtDynamic.Value<string>("Message");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result.Message = "Correo enviado.";
                }
                else
                {
                    Result.Message = "Error al enviar el correo.";
                }
            }
            catch (Exception)
            {
                Result = new RegisterResultModel();
                Result.Message = "No se pudo conectar con el servidor, intentalo mas tarde";
            }
            return Result;
        }

        /// <summary>
        /// Datos del usuario
        /// </summary>
        /// <param name="login">Datos del usuario que inicio sesion(Con token)</param>
        /// <returns>Datos del usuario</returns>
        public async Task<UserModel> GetUserDataAsync()
        {
            UserModel Result = null;
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/Usuario");

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<UserModel>(content);
            }
            catch (Exception e)
            {
                throw new Exception("UserService => GetUserDataAsync(LoginModel login)", e);
            }
            return Result;
        }

        /// <summary>
        /// Actualizacion de datos
        /// </summary>
        /// <param name="login">Datos del inicio de sesion</param>
        /// <param name="userModel">Datos del usuario que inicio sesion</param>
        /// <returns>Datos actualizados del usuario</returns>
        public async Task<UserModel> SetUserDataAsync(UserModel userModel)
        {
            UserModel Result = null;
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/v2/Usuario");
                request.Content = new StringContent(JsonConvert.SerializeObject(userModel),
                                        Encoding.UTF8,
                                        "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<UserModel>(content);
                Result.StatusCode = response.StatusCode;
                if (Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    userModel.Message = Result.Message;
                    Result = userModel;
                }
            }
            catch
            {
                Result = new UserModel();
                Result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                Result.Message = "Se produjo un error en el proceso de guardado, intentelo mas tarde.";
            }
            return Result;
        }

        ///// <summary>
        ///// Recuperar password de usuario
        ///// </summary>
        ///// <param name="recoverPasswordModel">Datos necesarios para recuperar password</param>
        ///// <returns>Resultado de la operacion</returns>
        //public async Task<RecoverPasswordModel> RecoverUserPassAsync(RecoverPasswordModel recoverPasswordModel)
        //{
        //    RecoverPasswordModel Result = new RecoverPasswordModel();
        //    try
        //    {

        //        var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/account/sendrecovery");
        //        request.Content = new StringContent(JsonConvert.SerializeObject(recoverPasswordModel),
        //                                Encoding.UTF8,
        //                                "application/json");
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders
        //              .Accept
        //              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var response = await client.SendAsync(request).ConfigureAwait(true);
        //        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        //        JObject ResultText = JsonConvert.DeserializeObject(content) as JObject;
        //        if (ResultText != null)
        //        {
        //            Result.Message = ResultText["Message"].ToString();
        //        }
        //        else
        //        {
        //            Result.Message = "Se ha enviado un enlace al correo para recuperar el password.";
        //        }
        //        Result.StatusCode = response.StatusCode;
        //    }
        //    catch
        //    {
        //        Result.Message = "Se produjo un error al contactar con el servidor, intentelo mas tarde.";
        //        Result.StatusCode = System.Net.HttpStatusCode.NotFound;
        //    }
        //    return Result;
        //}

        /// <summary>
        /// Recuperar password de usuario
        /// </summary>
        /// <param name="recoverPasswordModel">Datos necesarios para recuperar password</param>
        /// <returns>Resultado de la operacion</returns>
        public async Task<RecoverPasswordModel> RecoverUserPassV2Async(RecoverPasswordModel recoverPasswordModel, string token, int platform)
        {
            RecoverPasswordModel Result = new RecoverPasswordModel();
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/account/v2/sendrecovery");
                request.Content = new StringContent(JsonConvert.SerializeObject(recoverPasswordModel),
                                        Encoding.UTF8,
                                        "application/json");

                request.Headers.Add("tokenRecaptcha", token);
                request.Headers.Add("origen", platform.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                JObject ResultText = JsonConvert.DeserializeObject(content) as JObject;
                if (ResultText != null)
                {
                    Result.Message = ResultText["Message"].ToString();
                }
                else
                {
                    Result.Message = "Se ha enviado un enlace al correo para recuperar el password.";
                }
                Result.StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                Result.Message = Constants.ERROR_EXCEPTION_SERVICE;
                Result.StatusCode = System.Net.HttpStatusCode.NotFound;
                Log.Error("send recovery", ex.ToString());
            }
            return Result;
        }

    }
}
