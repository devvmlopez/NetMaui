using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Cambio de NIP
    /// </summary>
    public class CardNipModel
    {
        /// <summary>
        /// Numero de tarjeta
        /// </summary>
        public string Tarjeta { get; set; }

        /// <summary>
        /// Nuevo NIP
        /// </summary>
        public string Nip { get; set; }

        /// <summary>
        /// Fecha de vencimiento de la tarjeta
        /// </summary>
        public string FechaVencimiento { get; set; }

        /// <summary>
        /// Identificador unico de tarjeta
        /// </summary>
        public int UsuarioCsmTarjetaId { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }
}
