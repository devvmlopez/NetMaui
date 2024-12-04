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
        /// Codigo Autorizacion
        /// </summary>
        public string CodigoAutorizacion { get; set; }

        /// <summary>
        /// Color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Signo del movimiento
        /// </summary>
        public int Signo { get; set; }

        /// <summary>
        /// Tipo de movimiento en Texto
        /// </summary>
        public decimal MontoProcesadoLocal => MontoMonendaLocal * Signo;

        /// <summary>
        /// Color para el texto del movimiento
        /// </summary>
        public string ColorMonto => Color;

        /// <summary>
        /// Descripcion del comercio sin tipo de movimiento
        /// </summary>
        public string ComercioAbreviado => Comercio;

        /// <summary>
        /// Estatus que regresa la peticion del servidor
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; internal set; }
    }
}
