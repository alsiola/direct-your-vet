using DYV.Data;
using DYV.Models.ManageViewModels;
using DYV.Services.Options;
using Google.Authenticator;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserProvider _userProvider;
        private readonly QRCodeTokenProviderOptions _options;
        private readonly ApplicationDbContext _context;

        public AuthenticationService(IUserProvider userProvider, IOptions<QRCodeTokenProviderOptions> optionsAccessor, ApplicationDbContext context)
        {
            _userProvider = userProvider;
            _options = optionsAccessor.Value;
            _context = context;
        }

        public async Task<EnableQRCodeViewModel> GetEnableQRCodeViewModel()
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var info = tfa.GenerateSetupCode("Direct Your Vet", await _userProvider.GetEmailAsync(), _options.QRCodeSecret, 300, 300);

            return new EnableQRCodeViewModel()
            {
                QRCodeURL = info.QrCodeSetupImageUrl,
                ManualEntryKey = info.ManualEntryKey
            };
        }

        public async Task SetUserQREnabled()
        {
            await SetUserQR(true);
        }

        public async Task SetUserQRDisabled()
        {
            await SetUserQR(false);
        }

        private async Task SetUserQR(bool isEnabled)
        {
            var user = _context.Users.Where(db => db.Id == _userProvider.GetUserId()).SingleOrDefault();

            if (user == null)
                throw new InvalidOperationException("No logged in user found.");

            user.QRCodeEnabled = isEnabled;

            await _context.SaveChangesAsync();
        }
    }
}
