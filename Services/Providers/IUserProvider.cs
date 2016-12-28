using System.Threading.Tasks;

namespace DYV.Services
{
    public interface IUserProvider
    {
        string GetUserId();
        Task<string> GetEmailAsync();
    }
}
