using smsSender.Data.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Core.IAppService
{
    public interface IProviderAppService
    {
        Task<ResultBaseDto<bool>> Create(AddProviderDto provider);
        Task<List<ProviderDto>> GetAll(string providerName);
    }
}
