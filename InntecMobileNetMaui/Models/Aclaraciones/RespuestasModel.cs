using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models.Aclaraciones
{
    public class RespuestasModel
    {
        public int PreguntaId { set; get; }
        public int? FormatoId { set; get; }
        public string Pregunta { set; get; }
        public string Marca { get; set; }
        public bool? Estatus { get; set; }
    }
}
