using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services.Providers
{
    public interface IInviteCodeProvider
    {
        string GetNewInviteCode();
        string GetNewInviteCode(int length);
    }
}
