using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Movimientos de la tarjeta
    /// </summary>
    public class MovementsModel
    {
        /// <summary>
        /// Listado de movimientos
        /// </summary>
        public List<MovementsDetailsModel> ListMovimientos { get; set; }
        /// <summary>
        /// Respueta
        /// </summary>
        public int Respuesta { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Detalle
        /// </summary>
        public string Detalle { get; set; }
        /// <summary>
        /// Estatus de la respuesta del servidor
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; internal set; }
    }
}
