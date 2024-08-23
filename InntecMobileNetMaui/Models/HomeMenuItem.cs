using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Menu principal
    /// </summary>
    class HomeMenuItem
    {
        /// <summary>
        /// Identificador del Menu
        /// </summary>
        public Enumeradores.enumMenuItemType Id { get; set; }
        /// <summary>
        /// Imagen a mostrar
        /// </summary>
        public string ImgMenu { get; set; }
        /// <summary>
        /// Nombre de la opcion
        /// </summary>
        public string Title { get; set; }
    }
}
