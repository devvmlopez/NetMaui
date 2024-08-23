using InntecMobileNetMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Card
{
    public interface ICardService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<MovementsModel> GetMovementsMonthAsync(T card, int year, int month);
        Task<BalanceModel> GetBalanceAsync(T card);
        Task<T> AddItemV2Async(T item);
        Task<T> BlockItemAsync(T card);
        Task<T> BlockEcommerceItemAsync(T card);
    }
}
