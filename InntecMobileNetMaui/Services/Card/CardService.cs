using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Card
{
    public class CardService : ICardService<CardModel>
    {
        /// <summary>
        /// Obtencion del listado de tarjetas relacionadas al usuario.
        /// </summary>
        /// <param name="login">objeto de login, para identificar al usuario</param>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CardModel>> GetItemsAsync(bool forceRefresh = false)
        {

            IEnumerable<CardModel> Result = null;

            var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/Tarjeta");

            var client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Instance.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);
            var response = await client.SendAsync(request).ConfigureAwait(true);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            Result = JsonConvert.DeserializeObject<List<CardModel>>(content);

            return Result;
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
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Instance.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);
            var response = await client.SendAsync(request).ConfigureAwait(true);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            BalanceModel balance = JsonConvert.DeserializeObject<BalanceModel>(content);
            balance.HttpStatusCode = response.StatusCode;
            return balance;
        }

        /// <summary>
        /// Se agregar una nueva tarjeta a un usuario.
        /// </summary>
        /// <param name="item">Tarjeta a agragar</param>
        /// <returns></returns>
        public async Task<CardModel> AddItemV2Async(CardModel item)
        {

            CardModel Result = null;
            try
            {
                item.EstatusDescripcion = "";

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/v2/Tarjeta");

                var client = new HttpClient();
                request.Content = new StringContent(JsonConvert.SerializeObject(item),
                                           Encoding.UTF8,
                                           "application/json");
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Instance.Token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);
                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<CardModel>(content);
                Result.StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                Result = item;
            }
            return Result;
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

            var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + $"/api/MovimientosMes/{card.UsuarioCsmTarjetaId}/{year}/{month}");

            var client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Instance.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);
            var response = await client.SendAsync(request).ConfigureAwait(true);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            MovementsModel movements = JsonConvert.DeserializeObject<MovementsModel>(content);
            movements.HttpStatusCode = response.StatusCode;
            return movements;
        }

        /// <summary>
        /// Bloqueo permanente de tarjeta.
        /// </summary>
        /// <param name="loginModel">Objeto de login</param>
        /// <param name="cardModel">Tarjeta que se bloqueara</param>
        /// <returns></returns>
        public async Task<CardModel> BlockItemAsync(CardModel cardModel)
        {
            CardModel Result;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + $"/api/Tarjeta/Estatus/{cardModel.UsuarioCsmTarjetaId}");
                var client = new HttpClient();

                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
               // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Instance.Token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);
                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<CardModel>(content);
                Result.StatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    Result.Message = "El estatus de la tarjeta ha sido actualizado con exito.";
                }
            }
            catch
            {
                Result = new CardModel();
                Result.StatusCode = System.Net.HttpStatusCode.NotFound;
                Result.Message = "Hubo un problema al actualizar el estatus de la tarjeta, intenta mas tarde.";
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
            CardModel Result;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + $"/api/Tarjeta/Ecommerce/")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(cardModel), Encoding.UTF8, "application/json")
                };
                var client = new HttpClient();

                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Instance.Token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<CardModel>(content);
                Result.StatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    Result.Message = Result.EstatusEcommerce ? "El estatus ha sido actualizado, a partir de este momento SI podrá realizar compras en línea." : "El estatus ha sido actualizado, a partir de este momento NO podrá realizar compras en línea.";
                }
                else
                {
                    Result.Message = "Ocurrio un problema con el cambio de estatus.";
                }
            }
            catch
            {
                Result = new CardModel();
                Result.StatusCode = System.Net.HttpStatusCode.NotFound;
                Result.Message = "Hubo un problema al actualizar el estatus de la tarjeta, intenta mas tarde.";
            }
            return Result;
        }
    }
}
