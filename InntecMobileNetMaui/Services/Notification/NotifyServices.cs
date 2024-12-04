using InntecMobileNetMaui.Models.Notify;
using InntecMobileNetMaui.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Notification
{
    public class NotifyServices : INotifyServices
    {
        public NotifyServices()
        {
        }

        /// <summary>
        /// Se obtienen las notificaciones que pertenesen al usuario dependiendo de los productos.
        /// </summary>
        /// <param name="Products"></param>
        /// <returns></returns>
        public async Task<List<NotifyUser>> getNotifications(string Products)
        {
            List<NotifyUser> Result = new List<NotifyUser>();
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/notificacion/ObtenerNotificacion/" + Products);

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<List<NotifyUser>>(content);
            }
            catch
            {
                Result = new List<NotifyUser>();
            }
            return Result;
        }
        /// <summary>
        /// Verifica si existen nuevas notificaciones para el usuario
        /// </summary>
        /// <param name="Products"></param>
        /// <returns></returns>
        public async Task<bool> verifyNewNotify(string Products)
        {
            bool Result;
            Constants.Products = Products;
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Get, Constants.Url_Base + "/api/notificacion/pendiente/" + Products);

                var client = new HttpClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Token_Type, Constants.Token);

                var response = await client.SendAsync(request).ConfigureAwait(true);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                Result = JsonConvert.DeserializeObject<bool>(content);
            }
            catch
            {
                Result = false;
            }
            return Result;
        }
        /// <summary>
        /// Identificar cual es la notificacion que se a abierto.
        /// </summary>
        /// <param name="notification">datos de la notificacion abierta.</param>
        /// <returns>Detalle de la solicitud</returns>
        public async Task<bool> SetNotificationReadtAsync(NotificationSent notification)
        {
            bool result = false;
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, Constants.Url_Base + "/api/notificacion/NotificacionLeida")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(notification),
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
                bool results = Convert.ToBoolean(JsonConvert.DeserializeObject(content));


            }
            catch
            {
                result = false;
            }
            return result;
        }


    }
}
