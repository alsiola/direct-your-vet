using DYV.Models;
using DYV.Models.AccountViewModels;
using DYV.Models.PracticeViewModels;
using DYV.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services.User
{
    public interface IUserService
    {
        Task<UserInvite> CreateInvite(InviteViewModel invite);
        Task<InviteCodeResult> GetCodeDetails(string code);
        Task AddUserToPractice(string id, int practiceId);
        Task RemoveUserFromPractice(string id);
        Task<List<SubscriberUserViewModel>> GetUsersForPracticeManager();
        Task<SubscriberUserViewModel> GetUserById(string id);
        List<int> GetCurrentUserManagedPracticeIds();
    }
}
