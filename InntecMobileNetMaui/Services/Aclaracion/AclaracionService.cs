using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.Models.Aclaraciones;

namespace InntecMobileNetMaui.Services.Aclaracion
{
    public class AclaracionService : IAclaracionService
    {


        /// <summary>
        /// Inicia el registro de la aclaracion.
        /// </summary>
        /// <param name="aclaracion">datos de la aclaracion.</param>
        /// <returns></returns>
        public async Task<AclaracionModel> GuardarAclaracion(AclaracionModel aclaracion)
        {
            var result = new AclaracionModel();
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/aclaracion/registrar")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(aclaracion),
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
                result = JsonConvert.DeserializeObject<AclaracionModel>(content);
                result.StatusCode = response.StatusCode;

            }
            catch (Exception ex)
            {
                result = new AclaracionModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
            return result;
        }

        /// <summary>
        /// Guarda los documentos del usuario.
        /// </summary>
        /// <param name="Archivos">Listado de archivos a guardar</param>
        /// <param name="AclaracionId">Identificador de la aclaracion a la que se le asignaran los archivos.</param>
        /// <returns></returns>
        public async Task<AclaracionModel> GuardarArchivos(Dictionary<string, byte[]> Archivos, int AclaracionId)
        {
            var result = new AclaracionModel();
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/aclaracion/guardardocumentos?AclaracionId=" + AclaracionId.ToString())
                {
                    Content = new StringContent(JsonConvert.SerializeObject(Archivos),
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

                return result;
            }
            catch (Exception ex)
            {
                result = new AclaracionModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Se producjo un error al registrar tu identificacion, intentalo nuevamente."
                };
                return result;
            }


        }
        /// <summary>
        /// Actualizar el estatus de la aclaracion. (Se utiliza cuando se termina de registrar una aclaracion.)
        /// </summary>
        /// <param name="AclaracionId"></param>
        /// <returns></returns>
        public async Task ActualizarStatus(int AclaracionId)
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/aclaracion/terminarregistro?AclaracionId=" + AclaracionId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var results = JsonConvert.DeserializeObject(content) as JObject;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Lista de aclaraciones creadas por el usuario
        /// </summary>
        /// <returns></returns>
        public async Task<List<AclaracionModel>> ObtenerAclaraciones()
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/aclaracion/obtenerAclaraciones");

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var Result = JsonConvert.DeserializeObject<List<AclaracionModel>>(content);

                return Result;
            }
            catch (Exception ex)
            {
                return new List<AclaracionModel>();
            }

        }

        /// <summary>
        /// Detalle de la aclaracion seleccionada
        /// </summary>
        /// <param name="AclaracionId">Identificador de la aclaracion.</param>
        /// <returns></returns>
        public async Task<List<DetalleAclaracionModel>> ObtenerAclaracionDetalle(int AclaracionId)
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/aclaracion/obteneraclaraciondetalle/" + AclaracionId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var Result = JsonConvert.DeserializeObject<List<DetalleAclaracionModel>>(content);

                return Result;
            }
            catch (Exception ex)
            {
                return new List<DetalleAclaracionModel>();
            }
        }

        /// <summary>
        /// Se envia los datos necesarios para generar los archivos PDF.
        /// </summary>
        /// <param name="AclaracionId">Identificador de la aclaracion a la que se le generaran los archivos.</param>
        /// <param name="Imagenes">imagenes de para los encabezados de los archivos, asi como la firma</param>
        /// <param name="datosDocumento">datos que se plasmaran en el PDF.</param>
        /// <returns></returns>
        public async Task GenerarPDFs(int AclaracionId, Dictionary<string, byte[]> Imagenes, DatosDocumento datosDocumento)
        {
            try
            {
                List<object> datos = new List<object>();
                datos.Add(datosDocumento);
                datos.Add(Imagenes);


                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/aclaracion/generarpdf?AclaracionId=" + AclaracionId.ToString() + "&Marca=" + App.RegistroAclaracion.Marca)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(datos),
                        Encoding.UTF8,
                        "application/json")
                };
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var results = JsonConvert.DeserializeObject(content) as JObject;

                return;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Se verifica el area de aclaraciones ya cargo algun archivo
        /// </summary>
        /// <param name="AclaracionId">Identificador de la aclaracion que se quiere revisar.</param>
        /// <returns></returns>
        public async Task<bool> VerificarArchivosAclaracion(int AclaracionId)
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/aclaracion/verificararchivosaclaracion/" + AclaracionId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var Result = JsonConvert.DeserializeObject<bool>(content);

                return Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Se verifica el estatus en el quese encuentra la alcaracion, para saber si se puede cancelar o no.
        /// </summary>
        /// <param name="AclaracionId"></param>
        /// <returns></returns>
        public async Task<bool> VerificarPermisoCancelar(int AclaracionId)
        {
            try
            {


                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/aclaracion/verificarestatusparacancelar/" + AclaracionId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var Result = JsonConvert.DeserializeObject<bool>(content);

                return Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Se cancela una Aclaracion
        /// </summary>
        /// <param name="AclaracionId">Identificador de la aclaracion a cancelar.</param>
        /// <returns></returns>
        public async Task<CancelarAclaracionModel> CancelarAclaracion(int AclaracionId)
        {
            try
            {

                CancelarAclaracionModel result = new CancelarAclaracionModel();
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/aclaracion/cancelaraclaracion?AclaracionId=" + AclaracionId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

                result.Texto = JsonConvert.DeserializeObject<string>(content);
                result.StatusCode = response.StatusCode;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Descargar documento de la aclaracion
        /// </summary>
        /// <param name="AclaracionId">identificador de la aclaracion que contiene el documento</param>
        /// <returns></returns>

        public async Task<byte[]> DescargarDocumentos(int AclaracionId)
        {
            try
            {


                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/aclaracion/descargardocumento/" + AclaracionId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var Result = JsonConvert.DeserializeObject<byte[]>(content);

                return Result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
