using InntecMobileNetMaui.Models.Notify;
using InntecMobileNetMaui.Resources;
using InntecMobileNetMaui.Views.Notify;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.ViewModels.Notify
{
    public class NotifyViewModel : BaseViewModel
    {
        public Command getNotificationsCommand;
        public Command ShowNotifyCommand { get; set; }
        public ObservableCollection<NotifyUser> Notifications { get; set; }
        private NotityPage _notityPage;
        public NotifyViewModel(NotityPage notifypage)
        {
            Title = "Notificaciones";
            this._notityPage = notifypage;
            Notifications = new ObservableCollection<NotifyUser>();
            getNotificationsCommand = new Command(async () => await ExecutegetNotificationsCommand().ConfigureAwait(true));
            ShowNotifyCommand = new Command((obj) => ExecuteShowNotifyCommand((NotifyUser)obj));
        }
        private async Task ExecutegetNotificationsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            List<NotifyUser> getNotify = await DataNotify.getNotifications(Constants.Products);
            foreach (NotifyUser item in getNotify)
            {
                Notifications.Add(item);
                if (item.Estatus == "Leida")
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                        Preferences.Default.Set("IconNotificacion", "notification.svg");
                    else
                        Preferences.Default.Set("IconNotificacion", "pngnotification.png");
                }
                else
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                        Preferences.Default.Set("IconNotificacion", "notificationr.svg");
                    else
                        Preferences.Default.Set("IconNotificacion", "pngnotificationr.png");
                }
            }
            IsBusy = false;
        }
        private void ExecuteShowNotifyCommand(NotifyUser obj)
        {
            bool itemRead = false;
            var i = Notifications.Where(item => item.NotificacionEnviadaId == obj.NotificacionEnviadaId).FirstOrDefault();
            if (i.NotificacionEstatusId == 6)
                itemRead = true;
            i.NotificacionEstatusId = 6;
            _notityPage.Navigation.PushAsync(new NotificationPage(obj, itemRead));
        }

        
    }
}
