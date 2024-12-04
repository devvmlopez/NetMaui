using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.Models.Gas;

namespace InntecMobileNetMaui.Services.Gas
{
    /// <summary>
    /// Servicios de combustibles
    /// </summary>
    public class GasService : IGasService<Guid>
    {


        /// <summary>
        /// Enlazar usuario a QR
        /// </summary>
        /// <param name="guid">identificador del QR</param>
        /// <param name="login">datos del usuario logeado</param>
        /// <returns></returns>
        public async Task<bool> LinkedUser(Guid guid)
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Combustible/EnlazarTarjeta?guid=" + guid.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                JObject Results = JsonConvert.DeserializeObject(content) as JObject;

                bool result;
                _ = bool.TryParse(content, out result);
                return result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// DesEnlazar usuario de un QR
        /// </summary>
        /// <param name="login">Usuairo que inicio sesion</param>
        /// <returns></returns>
        public async Task<bool> UnLinkedUser()
        {
            try
            {


                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Combustible/DesEnlazarTarjeta");

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                JObject Results = JsonConvert.DeserializeObject(content) as JObject;

                bool result;
                _ = bool.TryParse(content, out result);
                return result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verificar QR en tarjeta
        /// </summary>
        /// <param name="_guid">Identifiador del QR</param>
        /// <param name="login">Usuario que hizo login</param>
        /// <returns></returns>
        public async Task<bool> VerifyQR(Guid _guid)
        {
            try
            {


                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Combustible/VerificarQR?guid=" + _guid.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                JObject Results = JsonConvert.DeserializeObject(content) as JObject;

                bool result;
                _ = bool.TryParse(content, out result);
                return result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verificar restricciones para tarjeta de combustibles
        /// </summary>
        /// <param name="login">Usuario que hizo login</param>
        /// <param name="csmId">Identificador unico de tarjeta</param>
        /// <returns></returns>
        public async Task<List<string>> CheckRestrictions(int csmId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Combustible/VerificarRestricciones?csmId=" + csmId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                List<string> Results = JsonConvert.DeserializeObject<List<string>>(content);

                if (response.IsSuccessStatusCode)
                    return Results;
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Activar tarjeta
        /// </summary>
        /// <param name="login">usuario que inicio sesion</param>
        /// <param name="requestActivation">Datos completos para activar tarjeta</param>
        /// <returns></returns>
        public async Task<ResponseActivationRequest> ActivateCard(ActivationRequestModel requestActivation)
        {

            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Combustible/ActivarTarjeta");
                request.Content = new StringContent(JsonConvert.SerializeObject(requestActivation),
                                        Encoding.UTF8,
                                        "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var Result = JsonConvert.DeserializeObject<ResponseActivationRequest>(content);

                return Result;
            }
            catch
            {
                ResponseActivationRequest responseActivation = new ResponseActivationRequest { Aprobada = false, ListObservaciones = new List<string> { "Error al comunicarse con el servidor, intente mas tarde." }, Folio = null };
                return responseActivation;
            }
        }

        /// <summary>
        /// Respuesta de activacion para tarjeta de combustibles
        /// </summary>
        /// <param name="login">usuario que inicio sesion</param>
        /// <param name="csmUsuarioId">identificador unico de tarjeta</param>
        /// <param name="filterModel">datos de tiempo que permanesera activa la tarjeta</param>
        /// <param name="forceRefresh"></param>
        /// <returns>Listado de motivos por el cual una tarjeta no se activa</returns>
        public async Task<IEnumerable<ActivationRequestReportModel>> GetActivationRequest(int csmUsuarioId, RequestActivationFilterModel filterModel, bool forceRefresh = false)
        {


            var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/Combustible/Solicitudes/" + csmUsuarioId.ToString() + "?fechaDesde=" + filterModel.dateStart.ToString("yyyy/M/d") + "&fechaHasta=" + filterModel.dateEnd.ToString("yyyy/M/d") + "&estatusId=" + ((filterModel.statusId == 0) ? "" : filterModel.statusId.ToString()));

            var client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

            var response = await client.SendAsync(request).ConfigureAwait(true);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var Result = JsonConvert.DeserializeObject<IEnumerable<ActivationRequestReportModel>>(content);

            return Result;
        }

        /// <summary>
        /// Kilometraje anterior
        /// </summary>
        /// <param name="login">usuario que inicio sesion</param>
        /// <param name="tarjetaId">Identificador unico de tarjeta</param>
        /// <returns>Kilometraje</returns>
        public async Task<decimal> GetKM(int? tarjetaId)
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/Combustible/Kilometraje/" + tarjetaId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

                decimal Result = -1;
                if (response.IsSuccessStatusCode)
                    Result = JsonConvert.DeserializeObject<decimal>(content);

                return Result;
            }
            catch (Exception)
            { return -1; }

        }
    }
}
