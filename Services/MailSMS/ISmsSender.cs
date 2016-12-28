using DYV.Models.ClientRelations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services
{
    public interface ISmsSender
    {
        Task<SMSSendResult> SendSmsAsync(string number, string message);
        Task<SMSSendResult> SendSmsAsync(string n, string message, string v);
        Task<SMSSendResult> SendSmsAsync(string number, string message, string signupCode, string from);
    }
}
