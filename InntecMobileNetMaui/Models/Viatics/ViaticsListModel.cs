using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Viatics
{
    /// <summary>
    /// Datos generales de la solicitud
    /// </summary>
    public class ViaticsListModel
    {
        /// <summary>
        /// Fecha en que se debe iniciar
        /// </summary>
        public DateTime fechaDesde { get; set; }
        /// <summary>
        /// Fecha en que se debe regresar
        /// </summary>
        public DateTime fechaHasta { get; set; }
        /// <summary>
        /// Folio de la solicitud
        /// </summary>
        public string folio { get; set; }
        /// <summary>
        /// Estatus en el que se encuenta la solicitud
        /// </summary>
        public int status { get; set; }
    }
}
