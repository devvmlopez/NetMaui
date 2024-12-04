using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Viatics
{

    /// <summary>
    /// Indicador de la solicitud de viaticos
    /// </summary>
    public class ViaticsRequest
    {
        /// <summary>
        /// Identificador de la solicitud
        /// </summary>
        public int SolicitudId { get; set; }
        /// <summary>
        /// Fecha de inicio
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha de termino
        /// </summary>
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Monto que se solicito
        /// </summary>
        public decimal MontoSolicitado { get; set; }
        /// <summary>
        /// Monto autorizado
        /// </summary>
        public decimal MontoAutorizado { get; set; }
        /// <summary>
        /// Monto comprobado
        /// </summary>
        public decimal MontoComprobado { get; set; }
        /// <summary>
        /// Comentarios
        /// </summary>
        public string Comentarios { get; set; }
        /// <summary>
        /// Estatus en el que se encuenta la solicitud de viaticos
        /// </summary>
        public byte EstatusSolicitudId { get; set; }
        /// <summary>
        /// Estatus de la solicitud en Texto
        /// </summary>
        public string EstatusSolicitud { get; set; }
        /// <summary>
        /// Identificador del cliente
        /// </summary>
        public int ClienteId { get; set; }
        /// <summary>
        /// Identificador del empleado
        /// </summary>
        public int EmpleadoId { get; set; }
        /// <summary>
        /// Numero de la tarjeta a la que se asignara el viatico
        /// </summary>
        public string NoTarjeta { get; set; }
        /// <summary>
        /// Origen
        /// </summary>
        public string Origen { get; set; }
        /// <summary>
        /// Folio de la solicitud
        /// </summary>
        public string Folio { get; set; }
        /// <summary>
        /// Centro de costos del que se tomara el capital
        /// </summary>
        public int CentroDeCostoId { get; set; }
        /// <summary>
        /// Descripcion adicional
        /// </summary>
        public string DescCc { get; set; }
        /// <summary>
        /// Identicicador de la solicitud
        /// </summary>
        public string Identificador { get; set; }
        /// <summary>
        /// Puesto de quien revisa la solicitud
        /// </summary>
        public int PuestoId { get; set; }
        /// <summary>
        /// Puesto en Texto
        /// </summary>
        public string DescPuesto { get; set; }
        /// <summary>
        /// Detalle de la solicitud
        /// </summary>
        public List<DetailsViaticsRequest> ListDetalleSolicitud { get; set; }

        /// <summary>
        /// Inicializacion del detalle
        /// </summary>
        public ViaticsRequest()
        {
            ListDetalleSolicitud = new List<DetailsViaticsRequest>();
        }
    }
}
