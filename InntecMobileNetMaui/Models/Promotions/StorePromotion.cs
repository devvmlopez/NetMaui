using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Promotions
{
    //Objeto para almacenar las tiendas que contienen una promocion activa.
    public class StorePromotion
    {
        public int ComercioId { get; set; }
        public string Nombre { get; set; }
        public string URLImage { get; set; }
        public Thickness paddinCarousel { get; set; }
    }
}
