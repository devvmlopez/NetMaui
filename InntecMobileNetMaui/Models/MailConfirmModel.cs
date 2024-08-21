using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{ 
  /// <summary>
  /// Confirmacion de correo
  /// </summary>
    public class MailConfirmModel
    {
        /// <summary>
        /// Correo al que se enviara
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Codigo
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Usuario
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Nuevo Password
        /// </summary>
        public string NewPassword { get; set; }
    }
}
