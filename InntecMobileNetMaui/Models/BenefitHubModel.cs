using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Informacion para BenefitHub
    /// </summary>
    public class BenefitHubModel
    {
        /// <summary>
        /// Ruta a BenefitHub (PreRegistro-Registro) Respuesta del Servidor
        /// </summary>
        public Uri uri { get; set; }
        /// <summary>
        /// Codigo HTML devuelto de una peticion a BenefitHub
        /// </summary>
        public string HTML { get; set; }
        /// <summary>
        /// Estatus en el que se encuenta el mail enviado en la peticion a BenefitHub
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Indicador de si el mail tiene o no cuenta registrada en BenefitHub
        /// </summary>
        public bool exist { get; set; }
        /// <summary>
        /// Token que indica el PreRegistro de un usuario a BenefitHub
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// Aceptacion de terminos
        /// </summary>
        public bool terms { get; set; }
        /// <summary>
        /// Aceptacion de recibir correos
        /// </summary>
        public bool promo { get; set; }
        public BenefitHubModel()
        {
        }
    }
}
