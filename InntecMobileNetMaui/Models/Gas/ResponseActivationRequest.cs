using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Gas
{
    /// <summary>
    /// Reporte de estatus regresados por solicitud de activacion 
    /// </summary>
    public class ResponseActivationRequest
    {
        /// <summary>
        /// Indicador aprobacion
        /// </summary>
        public bool Aprobada { get; set; }
        /// <summary>
        /// Folio del movimiento
        /// </summary>
        public string Folio { get; set; }
        /// <summary>
        /// Listado de observaciones (Motivos por los que no se activo la tarjeta)
        /// </summary>
        public List<string> ListObservaciones { get; set; }
        /// <summary>
        /// Inicializacion del listado(Modelo)
        /// </summary>
        public ResponseActivationRequest()
        {
            ListObservaciones = new List<string>();
        }
    }
}
