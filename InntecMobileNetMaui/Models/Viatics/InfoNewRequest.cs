using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Viatics
{ 
    /// <summary>
  /// Nueva solicitud de viaticos
  /// </summary>
    public class InfoNewRequest
    {
        /// <summary>
        /// Identificador unico de tarjeta
        /// </summary>
        public int UsuarioCsmTarjetaId { get; set; }

        /// <summary>
        /// Numero de tarjeta
        /// </summary>
        public string Tarjeta { get; set; }

        /// <summary>
        /// Descripcion del estatus de la solicitud
        /// </summary>
        public string EstatusDescripcion { get; set; }

        public long EstatusId { get; set; }

        /// <summary>
        /// Cliente al que pertenece la tarjeta
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// Empelado de la tarjeta
        /// </summary>
        public int EmpleadoId { get; set; }

        /// <summary>
        /// Datos del lugar al que pertenecera la solicitud
        /// </summary>
        public List<InfoCity> InfoCiudades { get; set; }

        /// <summary>
        /// Rubros que perteneceran a la solicitud
        /// </summary>
        public List<InfoItem> InfoRubros { get; set; }
        /// <summary>
        /// Codigo de la solicitud de viaticos
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }
        /// <summary>
        /// Mensaje del motivo de la solicitud
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Inicializacion de las listas que contendra la solicitud de viaticos
        /// </summary>
        public InfoNewRequest()
        {
            InfoCiudades = new List<InfoCity>();
            InfoRubros = new List<InfoItem>();
        }
    }
    /// <summary>
    /// Informacion del Lugar para los rubros
    /// </summary>
    public class InfoCity
    {
        /// <summary>
        /// Identicador unico de la ciudad
        /// </summary>
        public int IdUnico { get; set; }
        /// <summary>
        /// Ciudad-Ciudad-Pais
        /// </summary>
        public string Nombre { get; set; }
    }

    /// <summary>
    /// Rubros de la solicitud
    /// </summary>
    public class InfoItem
    {
        /// <summary>
        /// Identificador del rubro
        /// </summary>
        public int RubroId { get; set; }
        /// <summary>
        /// Descripcion del rubro
        /// </summary>
        public string Descripcion { get; set; }
    }
}
