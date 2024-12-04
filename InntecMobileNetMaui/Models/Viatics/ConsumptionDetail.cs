    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Viatics
{
    /// <summary>
    /// Detalles de consumo
    /// </summary>
    public class ConsumptionDetail
    {
        /// <summary>
        /// Identificador del consumo
        /// </summary>
        public long ViaticoConsumoId { get; set; }
        /// <summary>
        /// Tarjeta con el que se realizo el movimiento
        /// </summary>
        public string NoTarjeta { get; set; }
        /// <summary>
        /// Datos del comercio
        /// </summary>
        public string DatosComerciante { get; set; }
        /// <summary>
        /// Monto de la compra
        /// </summary>
        public decimal MontoTransaccion { get; set; }
        /// <summary>
        /// Monto de comprobacion (Factura)
        /// </summary>
        public decimal MontoComprobado { get; set; }
        /// <summary>
        /// Fecha en que se registro el movimiento
        /// </summary>
        public DateTime? FechaRegistro { get; set; }
        /// <summary>
        /// Indicador de existencia de XML
        /// </summary>
        public bool ExisteXml { get; set; }
        /// <summary>
        /// Ruta en que se encuenta el archivo XML en caso de existir
        /// </summary>
        public string UrlXml { get; set; }
        /// <summary>
        /// Indicador de existencia de arhcivo PDF
        /// </summary>
        public bool ExistePdf { get; set; }
        /// <summary>
        /// Ruta en la que se encuenta el archivo PDF en caso de existir
        /// </summary>
        public string UrlPdf { get; set; }
        /// <summary>
        /// Indicador de existencia de comprobante, en caso de no existir factura
        /// </summary>
        public bool ExisteComp { get; set; }
        /// <summary>
        /// Ruta en la que se encuenta el archivo del comprobante en caso de existir
        /// </summary>
        public string UrlComprobante { get; set; }
        /// <summary>
        /// Estatus en el que se encuenta la comprobacion de los movimientos
        /// </summary>
        public byte StatusComprobacionId { get; set; }
        /// <summary>
        /// Estatus en Texto
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Observacines del movimiento
        /// </summary>
        public string Observacion { get; set; }
        /// <summary>
        /// Identificador del la solicitud a la que se quiere comprobar el gasto
        /// </summary>
        public int? SolicitudId { get; set; }
        /// <summary>
        /// Folio de comprobante
        /// </summary>
        public string Folio { get; set; }
        /// <summary>
        /// identificador del cliente que realiza la comprobacion
        /// </summary>
        public int ClienteId { get; set; }
        /// <summary>
        /// Identifador del origen
        /// </summary>
        public int Origen { get; set; }
        /// <summary>
        /// Nombre del archivo que comprueba.
        /// </summary>
        public string FileName { get; set; }
    }

    /// <summary>
    /// Resultado del envio de archivos
    /// </summary>
    public class FileResult
    {
        /// <summary>
        /// mensaje
        /// </summary>
        public String Msg;
        /// <summary>
        /// Respeusta del servidor
        /// </summary>
        public HttpRequestMessage HttpRequestMessage;
        /// <summary>
        /// Indicador del Estatus de la peticion.
        /// </summary>
        public HttpStatusCode HttpStatusCode;
    }
}
