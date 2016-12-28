using DYV.Services.Options;
using Microsoft.Extensions.Options;

namespace DYV.Services
{
    public class ApiKeyManager
    {
        GoogleMapsOptions _googleMapsOptions;
        StripeOptions _stripeOptions;

        public ApiKeyManager(IOptions<GoogleMapsOptions> optionsAccessor, IOptions<StripeOptions> stripeAccessor)
        {
            _googleMapsOptions = optionsAccessor.Value;
            _stripeOptions = stripeAccessor.Value;
        }

        public string GetGoogleMapsApiKey()
        {
            return _googleMapsOptions.GMapsApiKey;
        }

        public string GetStripePublicKey()
        {
            return _stripeOptions.StripeTestKeyPublishable;
        }

        public string GetStripeSecretKey()
        {
            return _stripeOptions.StripeTestKeySecret;
        }
    }
}
