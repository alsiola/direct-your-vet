using DYV.Models;
using DYV.Services.Options;
using Google.Authenticator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services
{
    public class QRCodeTokenProvider : TotpSecurityStampBasedTokenProvider<ApplicationUser>, IUserTwoFactorTokenProvider<ApplicationUser>
    {
        private readonly QRCodeTokenProviderOptions _options;

        public QRCodeTokenProvider(IOptions<QRCodeTokenProviderOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(user.QRCodeEnabled);
        }

        public override Task<string> GenerateAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult("");
        }

        public override Task<bool> ValidateAsync(string purpose, string token, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            return Task.FromResult(tfa.ValidateTwoFactorPIN(_options.QRCodeSecret, token));
        }
    }
}
