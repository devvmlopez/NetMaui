using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Login registro
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Token de logeo
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// Usuario (No usado)
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// Usuario para login
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Password del usuario
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Identificador del cliente
        /// </summary>
        public int client_id { get; set; }
        /// <summary>
        /// Tiempo en que expira la sesion
        /// </summary>
        public Int64 expires_in { get; set; }
        /// <summary>
        /// Fecha en que expira la sesion
        /// </summary>
        public DateTime expires { get; set; }
        /// <summary>
        /// emision
        /// </summary>
        public DateTime issued { get; set; }
        /// <summary>
        /// Tipo de tiken regresado
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// Mensaje de error
        /// </summary>
        public string error { get; set; }
        /// <summary>
        /// Descripcion del error
        /// </summary>
        public string error_description { get; set; }
        /// <summary>
        /// Estatus de la peticion al servidor
        /// </summary>
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
        /// <summary>
        /// Indicador para recordar la contraseña
        /// </summary>
        public bool rememberPWS { get; set; }
        /// <summary>
        /// Rol
        /// </summary>
        public string rol { get; set; }
    }
}
