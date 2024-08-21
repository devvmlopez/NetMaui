using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Recuperacion de password
    /// </summary>
    public class RecoverPasswordModel
    {
        /// <summary>
        /// Correo a donde se enviara la recuperacion
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Usuario
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Mensaje de la solicitud de la recuperacion
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Estatus que regresa la peticion al servidor
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}
