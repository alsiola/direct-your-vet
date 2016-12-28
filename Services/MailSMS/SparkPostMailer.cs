using DYV.Services.Options;
using Microsoft.Extensions.Options;
using SparkPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services
{
    public class SparkPostMailer : IEmailProvider
    {
        private readonly SparkPostOptions _options;

        public SparkPostMailer(IOptions<SparkPostOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public async Task SendEmailAsync(string email, string subject, Dictionary<string,string> substitutions, string templateName)
        {
            var transmission = new Transmission();
            transmission.Content.TemplateId = templateName;
            transmission.Content.From.Email = "testing@sparkpostbox.com";

            foreach(var entry in substitutions)
            {
                transmission.SubstitutionData[entry.Key] = entry.Value;
            }

            var recipient = new Recipient()
            {
                Address = new Address() { Email = email }
            };

            transmission.Recipients.Add(recipient);

            var client = new Client(_options.SparkPostApiKey);

            await client.Transmissions.Send(transmission);
        }
    }
}
