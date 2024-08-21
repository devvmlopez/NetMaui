using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    public class UserV2Model
    {

        /// <summary>
        /// Nombre de la persona
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Apellido paterno
        /// </summary>
        public string Paterno { get; set; }
        /// <summary>
        /// Apellido materno
        /// </summary>
        public string Materno { get; set; }
        /// <summary>
        /// Correo de la persona
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Numero celular para contacto
        /// </summary>
        public string Celular { get; set; }
        /// <summary>
        /// Usuario con el que se ingresara a la APP
        /// </summary>
        public string UsuarioNombre { get; set; }
        /// <summary>
        /// Tarjeta con la que se generara el registro
        /// </summary>
        public string Tarjeta { get; set; }
        /// <summary>
        /// Token unico para la alta de una tarjeta
        /// </summary>
        public string TokenUnico { get; set; }
        /// <summary>
        /// Password para ingresar a la APP
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Confirmacion de Password para identificar la correcta escritura.
        /// </summary>
        public string PasswordConfirm { get; set; }



    }
}
