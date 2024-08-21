using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Registro terminado
    /// </summary>
    public class RegisterResultModel
    {
        /// <summary>
        /// Mensaje indicando siguientes pasos
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Estatus que regresa el servidor a peticion de registro
        /// </summary>
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
    }
}
