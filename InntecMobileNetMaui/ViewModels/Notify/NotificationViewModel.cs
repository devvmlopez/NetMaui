using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Notify
{
    public class NotificationViewModel : BaseViewModel
    {
        public NotificationViewModel(Models.Notify.NotificationSent notification, bool itemRead)
        {
            if (!itemRead)
                DataNotify.SetNotificationReadtAsync(notification);
        }
    }
}
