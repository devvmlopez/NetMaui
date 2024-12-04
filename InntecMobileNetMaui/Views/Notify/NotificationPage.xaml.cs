using InntecMobileNetMaui.Models.Notify;
using InntecMobileNetMaui.ViewModels.Notify;

namespace InntecMobileNetMaui.Views.Notify;

public partial class NotificationPage : ContentPage
{
    public NotificationPage(NotifyUser notifyUser, bool itemRead)
    {

        InitializeComponent();

        

        new NotificationViewModel(new NotificationSent
        {
            NotificacionId = notifyUser.NotificacionId,
            NotificacionEnviadaId = notifyUser.NotificacionEnviadaId,
            NotificacionEstatusId = notifyUser.NotificacionEstatusId,
            Usuario = "",
            Imagen = notifyUser.Imagen,
            LetraNegra = notifyUser.LetraNegra
        }, itemRead);
        notifyUser.NotificacionEstatusId = 6;
        notifyUser.Alto = (DeviceInfo.Platform == DevicePlatform.Android) ?
                            (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 120
                            : ((DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 90 <= 577) ? (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 50 :
                            (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) - 90;
        this.BindingContext = notifyUser;

    }
}