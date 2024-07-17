using Microsoft.EntityFrameworkCore.Metadata;
using Serilog;
using smsSender.Common.Enum;
using smsSender.Core.SMSProvider.Integration.Interface;
using smsSender.Data.Infrastructure.Dto;
using smsSender.Data.Infrastructure.Dto.SettingsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vonage;
using Vonage.Request;

namespace smsSender.Core.SMSProvider.Integration.Logic
{
    public class NexmoSmsProvider : ISmsProvider
    {

        private readonly NexmoSettings NexmoSettings;
        private readonly VonageClient _client;

        int NoOfRetry = 1;

        public NexmoSmsProvider(NexmoSettings nexmoSettings)
        {
            NexmoSettings = nexmoSettings;
        }

        public async Task<SmsProviderResultDto> SendSmsAsync(string recieverNumber, string message)
        {
            SmsProviderResultDto result = new SmsProviderResultDto() { ResponseResult = "Error", Status = (int)SmsStatusEnum.NotSent };
            try
            {
                var credentials = Credentials.FromApiKeyAndSecret(NexmoSettings.ApiKey, NexmoSettings.ApiSecret);
                VonageClient _client = new VonageClient(credentials);
                var response = await _client.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest
                {
                    To = recieverNumber,
                    From = NexmoSettings.PhoneNumber,
                    Text = message
                });

                if (response != null && response.Messages[0].StatusCode == Vonage.Messaging.SmsStatusCode.Success)
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
