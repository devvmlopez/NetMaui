using InntecMobileNetMaui.Models.Promotions;
using InntecMobileNetMaui.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Promociones
{
    public class PromotionServices : IPromotionServices<StorePromotion>
    {
        public PromotionServices()
        {
        }



        /// <summary>
        /// Obtener listado de promociones asociadas a un comercio.
        /// </summary>
        /// <param name="StoreId">Identificador del comercio.</param>
        /// <returns></returns>
        public async Task<List<PromotionModel>> getPromotion(int StoreId)
        {
            List<PromotionModel> Result = new List<PromotionModel>();
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/promociones/getPromocionesActivas/" + StoreId.ToString());

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<List<PromotionModel>>(content);
            }
            catch
            {
                Result = new List<PromotionModel>();
            }
            return Result;
        }

        /// <summary>
        /// Obtener un lisatdo de tiendas que tengan alguna promocion activa.
        /// </summary>
        /// <returns></returns>
        public async Task<List<StorePromotion>> getStorePromotion()
        {
            List<StorePromotion> Result = new List<StorePromotion>();
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/promociones/getPromociones");

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<List<StorePromotion>>(content);
            }
            catch
            {
                Result = new List<StorePromotion>();
            }
            return Result;
        }
    }
}
