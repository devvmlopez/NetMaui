using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Notify
{
    public class NotificationSent
    {
        public int NotificacionEnviadaId { get; set; }
        public int NotificacionEstatusId { get; set; }
        public int NotificacionId { get; set; }
        public string Usuario { get; set; }
        public bool LetraNegra { get; set; }
        public string Imagen { get; set; }
    }
}
