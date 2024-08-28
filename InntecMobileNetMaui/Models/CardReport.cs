using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Reportes de tarejta
    /// </summary>
    public class CardReport
    {
        /// <summary>
        /// Identificador del reporte
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tarjeta a reportar
        /// </summary>
        public string Tarjeta { get; set; }
        /// <summary>
        /// Motivo por el que se reportara la tarjeta
        /// </summary>
        public string Motivo { get; set; }
        /// <summary>
        /// Plataforma por la cual se esta reportando (MOVIL)
        /// </summary>
        public string Plataforma { get; set; }
        /// <summary>
        /// Folio del reporte
        /// </summary>
        public string Folio { get; set; }
        /// <summary>
        /// Mensaje
        /// </summary>
        public string Mensaje { get; set; }
    }
}
