using Acr.UserDialogs.Infrastructure;
using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Assist;
using InntecMobileNetMaui.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



//[assembly: Dependency(typeof(InntecMobile.Services.CardsService))]

namespace InntecMobileNetMaui.Services
{
    /// <summary>
    /// Clase para los servicios relacionados a las tarjetas.
    /// </summary>
    public class CardsService : ICardsService<CardModel>
    {
        bool isBusy;

        ///// <summary>
        ///// Agregar tarjeta nueva al usuario.
        ///// </summary>
        ///// <param name="login">objeto del login</param>
        ///// <param name="item">tarjeta que se agregara</param>
        ///// <returns></returns>
        //public async Task<CardModel> AddItemAsync(CardModel item)
        //{
        //    if (isBusy) { return null; }
        //    isBusy = true;

        //    CardModel Result = null;
        //    item.EstatusDescripcion = "";

        //    var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Tarjeta");

        //    var client = new HttpClient();
        //    request.Content = new StringContent(JsonConvert.SerializeObject(item),
        //                               Encoding.UTF8,
        //                               "application/json");
        //    client.DefaultRequestHeaders
        //          .Accept
        //          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

        //    var response = await client.SendAsync(request).ConfigureAwait(true);
        //    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        //    Result = JsonConvert.DeserializeObject<CardModel>(content);
        //    Result.StatusCode = response.StatusCode;
        //    isBusy = false;
        //    return Result;
        //}

        /// <summary>
        /// Agregar tarjeta nueva al usuario.
        /// </summary>
        /// <param name="login">objeto del login</param>
        /// <param name="item">tarjeta que se agregara</param>
        /// <returns></returns>
        public async Task<CardModel> AddItemV2Async(CardModel item)
        {
            CardModel Result = null;
            item.EstatusDescripcion = "";

            var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/v2/Tarjeta");

            var client = new HttpClient();
            request.Content = new StringContent(JsonConvert.SerializeObject(item),
                                       Encoding.UTF8,
                                       "application/json");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

            var response = await client.SendAsync(request).ConfigureAwait(true);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            Result = JsonConvert.DeserializeObject<CardModel>(content);
            Result.StatusCode = response.StatusCode;
            return Result;
        }

        ///// <summary>
        ///// Obtencion del listado de tarjetas relacionadas al usuario.
        ///// </summary>
        ///// <param name="login">objeto de login, para identificar al usuario</param>
        ///// <param name="forceRefresh"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<CardModel>> GetItemsAsync(bool forceRefresh = false)
        //{

        //    IEnumerable<CardModel> Result = null;

        //    var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/Tarjeta");

        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders
        //          .Accept
        //          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

        //    var response = await client.SendAsync(request).ConfigureAwait(true);
        //    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        //    Result = JsonConvert.DeserializeObject<List<CardModel>>(content);

        //    return Result;
        //}

        /// <summary>
        /// Obtencion del listado de tarjetas relacionadas al usuario.
        /// </summary>
        /// <param name="login">objeto de login, para identificar al usuario</param>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CardModel>> GetItemsV2Async(bool forceRefresh = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/v2/Tarjeta");

            var client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

            var response = await client.SendAsync(request).ConfigureAwait(true);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            IEnumerable<CardModel> Result = JsonConvert.DeserializeObject<List<CardModel>>(content);
            return Result;
        }

        ///// <summary>
        ///// Eliminar tarjeta relacionada a un usuario
        ///// </summary>
        ///// <param name="login">Objeto de login</param>
        ///// <param name="card">Tarjeta a eliminar</param>
        ///// <param name="forceRefresh"></param>
        ///// <returns></returns>
        //public async Task<string> DeleteItemAsync(CardModel card, bool forceRefresh = false)
        //{
        //    JObject Result = null;

        //    var request = new HttpRequestMessage(HttpMethod.Delete, Constants.Url_Base + $"/api/Tarjeta/{card.UsuarioCsmTarjetaId}");

        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders
        //          .Accept
        //          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

        //    var response = await client.SendAsync(request).ConfigureAwait(true);
        //    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        //    Result = JsonConvert.DeserializeObject(content) as JObject;
        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        return "Tarjeta eliminada correctamente.";
        //    }
        //    else
        //        return Result["Message"].ToString();
        //}

