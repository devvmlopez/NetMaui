using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Aclaraciones
{
    public class AclaracionModel
    {
        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int AclaracionId { get; set; }

        /// <summary>
        /// Folio de la aclaracion
        /// </summary>
        public string Folio { get; set; }



        /// <summary>
        /// Fecha en que se registro la aclaracion
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        public string Dia { get => FechaRegistro.Day.ToString(); }
        public string Mes
        {
            get => (FechaRegistro.Month == 1) ? "ENE" :
                   (FechaRegistro.Month == 2) ? "FEB" :
                   (FechaRegistro.Month == 3) ? "MAR" :
                   (FechaRegistro.Month == 4) ? "ABR" :
                   (FechaRegistro.Month == 5) ? "MAY" :
                   (FechaRegistro.Month == 6) ? "JUN" :
                   (FechaRegistro.Month == 7) ? "JUL" :
                   (FechaRegistro.Month == 8) ? "AGO" :
                   (FechaRegistro.Month == 9) ? "SEPT" :
                   (FechaRegistro.Month == 10) ? "OCT" :
                   (FechaRegistro.Month == 11) ? "NOV" : "DIC";

        }
        /// <summary>
        /// Fecha en que se finalizo la aclaracion
        /// </summary>
        public Nullable<DateTime> FechaFin { get; set; }

        /// <summary>
        /// Identificador CSM de la tarjeta
        /// </summary>
        public int UsuarioCsmTarjetaId { get; set; }

        /// <summary>
        /// Usuario de la app
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Comercio en que se genero el movimiento
        /// </summary>
        public string Comercio { get; set; }

        /// <summary>
        /// Comercio en que se genero el movimiento
        /// </summary>
        public DateTime FechaTransaccion { get; set; }

        /// <summary>
        /// Fecha en que se realizo el movimitno
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Identificador del estatus
        /// </summary>
        public byte AclaracionEstatusId { get; set; }
        public string StatusAclaracion { get; set; }
        /// <summary>
        /// Importe del movimiento
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Identifica si la aclaracion ya fue cobrada
        /// </summary>
        public byte AclaracionEstatusCobroId { get; set; }

        /// <summary>
        /// Importe del movimiento
        /// </summary>
        public Nullable<int> EstatusTransaccionId { get; set; }

        /// <summary>
        /// Identificador del motivo de la aclaracion
        /// </summary>
        public int MotivoId { get; set; }

        /// <summary>
        /// Id del producto
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Marca del producto
        /// </summary>
        public string Marca { get; set; }

        public string Nombre { get; set; }
        public string Tarjeta { get; set; }

        public System.Net.HttpStatusCode StatusCode { set; get; }

        public string Message { get; set; }

    }
}
