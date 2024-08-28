using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Models
{
    /// <summary>
    /// Listado de meses
    /// </summary>
    public class Months
    {
        /// <summary>
        /// Acomodo de meses
        /// </summary>
        public List<Month> _months = new List<Month>()
        {
            new Month() { Id = 1, Name = "Enero" },
            new Month() { Id = 2, Name = "Febrero" },
            new Month() { Id = 3, Name = "Marzo" },
            new Month() { Id = 4, Name = "Abril" },
            new Month() { Id = 5, Name = "Mayo" },
            new Month() { Id = 6, Name = "Junio" },
            new Month() { Id = 7, Name = "Julio" },
            new Month() { Id = 8, Name = "Agosto" },
            new Month() { Id = 9, Name = "Septiembre" },
            new Month() { Id = 10, Name = "Octubre" },
            new Month() { Id = 11, Name = "Noviembre" },
            new Month() { Id = 12, Name = "Diciembre" },
            new Month() { Id = 99, Name = "Recientes" }
        };


    }

    /// <summary>
    /// Mes
    /// </summary>
    public class Month
    {
        /// <summary>
        /// Identificador del mes
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del mes
        /// </summary>
        public string Name { get; set; }

    }
}
