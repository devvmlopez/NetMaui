using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Datos del usuario
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Token unico para registro
        /// </summary>
        public string TokenUnico { get; set; }

        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public int UsuarioId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Tarjetas pertenecientes a usuario
        /// </summary>
        public string Tarjeta { get; set; }
        /// <summary>
        /// Vigencias
        /// </summary>
        public string Vigencia => Mes + "/" + Anio;
        /// <summary>
        /// Mes
        /// </summary>
        public string Mes { get; set; }
        public string Anio { get; set; }
        /// <summary>
        /// Usuario
        /// </summary>
        public string UsuarioNombre { get; set; }
        /// <summary>
        /// Contraseña
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Confirmacion de contraseña
        /// </summary>
        public string PasswordConfirm { get; set; }
        /// <summary>
        /// Nombre del usuario
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
        /// Correo
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Telefono
        /// </summary>
        public string Telefono { get; set; }
        /// <summary>
        /// Celular
        /// </summary>
        public string Celular { get; set; }
        /// <summary>
        /// Estatus del registro
        /// </summary>
        public byte Estatus { get; set; }
        /// <summary>
        /// Estatus del correo enviado
        /// </summary>
        public bool EstatusCorreo { get; set; }
        /// <summary>
        /// RFC del usuario
        /// </summary>
        public string Rfc { get; set; }
        /// <summary>
        /// CURP del usuario
        /// </summary>
        public string Curp { get; set; }
        /// <summary>
        /// Numero se seguro social
        /// </summary>
        public string Nss { get; set; }
        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Beneficiario
        /// </summary>
        public string Beneficiarios { get; set; }
        /// <summary>
        /// Tipo de asistencia
        /// </summary>
        public string TipoAsistencia { get; set; }
        /// <summary>
        /// Estatus debuelto por el servidor para el registo de la solicitud.
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }

        /// <summary>
        /// mensaje enviado por el servidor para identificar el estatus en el que se encuenta el proceso
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Codigo postal del usaurio
        /// </summary>
        public string CP { get; set; }
        /// <summary>
        /// inicializacion del modelo para el registro de la persona
        /// </summary>
        public UserModel()
        {
            UsuarioId = 0;
            Id = "";
            Estatus = 0;
            EstatusCorreo = false;
            Rfc = "";
            Curp = "";
            Nss = "";
            Observaciones = "";
            Beneficiarios = "";
            TipoAsistencia = "";
        }
    }
}
