using DYV.Models.ClientRelations;
using DYV.Services.MailSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly IEmailProvider _emailProvider;
        private readonly ISmsProvider _smsProvider;

        public AuthMessageSender(IEmailProvider emailProvider, ISmsProvider smsProvider)
        {
            _emailProvider = emailProvider;
            _smsProvider = smsProvider;
        }

        public async Task SendEmailAsync(string email, string subject, Dictionary<string, string> replacements, string template)
        {
            await _emailProvider.SendEmailAsync(email, subject, replacements, template);
        }

        public async Task<SMSSendResult> SendSmsAsync(string number, string message)
        {
            return await _smsProvider.SendSmsAsync(number, message);
        }

        public async Task<SMSSendResult> SendSmsAsync(string n, string message, string v)
        {
            return await _smsProvider.SendSmsAsync(n, message, v);
        }

        public async Task<SMSSendResult> SendSmsAsync(string number, string message, string signupCode, string from)
        {
            return await _smsProvider.SendSmsAsync(number, message, signupCode, from);
        }
    }
}
