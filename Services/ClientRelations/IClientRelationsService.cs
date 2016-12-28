using DYV.Models;
using DYV.Models.ClientRelations;
using DYV.Models.Purchase;
using DYV.Models.Purchase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services.ClientRelations
{
    public interface IClientRelationsService
    {
        Task<PurchaseCostsViewModel> GetPurchaseCostsViewModel();
        Task<PaymentResult> MakePayment(StripeTokenViewModel model);
        Task<ClientRelationsIndexViewModel> GetIndexViewModel();
        Task<int> GetMsgQuotaForCurrentPractice(PurchaseType type);
        Task<SMSGroupSendResult> SendSMS(SendSMSViewModel vm);
        Task<List<GroupSendResultViewModel>> GetMessageGroupResults<T1>() where T1 : MessageGroupResult;
        Task<List<T3>> GetMessageResultsByGroupId<T1, T2, T3>(int groupid) where T1 : class, IMessageSendResult<T2> where T2 : MessageGroupResult where T3 : MessageSendResultViewModel;
        Task SetMarketingCodeAsOpened(string marketingSlug);
        Task SetMarketingCodeAsRegistered(string marketingCode);
    }
}
