using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Gas
{
    /// <summary>
    /// Datos necesarios para la activacion de una tarjeta de combustibles.
    /// </summary>
    public class ActivationRequestModel
    {
        /// <summary>
        /// codigo unico de QR
        /// </summary>
        public string _guid { get; set; }
        /// <summary>
        /// identificador unico de tarjeta
        /// </summary>
        public int CsmUsuarioId { get; set; }
        /// <summary>
        /// cantidad con la que contara la tarjeta al ser activada
        /// </summary>
        public decimal Mount { get; set; }
        /// <summary>
        /// Precio por litro de gasolina
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// Lectura del kilometraje al momento de la activacion de la tarjeta
        /// </summary>
        public decimal KM { get; set; }
        /// <summary>
        /// Cantidad de litos a comprar
        /// </summary>
        public decimal Liters { get; set; }
        /// <summary>
        /// Latitud del lugar donde se activo la tarjeta
        /// </summary>
        public double GpsLat { get; set; }
        /// <summary>
        /// Longitud del lugar donde se activo la tarjeta
        /// </summary>
        public double GpsLng { get; set; }
        /// <summary>
        /// Variable que indica si el QR debe o no ser escaneado.
        /// </summary>
        public byte EscaneoQR { get; set; }
    }
}
