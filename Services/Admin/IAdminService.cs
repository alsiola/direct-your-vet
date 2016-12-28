using DYV.Models.User;
using System.Threading.Tasks;

namespace DYV.Services.Admin
{
    public interface IAdminService
    {
        Task<SubscriberUser> GetUserById(string id);
    }
}
