using smsSender.Data.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Core.SMSProvider.Integration.Interface
{
    public interface ISmsProvider
    {
        public Task<SmsProviderResultDto> SendSmsAsync(string phoneNumber, string message);
    }
}
