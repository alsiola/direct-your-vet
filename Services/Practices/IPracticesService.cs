using DYV.Models.PracticeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DYV.Models.PlaceViewModels;
using DYV.Models;
using DYV.Models.AccountViewModels;

namespace DYV.Services.Practices
{
    public interface IPracticesService
    {
        Task<PracticeListViewModel> GetPracticesListViewModel();
        Task AddNewPractice(AddPracticeViewModel addPracticeViewModel);
        Task Unshare(int practiceId);
        Task<PracticeDetailsViewModel> GetPracticeDetailsViewModel(int value);
        Task<EditPracticeViewModel> GetEditPracticeViewModel(int value);
        Task<string> GetSignupSlugForCurrentManagerPractice();
        Task<JsonResponse> UpdatePractice(EditPracticeViewModel editPracticeViewModel);
        Task<string> GetCurrentUserPracticeName();
        Task<string> GetPracticeNameById(int value);
        Task<Practice> GetPracticeBySignupSlug(string slug);
        Task<JsonResponse> RemoveSubscriberFromPractice(SubscriberPractice subscriberPractice);
        Task<JsonResponse> UpdateUserManagerStatus(SubscriberPractice subscriberPractice);
        Task AddMessagePurchaseToPractice(int purchaseType, int qty);
    }
}
