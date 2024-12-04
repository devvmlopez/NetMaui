using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Gas
{
    /// <summary>
    /// Respuesta de la activaion de la tarjeta de combustible
    /// </summary>
    public class ActivationRequestReportModel
    {
        /// <summary>
        /// identificador unico de la solicitud de la activacion de la tarjeta.
        /// </summary>
        public int? SolicitudId { get; set; }
        /// <summary>
        /// Folio de la activacion
        /// </summary>
        public string Folio { get; set; }
        /// <summary>
        /// Tarjeta que se activo
        /// </summary>
        public string NoTarjeta { get; set; }
        /// <summary>
        /// Monto que se solicitu para la activacion de la tarejta.
        /// </summary>
        public decimal? MontoSolicitado { get; set; }
        /// <summary>
        /// Precio del litro de combustible
        /// </summary>
        public decimal? Precio { get; set; }
        /// <summary>
        /// Kilometraje actual del vehiculo
        /// </summary>
        public decimal? KmActual { get; set; }
        /// <summary>
        /// Litros suministrados
        /// </summary>
        public decimal? Litros { get; set; }
        /// <summary>
        /// Fecha de solicitud
        /// </summary>
        public DateTime? FechaSolicitud { get; set; }
        /// <summary>
        /// Estatus en el que quedo la tarjeta
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Color (Idiciador visual) que indica el estatus en que quedo la activacion
        /// </summary>
        public string Color => string.Equals(Estatus, "Finalizo") ? "#009945" : string.Equals(Estatus, "Aprobada") ? "#337dc4" : string.Equals(Estatus, "Declinada") ? "#cc001f" : "";
        /// <summary>
        /// Latitud de solicitud de activacion
        /// </summary>
        public decimal? GpsLatitud { get; set; }
        /// <summary>
        /// Longitud de solicitud de activacion
        /// </summary>
        public decimal? GpsLongitud { get; set; }
        /// <summary>
        /// Indicador de si se varifico el QR
        /// </summary>
        public bool? EscaneoQr { get; set; }
        /// <summary>
        /// Detalles de la solicitud
        /// </summary>
        public List<DetalleTrackingSolicitud> ListadoDetalleTrackingSolicitud { get; set; }

        /// <summary>
        /// Inicializacion del modelo para la activacion.
        /// </summary>
        public ActivationRequestReportModel()
        {

            ListadoDetalleTrackingSolicitud = new List<DetalleTrackingSolicitud>();
        }
    }

    /// <summary>
    /// Detalle de la solicitud de activacion
    /// </summary>
    public class DetalleTrackingSolicitud
    {
        /// <summary>
        /// identificador del detalle
        /// </summary>
        public int TrackingSolicitudId { get; set; }
        /// <summary>
        /// identificador de la solicitud de activacion
        /// </summary>
        public int SolicitudId { get; set; }
        /// <summary>
        /// Fecha del movimiento
        /// </summary>
        public DateTime? FechaMovimiento { get; set; }
        /// <summary>
        /// observaciones del por que no se activa la tarjeta(en caso de ser denegada).
        /// </summary>
        public string Observaciones { get; set; }
    }
}
