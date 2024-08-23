using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Balance de la tarjeta
    /// </summary>
    public class BalanceModel
    {
        /// <summary>
        /// Saldo disponible de la tarjeta
        /// </summary>
        public decimal SaldoDisponible { get; set; }
        /// <summary>
        /// Limite de credito
        /// </summary>
        public decimal CreditoLimite { get; set; }
        /// <summary>
        /// Respuesta
        /// </summary>
        public int Respuesta { get; set; }
        /// <summary>
        /// Descripcion del monto
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Deatlle adicional
        /// </summary>
        public string Detalle { get; set; }
        /// <summary>
        /// Estatus de la solicidud al servidor
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; internal set; }
    }
}
