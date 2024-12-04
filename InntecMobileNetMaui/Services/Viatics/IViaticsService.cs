using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Viatics;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Viatics
{
    public interface IViaticsService<T>
    {
        Task<T> GetViaticsRequestAsync();
        Task<T> GetViaticsRequestAsyncV2(int id);
        //Task<T> GetViaticsRequestAsyncV2(int id);
        Task<T> SetViaticsRequestAsync(NewViaticsRequest newViaticsRequest);
        Task<T> SetViaticsRequestAsyncV2(NewViaticsRequest newViaticsRequest);
        Task<IEnumerable<object>> GetViaticsListAsync(ViaticsFilterModel filterModel , int UsuarioCsmTarjetaId);
        Task<IEnumerable<object>> GetViaticsConsumptionDetailAsync(ViaticsListModel viaticsListModel, int UsuarioCsmTarjetaId);
        Task<Models.Viatics.FileResult> sendFile(byte[] dataArray, string kind, ConsumptionDetail param);
    }
}
