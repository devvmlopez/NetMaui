using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.Models.Viatics;
using Microsoft.Maui;

namespace InntecMobileNetMaui.Services.Viatics
{
    public class ViaticsService : IViaticsService<InfoNewRequest>
    {
        /// <summary>
        /// Nueva solicitud de viaticos
        /// </summary>
        /// <returns>Detalle de la solicitud solicitada</returns> 
        public async Task<Models.Viatics.InfoNewRequest> GetViaticsRequestAsync()
        {
            Models.Viatics.InfoNewRequest Result = null;
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/v2/Viaticos/Solicitudes/Nueva");

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<Models.Viatics.InfoNewRequest>(content);
            }
            catch
            {
                throw new Exception("ViaticosService => GetViaticsRequestAsync(LoginModel login)");
            }
            return Result;
        }

        /// <summary>
        /// Nueva solicitud de viaticos
        /// </summary>
        /// <returns>Detalle de la solicitud solicitada</returns> 
        public async Task<Models.Viatics.InfoNewRequest> GetViaticsRequestAsyncV2(int id)
        {
            Models.Viatics.InfoNewRequest Result = null;
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/v2/Viaticos/Solicitudes/Nueva/" + id);

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<Models.Viatics.InfoNewRequest>(content);
            }
            catch
            {
                throw new Exception("ViaticosService => GetViaticsRequestAsyncV2(LoginModel login)");
            }
            return Result;
        }

        /// <summary>
        /// Registrar nueva solicitud de viaticos
        /// </summary>
        /// <param name="newViaticsRequest">Datos de la nueva solicitud de viaticos</param>
        /// <returns>Detalle de la solicitud</returns>
        public async Task<InfoNewRequest> SetViaticsRequestAsync(NewViaticsRequest newViaticsRequest)
        {
            var result = new InfoNewRequest();
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Viaticos/Solicitudes/Nueva")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(newViaticsRequest),
                        Encoding.UTF8,
                        "application/json")
                };
                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var results = JsonConvert.DeserializeObject(content) as JObject;
                result.StatusCode = response.StatusCode;
                result.Message = results != null && results.ContainsKey("Message") ? results.GetValue("Message")?.ToString() : "Registro de solicitud correcto.";
            }
            catch
            {
                result = new InfoNewRequest
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Se produjo un error al contactarse con el servidor, Intenta mas tarde."
                };
            }
            return result;
        }

        /// <summary>
        /// Registrar nueva solicitud de viaticos
        /// </summary>
        /// <param name="newViaticsRequest">Datos de la nueva solicitud de viaticos</param>
        /// <returns>Detalle de la solicitud</returns>
        public async Task<InfoNewRequest> SetViaticsRequestAsyncV2(NewViaticsRequest newViaticsRequest)
        {
            var result = new InfoNewRequest();
            try
            {

                //var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Viaticos/Solicitudes/Nueva")
                //{
                //    Content = new StringContent(JsonConvert.SerializeObject(newViaticsRequest),
                //        Encoding.UTF8,
                //        "application/json")
                //};
                //var client = new HttpClient();
                //client.DefaultRequestHeaders
                //      .Accept
                //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                //var response = await client.SendAsync(request).ConfigureAwait(true);
                //var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                //var results = JsonConvert.DeserializeObject(content) as JObject;
                //result.StatusCode = response.StatusCode;
                //result.Message = results != null && results.ContainsKey("Message") ? results.GetValue("Message")?.ToString() : "Registro de solicitud correcto.";
            }
            catch
            {
                result = new InfoNewRequest
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Se produjo un error al contactarse con el servidor, Intenta mas tarde."
                };
            }
            return result;
        }

        /// <summary>
        /// Listado de solicitudes de viaticos solicitadas
        /// </summary>
        /// <param name="viaticsListModel">Datos de la solicitud</param>
        /// <returns>Listado con datos generales de las solicitudes de vaiticos</returns>
        public async Task<IEnumerable<object>> GetViaticsListAsync( ViaticsFilterModel filterModel , int UsuarioCsmTarjetaId)
        {
            var result = new List<object>();
            try
            {

                filterModel.dateEnd = filterModel.dateEnd.AddDays(1);
                //if (string.IsNullOrEmpty(viaticsListModel.folio))
                //    viaticsListModel.folio = string.Empty;
                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + $"/api/v2/Viaticos/Solicitudes?folio=&id="+ UsuarioCsmTarjetaId + "&fechaIni=" + filterModel.dateStart.ToString("yyyy-M-d") + "&fechaFin=" + filterModel.dateEnd.ToString("yyyy-M-d"));

                var client = new HttpClient();

                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                IEnumerable<ViaticsRequest> lstItems = JsonConvert.DeserializeObject<List<ViaticsRequest>>(content);
                result.Add(lstItems);
                result.Add(response.StatusCode);

            }
            catch (Exception)
            {
                result.Add(HttpStatusCode.NotFound);
            }
            return result;
        }

        /// <summary>
        /// Detalles de una solicitud de viaticos
        /// </summary>
        /// <param name="viaticsListModel">Solicitudes de viaticos</param>
        /// <returns>Listado del detalle de una solicitud de viaticos</returns>
        public async Task<IEnumerable<object>> GetViaticsConsumptionDetailAsync(ViaticsListModel viaticsListModel , int UsuarioCsmTarjetaId)
        {
            List<object> Result = new List<object>();
            IEnumerable<ConsumptionDetail> LstItems = new List<ConsumptionDetail>();
            try
            {

                viaticsListModel.fechaHasta = viaticsListModel.fechaHasta.AddDays(1);
                if (string.IsNullOrEmpty(viaticsListModel.folio))
                    viaticsListModel.folio = string.Empty;

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + $"/api/v2/Viaticos/Solicitudes/Consumos?id=" + UsuarioCsmTarjetaId + "&fechaIni=" + viaticsListModel.fechaDesde.ToString("yyyy-MM-d") + "&fechaFin=" + viaticsListModel.fechaHasta.ToString("yyyy-MM-d")+ "&status&folio="+ viaticsListModel.folio + "");
                var client = new HttpClient();
                client.DefaultRequestHeaders
                .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                LstItems = JsonConvert.DeserializeObject<List<ConsumptionDetail>>(content);
                Result.Add(LstItems);
                Result.Add(response.StatusCode);

            }
            catch
            {
                Result.Add(HttpStatusCode.NotFound);
            }
            return Result;
        }

        /// <summary>
        /// Envio de archivos(Comprobacion)
        /// </summary>
        /// <param name="dataArray">Datos de la imagen</param>
        /// <param name="kind">Extencion</param>
        /// <param name="loginModel">Ususario que inicio sesion</param>
        /// <param name="param">Detalle del consumo</param>
        /// <returns>Resultado del envio del archivo</returns>
        public async Task<Models.Viatics.FileResult> sendFile(byte[] dataArray, string kind, ConsumptionDetail param)
        {

            Models.Viatics.FileResult fileResult = new Models.Viatics.FileResult();

            try
            {


                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Viaticos/Solicitudes/Consumos/Comprobacion");

                var client = new HttpClient();
                MultipartFormDataContent multipartFormData = new MultipartFormDataContent();
                multipartFormData.Add(new ByteArrayContent(dataArray), kind, param.FileName);
                multipartFormData.Add(new StringContent(param.ViaticoConsumoId.ToString()), "viaticoConsumoId");
                multipartFormData.Add(new StringContent(param.MontoComprobado.ToString()), "montoComprobado");
                multipartFormData.Add(new StringContent(param.Observacion), "observacion");
                multipartFormData.Add(new StringContent(kind), "tipo");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.PostAsync(request.RequestUri, multipartFormData).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                fileResult.HttpStatusCode = response.StatusCode;
                JObject resultRequest = JsonConvert.DeserializeObject(content) as JObject;
                fileResult.Msg = resultRequest["Message"].ToString();
            }
            catch (Exception ex)
            {
                fileResult.HttpStatusCode = HttpStatusCode.BadRequest;
                fileResult.Msg = ex.Message;
            }
            return fileResult;
        }
    }
}
