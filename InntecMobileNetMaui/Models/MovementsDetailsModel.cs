using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Detalle de movimientos
    /// </summary>
    public class MovementsDetailsModel
    {
        /// <summary>
        /// Monto del movimiento
        /// </summary>
        public decimal MontoMonendaLocal { get; set; }
        /// <summary>
        /// Monto del movmiento (Ref)
        /// </summary>
        public decimal MontoMonendaRef { get; set; }
        /// <summary>
        /// Fecha del movimiento
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Datos del comercio
        /// </summary>
        public string Comercio { get; set; }
        /// <summary>
        /// Estatus del movimiento
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Tipo de movimiento (Retiro, Depocito)
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Tipo de movimiento en Texto
        /// </summary>
        public string MontoProcesadoLocal => ((Tipo.ToUpper().Contains("RETIRO") || Tipo.ToUpper().Contains("CARGO") || Tipo.ToUpper().Contains("COMPRA")) && !Tipo.ToUpper().Contains("ABONO")) ? (MontoMonendaLocal * -1).ToString("F2") : MontoMonendaLocal.ToString("F2");

        /// <summary>
        /// Color para el texto del movimiento
        /// </summary>
        public string ColorMonto => ((Tipo.ToUpper().Contains("RETIRO") || Tipo.ToUpper().Contains("CARGO") || Tipo.ToUpper().Contains("COMPRA")) && !Tipo.ToUpper().Contains("ABONO")) ? "#FF0000" : "#00FF00";

        /// <summary>
        /// Descripcion del comercio sin tipo de movimiento
        /// </summary>
        public string ComercioAbreviado => (string.IsNullOrEmpty(Comercio)) ? "N/A" : (Comercio.Contains("-")) ? Comercio.Substring(0, Comercio.IndexOf('-') - 1) : ((Comercio.Contains("Cargo"))) ? Comercio.Substring(0, Comercio.IndexOf("Cargo") - 1) : ((Comercio.Contains("Abono"))) ? Comercio.Substring(0, Comercio.IndexOf("Abono") - 1) : Comercio;

        /// <summary>
        /// Estatus que regresa la peticion del servidor
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; internal set; }
    }
}
