using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Promotions
{
    public class PromotionModel
    {
        public int? ComercioId { get; set; }
        public int PromocionId { get; set; }
        public string UrlImage { get; set; }
        public string UrlPromo { get; set; }
    }
}
