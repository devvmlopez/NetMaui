using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Aclaraciones
{
    public class DatosDocumento
    {
        public string Tarjetahabiente { get; set; }
        public string Tarjeta { get; set; }
        public decimal MontoTotalDisputado { get; set; }
        public string Moneda { get; set; }

        public List<MotivosReclamacion> MotivoReclamacionList { get; set; }

        public string FechaTransaccion { get; set; }
        public string Comercio { get; set; }
        public decimal Monto { get; set; }

        public string DetalleSuceso { get; set; }
        public string Otro { get; set; }
    }

    public class MotivosReclamacion
    {
        public string Nombre { get; set; }
        public bool Checked { get; set; }
    }
}
