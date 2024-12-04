using InntecMobileNetMaui.Models.Gas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.Services.Gas
{
    public interface IGasService<T>
    {
        Task<bool> LinkedUser(T _guid);
        Task<bool> UnLinkedUser();
        Task<bool> VerifyQR(T _guid);
        Task<List<string>> CheckRestrictions(int csmId);
        Task<ResponseActivationRequest> ActivateCard(ActivationRequestModel requestActivation);
        Task<IEnumerable<ActivationRequestReportModel>> GetActivationRequest(int csmUsuarioId, RequestActivationFilterModel filterModel, bool forceRefresh = false);
        Task<decimal> GetKM(int? tarjetaId);

    }
}
