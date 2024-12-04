using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Viatics
{
    /// <summary>
    /// Nueva solicitud de viaticos
    /// </summary>
    public class NewViaticsRequest
    {
        /// <summary>
        /// Identificador unico de la tarjeta
        /// </summary>
        public int UsuarioCsmTarjetaId { get; set; }

        /// <summary>
        /// Empleado que solicita
        /// </summary>
        public int EmpleadoId { get; set; }

        /// <summary>
        /// Fecha de salida
        /// </summary>
        public DateTime? FechaIni { get; set; }

        /// <summary>
        /// Fecha de regreso
        /// </summary>
        public DateTime? FechaFin { get; set; }

        /// <summary>
        /// Comentarios adicionales
        /// </summary>
        public string Comentarios { get; set; }

        /// <summary>
        /// Listado de ciudades y rubros que necesitara
        /// </summary>
        public List<DetailViatic> ListadoDetalleViatico { get; set; }

        /// <summary>
        /// Inicializacion de Objetos para la nueva solicitud de viaticos
        /// </summary>
        public NewViaticsRequest()
        {
            ListadoDetalleViatico = new List<DetailViatic>();
            UsuarioCsmTarjetaId = 0;
            EmpleadoId = 0;
            Comentarios = string.Empty;
        }
    }
    /// <summary>
    /// Detalle de la solicitud de viaticos
    /// </summary>
    public class DetailViatic
    {
        /// <summary>
        /// Identificador del rubro solicitado
        /// </summary>
        public int RubroId { get; set; }
        /// <summary>
        /// Descripcion del rubro
        /// </summary>
        public string RubroDesc { get; set; }
        /// <summary>
        /// Monto solicitado por rubro
        /// </summary>
        public decimal MontoSolicitado { get; set; }
        /// <summary>
        /// Id unico para el registro del rubro
        /// </summary>
        public int IDUnico { get; set; }
        /// <summary>
        /// Descripcion de la ciudad a la que pertenecera el rubro
        /// </summary>
        public string CiudadDesc { get; set; }
    }
}
