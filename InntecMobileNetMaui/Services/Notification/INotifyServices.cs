using InntecMobileNetMaui.Models.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Notification
{
    public interface INotifyServices
    {
        Task<bool> verifyNewNotify(string Products);
        Task<List<NotifyUser>> getNotifications(string Products);
        Task<bool> SetNotificationReadtAsync(NotificationSent notificacion);
    }
}
