using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Viatics
{
    /// <summary>
    /// Detalle de una solicitud de viaticos
    /// </summary>
    public class DetailsViaticsRequest
    {
        /// <summary>
        /// Identicador del detalle
        /// </summary>
        public int DetalleId { get; set; }
        /// <summary>
        /// Identificador de la solicitud a la que se le agregara un detalle
        /// </summary>
        public int SolicitudId { get; set; }
        /// <summary>
        /// Identificador del rubro a agregar
        /// </summary>
        public int RubroId { get; set; }
        /// <summary>
        /// Rubro en texto
        /// </summary>
        public string DescRubro { get; set; }
        /// <summary>
        /// Monto que se solicitara para el rubro
        /// </summary>
        public decimal MontoSolicitado { get; set; }
        /// <summary>
        /// Monto que se autoriza para el rubro seleccionado
        /// </summary>
        public decimal MontoAutorizado { get; set; }
        /// <summary>
        /// Monto que se comprueba
        /// </summary>
        public decimal MontoComprobado { get; set; }
        /// <summary>
        /// Estatus del rubro
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Id Unido de rubro (en caso de tener rubros repetidos con ciudad diferente)
        /// </summary>
        public int IdUnico { get; set; }
        /// <summary>
        /// Ciudad para la que se necesita el rubro
        /// </summary>
        public string Ciudad { get; set; }
    }
}
