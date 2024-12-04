using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Viatics
{
    /// <summary>
    /// Tiempo en que permanecera activa la tarjeta de combustible
    /// </summary>
    public class ViaticsFilterModel
    {
        /// <summary>
        /// Inicio de la activacion
        /// </summary>
        public DateTime dateStart { set; get; }
        /// <summary>
        /// Fecha en que volvera a estar inactiva la tarjeta
        /// </summary>
        public DateTime dateEnd { set; get; }
        /// <summary>
        /// Estatus en el que se encuenta la tarjeta.
        /// </summary>
        public int statusId { get; set; }
    }
}
