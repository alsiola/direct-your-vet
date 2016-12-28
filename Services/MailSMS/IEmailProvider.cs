using System.Collections.Generic;
using System.Threading.Tasks;

namespace DYV.Services
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(string email, string subject, Dictionary<string, string> substitutions, string templateName);
    }
}
