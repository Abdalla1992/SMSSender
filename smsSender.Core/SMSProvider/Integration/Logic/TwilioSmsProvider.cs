using Serilog;
using Serilog.Core;
using smsSender.Common.Enum;
using smsSender.Core.SMSProvider.Integration.Interface;
using smsSender.Data.Infrastructure.Dto;
using smsSender.Data.Infrastructure.Dto.SettingsDto;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace smsSender.Core.SMSProvider.Integration.Logic
{
    public class TwilioSmsProvider : ISmsProvider
    {
        private readonly TwilioSettings TwilioSettings;
        int NoOfRetry = 1;

        public TwilioSmsProvider(TwilioSettings twilioSettings)
        {
            TwilioSettings = twilioSettings;
        }

        public async Task<SmsProviderResultDto> SendSmsAsync(string recieverNumber, string message)
        {
            SmsProviderResultDto result = new SmsProviderResultDto() { Status = (int)SmsStatusEnum.NotSent, ResponseResult = "Error" };
            try
            {
                TwilioClient.Init(TwilioSettings.AccountSid, TwilioSettings.AuthToken);
                var messageOptions = new CreateMessageOptions(new PhoneNumber(recieverNumber))
                {
                    From = new PhoneNumber(TwilioSettings.PhoneNumber),
                    Body = message
                };
                var response = MessageResource.Create(messageOptions);
                if (response != null && (response.Status == MessageResource.StatusEnum.Sent || response.Status == MessageResource.StatusEnum.Queued))
                {
                    //SENT 
                    result.Status = (int)SmsStatusEnum.Sent;
                    result.ResponseResult = "Message sent successfully";

                }
            }

            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred: {ex.Message}");
                result.ResponseResult = $"An unexpected error occurred: {ex.Message}";
            }

            return result;
        }
    }
}
