using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services
{
        public class MenuContigoServices: IMenuContigoServices<UserModel>
        {
        /// <summary>
        /// Implementacion Inntec Contigo Consulta
        /// </summary>
        /// <param name="userModel">objeto  usuario</param>
        /// <param name="item">Datos del usuario</param>
        /// <returns></returns>
        public async Task<UserModel> CheckBeneficionsContigoAsync(UserModel userModel)
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

        public async Task<UserModel> SuscripcionBeneficionsContigoAsync(UserModel user)
        {
            UserModel Result = user; // Modificar esta parte para que sea de tipo de dato del response para suscribirse
                                     ////item.EstatusDescripcion = "";
            try { }
            catch { }
            //var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/v2/Tarjeta"); // ubicacion de la Api , falta saber que se manda en el response

            //var client = new HttpClient();
            //request.Content = new StringContent(JsonConvert.SerializeObject(item),
            //                           Encoding.UTF8,
            //                           "application/json");
            //client.DefaultRequestHeaders
            //      .Accept
            //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

            //var response = await client.SendAsync(request).ConfigureAwait(true);
            //var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            //Result = JsonConvert.DeserializeObject<UserModel>(content);
            //Result.StatusCode = response.StatusCode;
            return Result;
        }
    }
}
