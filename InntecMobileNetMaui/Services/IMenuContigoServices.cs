using InntecMobileNetMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services
{
    public interface IMenuContigoServices<T>
    {
        Task<T> CheckBeneficionsContigoAsync(T user);
        Task<T> SuscripcionBeneficionsContigoAsync(T user);
    }
}