        /// <summary>
        /// Eliminar tarjeta relacionada a un usuario
        /// </summary>
        /// <param name="login">Objeto de login</param>
        /// <param name="card">Tarjeta a eliminar</param>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<string> DeleteItemV2Async(CardModel card, bool forceRefresh = false)
        {
            JObject Result = null;

            var request = new HttpRequestMessage(HttpMethod.Delete, Constants.Url_Base + $"/api/v2/Tarjeta/{card.UsuarioCsmTarjetaId}");

            var client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

            var response = await client.SendAsync(request).ConfigureAwait(true);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            Result = JsonConvert.DeserializeObject(content) as JObject;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return "La tarjeta fue removida con éxito";
            }
            else
            {
                return Result["Message"].ToString();
            }
        }

        /// <summary>
        /// Obtencion del balance correspondiente al usuario
        /// </summary>
        /// <param name="login">Objeto de login</param>
        /// <param name="card">Tarjeta de la que se requiere saber el saldo</param>
        /// <returns></returns>
        public async Task<BalanceModel> GetBalanceAsync(CardModel card)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + $"/api/Saldo/{card.UsuarioCsmTarjetaId}");

            var client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

            var response = await client.SendAsync(request).ConfigureAwait(true);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            BalanceModel balance = JsonConvert.DeserializeObject<BalanceModel>(content);
            balance.HttpStatusCode = response.StatusCode;
            return balance;
        }

        ///// <summary>
        ///// Reportar tarjeta y bloquear
        ///// </summary>
        ///// <param name="loginModel">Objeto de login</param>
        ///// <param name="cardreport">Tarjeta con motivo de cancelacion</param>
        ///// <returns></returns>
        //public async Task<string> CancelItemAsync(CardReport cardreport)
        //{
        //    string Result = string.Empty;
        //    try
        //    {

        //        var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/Tarjeta/Reportar");
        //        request.Content = new StringContent(JsonConvert.SerializeObject(cardreport),
        //                                Encoding.UTF8,
        //                                "application/json");
        //        var client = new HttpClient();

        //        client.DefaultRequestHeaders
        //          .Accept
        //          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

        //        var response = await client.SendAsync(request).ConfigureAwait(true);
        //        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        //        JObject jwtDynamic = JsonConvert.DeserializeObject(content) as JObject;

        //        if (jwtDynamic != null)
        //            Result = jwtDynamic.Value<string>("Message");

        //    }
        //    catch
        //    {
        //        Result = "No se pudo conectar con el servidor, intentalo mas tarde";
        //    }
        //    return Result;
        //}        

        /// <summary>
        /// Reportar tarjeta y bloquear
        /// </summary>
        /// <param name="loginModel">Objeto de login</param>
        /// <param name="cardreport">Tarjeta con motivo de cancelacion</param>
        /// <returns></returns>
        public async Task<string> ReportarItemV2Async(CardReport cardreport)
        {
            string Result = string.Empty;
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/v2/Tarjeta/Reportar");
                request.Content = new StringContent(JsonConvert.SerializeObject(cardreport),
                                        Encoding.UTF8,
                                        "application/json");
                var client = new HttpClient();

                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var jwtDynamic = JsonConvert.DeserializeObject(content) as JObject;



                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "Reporte exitoso, por seguridad la tarjeta ha quedado bloqueada";
                }
                else
                {
                    if (jwtDynamic != null)
                        Result = jwtDynamic.Value<string>("Message");
                }

            }
            catch (Exception ex)
            {
                Log.Error("reportar tarjeta", ex.ToString());
                Result = Constants.ERROR_EXCEPTION_SERVICE;
            }
            return Result;
        }

        ///// <summary>
        ///// Bloqueo permanente de tarjeta.
        ///// </summary>
        ///// <param name="loginModel">Objeto de login</param>
        ///// <param name="cardModel">Tarjeta que se bloqueara</param>
        ///// <returns></returns>
        //public async Task<CardModel> BlockItemAsync(CardModel cardModel)
        //{
        //    CardModel Result;
        //    try
        //    {

        //        var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + $"/api/Tarjeta/Estatus/{cardModel.UsuarioCsmTarjetaId}");
        //        var client = new HttpClient();

        //        client.DefaultRequestHeaders
        //          .Accept
        //          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

        //        var response = await client.SendAsync(request).ConfigureAwait(true);
        //        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        //        Result = JsonConvert.DeserializeObject<CardModel>(content);
        //        Result.StatusCode = response.StatusCode;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            Result.Message = "El estatus de la tarjeta ha sido actualizado con exito.";
        //        }
        //    }
        //    catch
        //    {
        //        Result = new CardModel();
        //        Result.StatusCode = System.Net.HttpStatusCode.NotFound;
        //        Result.Message = "Hubo un problema al actualizar el estatus de la tarjeta, intenta mas tarde.";
        //    }
        //    return Result;
        //}

        /// <summary>
        /// Bloqueo permanente de tarjeta.
        /// </summary>
        /// <param name="loginModel">Objeto de login</param>
        /// <param name="cardModel">Tarjeta que se bloqueara</param>
        /// <returns></returns>
        public async Task<CardModel> BlockItemV2Async(CardModel cardModel)
        {
            var Result = new CardModel();
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + $"/api/v2/Tarjeta/Estatus/{cardModel.UsuarioCsmTarjetaId}");
                var client = new HttpClient();

                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                // Result = JsonConvert.DeserializeObject<CardModel>(content);
                JObject jwtDynamic = JsonConvert.DeserializeObject(content) as JObject;
                Result.StatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    Result.Message = "El estatus de la tarjeta ha sido actualizado con exito.";
                }
                else
                {
                    if (jwtDynamic != null)
                        Result.Message = jwtDynamic.Value<string>("Message");
                }
            }
            catch (Exception ex)
            {
                Log.Error("error cambio estatus", ex.ToString());
                Result = new CardModel();
                Result.StatusCode = System.Net.HttpStatusCode.NotFound;
                Result.Message = Constants.ERROR_EXCEPTION_SERVICE;
            }
            return Result;
        }

        /// <summary>
        /// Bloqueo de transacciones ecommerce
        /// </summary>
        /// <param name="cardModel">Tarjeta que sera bloqueada</param>
        /// <returns></returns>
        public async Task<CardModel> BlockEcommerceItemAsync(CardModel cardModel)
        {
            //CardModel Result = cardModel;
            //try
            //{
            //    HttpRequestMessage request;
            //    BlockEcommerSayabhaModel parameter = new BlockEcommerSayabhaModel()
            //    {
            //        AllowEcommerce = cardModel.EstatusEcommerce,
            //        UsuarioCsmTarjetaId = cardModel.UsuarioCsmTarjetaId
            //    };


            //    request = new HttpRequestMessage(HttpMethod.Put, Constants.Url_Base + $"/api/v2/Tarjeta/Ecommerce/")
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json")
            //    };
            //    var client = new HttpClient();

            //    client.DefaultRequestHeaders
            //      .Accept
            //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

            //    var response = await client.SendAsync(request).ConfigureAwait(true);
            //    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            //    if (!cardModel.EsProductoSayabha)
            //        Result = JsonConvert.DeserializeObject<CardModel>(content);

            //    Result.StatusCode = response.StatusCode;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        Result.Message = Result.EstatusEcommerce ? "El estatus ha sido actualizado, a partir de este momento SI podrá realizar compras en línea." : "El estatus ha sido actualizado, a partir de este momento NO podrá realizar compras en línea.";
            //    }
            //    else
            //    {
            //        Result.Message = "Ocurrio un problema con el cambio de estatus.";
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Log.Error("error ecommerce", ex.ToString());
            //    Result = new CardModel();
            //    Result.StatusCode = System.Net.HttpStatusCode.NotFound;
            //    Result.Message = Constants.ERROR_EXCEPTION_SERVICE;
            //}
            //return Result;
            return null;
        }

        /// <summary>
        /// Obtencion de movimientos relacionados a la tarjeta.
        /// </summary>
        /// <param name="login">Objeto de login</param>
        /// <param name="card">Tarjeta de la que se requiere saber los movimientos</param>
        /// <returns></returns>
        public async Task<MovementsModel> GetMovementsAsync(CardModel card)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + $"/api/Movimientos/{card.UsuarioCsmTarjetaId}");

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                MovementsModel movements = JsonConvert.DeserializeObject<MovementsModel>(content);
                movements.HttpStatusCode = response.StatusCode;
                return movements;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtencion de movimientos relacionados a la tarjeta por mes.
        /// </summary>
        /// <param name="login">Objeto de login</param>
        /// <param name="card">Tarjeta de la que se requiere obtener los movimientos</param>
        /// <param name="year">Año de los movimientos</param>
        /// <param name="month">Mes de los movimientos</param>
        public async Task<MovementsModel> GetMovementsMonthAsync(CardModel card, int year, int month)
        {
            try
            {

                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);
                client.BaseAddress = new Uri(Constants.Url_Base);
                var response = await client.GetAsync(Constants.Url_Base + $"/api/MovimientosMes/{card.UsuarioCsmTarjetaId}/{year}/{month}").ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                MovementsModel movements = JsonConvert.DeserializeObject<MovementsModel>(content);
                movements.HttpStatusCode = response.StatusCode;
                return movements;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<bool> UpdateItemAsync(CardModel item)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// Cambio de NIP a tarjeta
        ///// </summary>
        ///// <param name="loginModel">Objeto de login</param>
        ///// <param name="cardNipMc">Nuevo NIP</param>
        ///// <returns></returns>
        //public async Task<CardNipModel> ChangeNIPAsync(CardNipModel cardNipMc)
        //{
        //    CardNipModel Result = new CardNipModel();
        //    try
        //    {
        //        DateTime tt;
        //        if (DateTime.TryParse(cardNipMc.FechaVencimiento, out tt))
        //            cardNipMc.FechaVencimiento = AesGcm.EncryptString(cardNipMc.FechaVencimiento, Constants.Token);

        //        if (cardNipMc.Nip.All(char.IsDigit))
        //            cardNipMc.Nip = AesGcm.EncryptString(cardNipMc.Nip, Constants.Token);

        //        var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/tarjeta/Servicios/Nip");
        //        request.Content = new StringContent(JsonConvert.SerializeObject(cardNipMc),
        //                                Encoding.UTF8,
        //                                "application/json");
        //        var client = new HttpClient();

        //        client.DefaultRequestHeaders
        //          .Accept
        //          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

        //        var response = await client.SendAsync(request).ConfigureAwait(true);
        //        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
        //        JObject jwtDynamic = JsonConvert.DeserializeObject(content) as JObject;

        //        if (jwtDynamic != null)
        //            Result.Message = jwtDynamic.Value<string>("Message");
        //        Result.StatusCode = response.StatusCode;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("error cambio nip", ex.ToString());
        //        Result.StatusCode = System.Net.HttpStatusCode.NotFound;
        //        Result.Message = Constants.ERROR_EXCEPTION_SERVICE;
        //    }
        //    return Result;
        //}

        public async Task<CardNipModel> ChangeNipV2Async(CardNipModel cardNipModel)
        {
            var result = new CardNipModel();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/v2/tarjeta/Nip");
                request.Content = new StringContent(JsonConvert.SerializeObject(cardNipModel),
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();

                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                
                result.StatusCode = response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    result.Message = "Se actualizo correctamente el NIP.";
                }
                else
                {
                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        if (JsonConvert.DeserializeObject(content) is JObject jwtDynamic)
                        {
                            result.Message = jwtDynamic.Value<string>("Message");
                        }
                        else
                        {
                            result.Message = "No fue posible cambiar el NIP, intentalo de nuevo.";
                        }
                    }
                    else
                    {
                        result.Message = "Se ha producido un error de comunicación con el servidor, no es posible continuar por el momento.";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("error cambio nip", ex.ToString());
                result.StatusCode = System.Net.HttpStatusCode.NotFound;
                result.Message = Constants.ERROR_EXCEPTION_SERVICE;
            }
            return result;
        }

        /// <summary>
        /// Registro de tarjeta a BenefitHub
        /// </summary>
        /// <param name="userModel">Datos del usuario</param>
        /// <param name="requestResult">Resultado del preRegistro a BenefitHub</param>
        /// <returns></returns>
        public async Task<BenefitHubModel> RegisterNewUserBenefit(UserModel userModel, BenefitHubModel requestResult)
        {
            BenefitHubModel Result = new BenefitHubModel();
            try
            {
                var parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("langselector", "es"));
                parameters.Add(new KeyValuePair<string, string>("FirstName", userModel.Nombre));
                parameters.Add(new KeyValuePair<string, string>("LastName", userModel.Paterno));
                parameters.Add(new KeyValuePair<string, string>("ZipCode", userModel.CP));
                parameters.Add(new KeyValuePair<string, string>("CountryId", "142"));
                parameters.Add(new KeyValuePair<string, string>("Email", userModel.Email));
                parameters.Add(new KeyValuePair<string, string>("Password", userModel.Password));
                parameters.Add(new KeyValuePair<string, string>("ConfirmPassword", userModel.Password));
                parameters.Add(new KeyValuePair<string, string>("PasswordQuestion", "This is a default question"));
                parameters.Add(new KeyValuePair<string, string>("PasswordAnswer", "This is a default answer"));
                parameters.Add(new KeyValuePair<string, string>("AgreeToTerms", "true"));
                parameters.Add(new KeyValuePair<string, string>("AgreeToTerms", "false"));
                parameters.Add(new KeyValuePair<string, string>("EmailUpdates", requestResult.promo.ToString()));
                parameters.Add(new KeyValuePair<string, string>("GDPRCheckbox", "false"));
                parameters.Add(new KeyValuePair<string, string>("InvitationCode", requestResult.token));
                parameters.Add(new KeyValuePair<string, string>("OrganizationId", "30474"));
                parameters.Add(new KeyValuePair<string, string>("IsActivation", "False"));
                parameters.Add(new KeyValuePair<string, string>("OldEmail", userModel.Email));
                parameters.Add(new KeyValuePair<string, string>("ActivationPassportUserId", "0"));
                parameters.Add(new KeyValuePair<string, string>("Subdomain", "inntecdescuentos"));

                HttpClient client = new HttpClient();

                HttpResponseMessage response = client.PostAsync(@"https://inntecdescuentos.benefithub.com/Authentication/UpdateInvitationUserInfo", new FormUrlEncodedContent(parameters)).Result;


                Result = await RegisterBenefit(userModel.Email).ConfigureAwait(true);

                client.Dispose();

            }
            catch
            {

            }

            return Result;
        }

        /// <summary>
        /// Pre-Registro BenefitHub
        /// </summary>
        /// <param name="email">mail a verificar</param>
        /// <returns>Objeto que indica o no la existencia del usuario en BenefitHub</returns>
        public async Task<BenefitHubModel> RegisterBenefit(string email)
        {
            BenefitHubModel Result = new BenefitHubModel();
            try
            {
                var parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("ReferralCode", "WPEOMH"));//Codigo de referencia Benefit.
                parameters.Add(new KeyValuePair<string, string>("Email", email));
                parameters.Add(new KeyValuePair<string, string>("ConfirmEmail", email));

                HttpClient client = new HttpClient();

                HttpResponseMessage response = client.PostAsync(@"https://inntecdescuentos.benefithub.com/Account/EasySignup", new FormUrlEncodedContent(parameters)).Result;

                var url = response.RequestMessage.RequestUri;
                if (!url.ToString().Contains("UpdateInvitationUserInfo"))
                {
                    Result.uri = new Uri(@"https://inntecdescuentos.benefithub.com/");
                    Result.HTML = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                    Result.HTML = Result.HTML.Replace("name=\"Username\"", "name=\"Username\" value=" + email);
                    Result.exist = true;

                }
                else
                {
                    Result.uri = url;
                    Result.token = url.ToString().Substring(url.ToString().IndexOf('=') + 1, 36);
                    Result.exist = false;
                    Result.promo = true;

                }

                client.Dispose();
            }
            catch
            {

            }
            return Result;
        }

        /// <summary>
        /// Verificar Asistencia
        /// </summary>
        /// <param name="login">Usuario que inicio sesion</param>
        /// <param name="CsmId">Identificador unico de tarjeta</param>
        /// <returns></returns>
        public async Task<bool> VerifyAssist(int CsmId)
        {

            bool result;
            try
            {

                UriBuilder uriBuilder = new UriBuilder(Constants.Url_Base + $"/api/Servicios/Asistencias");
                uriBuilder.Query = "id=" + CsmId.ToString();
                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);

                var client = new HttpClient();

                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var resultTmp = JsonConvert.DeserializeObject(content);

                _ = bool.TryParse(content, out result);

            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Registro a Asistencia
        /// </summary>
        /// <param name="login">Usuario que inicio sesion</param>
        /// <param name="assistModel">Datos para dar de alta asistencia</param>
        /// <returns>indicador de proceso correcto</returns>
        public async Task<bool> RegisterAssist(AssistModel assistModel)
        {

            bool result;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + $"/api/Servicios/Asistencias/" + assistModel.CsmId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var resultTmp = JsonConvert.DeserializeObject(content);

                _ = bool.TryParse(content, out result);

            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
