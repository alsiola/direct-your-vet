using DYV.Models.ClientRelations;
using DYV.Services.Options;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;
using MScience.Sms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services.MailSMS
{
    public class DragonflySmsSender : ISmsProvider
    {
        private readonly DragonFlySmsOptions _options;
        private readonly TelemetryClient _telemetry;

        public DragonflySmsSender(IOptions<DragonFlySmsOptions> optionsAccessor, TelemetryClient telemetry)
        {
            _options = optionsAccessor.Value;
            _telemetry = telemetry;
        }
        public async Task<SMSSendResult> SendSmsAsync(string number, string message)
        {
            return await SendSmsAsync(number, message, null);
        }        

        private string AddUKPrefix(string number)
        {
            if (number.StartsWith("0"))
            {
                number = number.Substring(1);
                number = "+44" + number;
            }
            return number;
        }

        private SmsClient GetNewSmsClient()
        {
            return new SmsClient()
            {
                AccountId = _options.DragonFlyAccountId,
                Password = _options.DragonFlyPassword
            };
        }

        public async Task<SMSSendResult> SendSmsAsync(string number, string message, string signupCode, string from)
        {
            bool success = false;
            int remainingSMS = 0;
            string error = "";
            int messageId = 0;
            try
            {
                number = AddUKPrefix(number);

                SendResult sendResult = await GetNewSmsClient().SendAsync(number, from, message, 0, true);

                success = !sendResult.HasError;
                remainingSMS = sendResult.MessageBalance;
                messageId = sendResult.MessageId;

                if (!success)
                {
                    error = sendResult.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                success = false;
                error = ex.Message;
            }
            finally
            {
                if (remainingSMS < 100)
                {
                    _telemetry.TrackEvent("Low Dragonfly SMS Event: " + remainingSMS + " remaining.");
                }
            }

            return new SMSSendResult()
            {
                Number = number,
                Success = success,
                RemainingSMS = remainingSMS,
                Error = error,
                UniqueSignupSlug = signupCode,
                DragonFlyMessageIdentifier = messageId
            };
        }

        public async Task<SMSSendResult> SendSmsAsync(string number, string message, string signupCode)
        {
            return await SendSmsAsync(number, message, signupCode, "DYV");
        }
    }
}
