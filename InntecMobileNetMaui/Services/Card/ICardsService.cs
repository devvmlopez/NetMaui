using InntecMobileNetMaui.Models;
using InntecMobileNetMaui.Models.Assist;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services
{
    public interface ICardsService<T>
    {
        //Task<T> AddItemAsync(T item);
        Task<T> AddItemV2Async(T item);
        Task<bool> UpdateItemAsync(T item);
        //Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetItemsV2Async(bool forceRefresh = false);
        //Task<string> DeleteItemAsync(T card, bool forceRefresh = false);
        Task<string> DeleteItemV2Async(T card, bool forceRefresh = false);
        //Task<string> CancelItemAsync(CardReport userModel);
        Task<string> ReportarItemV2Async(CardReport userModel); 
        Task<BalanceModel> GetBalanceAsync(T card);
        Task<MovementsModel> GetMovementsAsync(T card);
        Task<MovementsModel> GetMovementsMonthAsync(T card, int year, int month);
        //Task<T> BlockItemAsync(T card);
        Task<T> BlockItemV2Async(T card);
        Task<T> BlockEcommerceItemAsync(T card);
        //Task<CardNipModel> ChangeNIPAsync(CardNipModel cardNip);
        Task<CardNipModel> ChangeNipV2Async(CardNipModel cardNip);
        Task<BenefitHubModel> RegisterBenefit(string email);
        Task<BenefitHubModel> RegisterNewUserBenefit(UserModel userModel, BenefitHubModel requestResult);

        Task<bool> VerifyAssist(int CsmId);

        Task<bool> RegisterAssist(AssistModel assistModel);
    }
}
