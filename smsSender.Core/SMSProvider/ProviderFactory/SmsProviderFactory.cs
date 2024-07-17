using Microsoft.Extensions.DependencyInjection;
using smsSender.Core.SMSProvider.Integration.Interface;
using smsSender.Core.SMSProvider.Integration.Logic;
using smsSender.Data.Infrastructure.Dto.SettingsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Core.SMSProvider.ProviderFactory
{
    public class SmsProviderFactory : ISmsProviderFactory
    {
        private readonly TwilioSettings TwilioSettings;
        private readonly NexmoSettings NexmoSettings;

        public SmsProviderFactory(TwilioSettings twilioSettings, NexmoSettings nexmoSettings
          )
        {
           TwilioSettings = twilioSettings;
            NexmoSettings = nexmoSettings;

        }

        public ISmsProvider CreateProvider(string providerName)
        {
            return providerName switch
            {
                "Twilio" => new TwilioSmsProvider(TwilioSettings),
                "Nexmo" => new NexmoSmsProvider(NexmoSettings),
                _ => throw new NotImplementedException($"Provider {providerName} is not implemented"),
            };
        }
    }
}
