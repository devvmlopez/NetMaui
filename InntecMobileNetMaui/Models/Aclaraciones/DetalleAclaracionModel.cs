using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Aclaraciones
{
    public class DetalleAclaracionModel
    {
        public string EstatusNuevo { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Dia { get => Fecha.Day.ToString(); }
        public string Mes
        {
            get => (Fecha.Month == 1) ? "ENE" :
                   (Fecha.Month == 2) ? "FEB" :
                   (Fecha.Month == 3) ? "MAR" :
                   (Fecha.Month == 4) ? "ABR" :
                   (Fecha.Month == 5) ? "MAY" :
                   (Fecha.Month == 6) ? "JUN" :
                   (Fecha.Month == 7) ? "JUL" :
                   (Fecha.Month == 8) ? "AGO" :
                   (Fecha.Month == 9) ? "SEPT" :
                   (Fecha.Month == 10) ? "OCT" :
                   (Fecha.Month == 11) ? "NOV" : "DIC";

        }
    }
}
