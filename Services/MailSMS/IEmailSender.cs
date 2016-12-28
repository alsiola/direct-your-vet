using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, Dictionary<string,string> replacements, string template);
    }
}
