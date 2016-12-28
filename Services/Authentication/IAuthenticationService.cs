using DYV.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<EnableQRCodeViewModel> GetEnableQRCodeViewModel();
        Task SetUserQREnabled();
        Task SetUserQRDisabled();
    }
}
