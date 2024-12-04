using InntecMobileNetMaui.Models.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Promociones
{
    public interface IPromotionServices<T>
    {
        Task<List<StorePromotion>> getStorePromotion();
        Task<List<PromotionModel>> getPromotion(int StoreId);
    }
}
