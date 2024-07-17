using smsSender.Data.Entity.Entity;
using smsSender.Data.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Core.IAppService
{
    public interface ISMSSenderAppService
    {
        Task<List<SMSLogDto>> GetAll(string phoneNumberSearch);
        Task<ResultBaseDto<bool>> Send(SMSDto sms);
    }
}
