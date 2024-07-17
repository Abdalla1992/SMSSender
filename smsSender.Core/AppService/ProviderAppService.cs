using Newtonsoft.Json.Linq;
using smsSender.Common.Enum;
using smsSender.Core.IAppService;
using smsSender.Core.SMSProvider.ProviderFactory;
using smsSender.Data.Entity.Entity;
using smsSender.Data.Infrastructure.Dto;
using smsSender.Data.Infrastructure.IRepository;
using smsSender.Data.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Core.AppService
{
    public class ProviderAppService :  IProviderAppService
    {
        private IUnitOfWork UnitOfWork { get; }
        public ProviderAppService(IUnitOfWork _unitOfWork) 
        {
              UnitOfWork = _unitOfWork;
        }

        public async Task<List<ProviderDto>> GetAll(string providerName)
        {
            List<ProviderDto> providerList=new List<ProviderDto>(); 
            List<Provider> providers= await UnitOfWork.ProviderRepository.GetAll(providerName);
            if (providers?.Count > default(int))
            {
                providerList = providers.Select(x => new ProviderDto()
                {
                    ApiUrl = x.ApiUrl,
                    Cost = x.Cost,
                    Name = x.Name,
                    Status = x.Status,
                    StatusStr = Enum.GetName(typeof(ProviderStatusEnum), x.Status)
                }).ToList();
            }
            return providerList;
        }

        public async Task<ResultBaseDto<bool>> Create(AddProviderDto providerDto)
        {
            ResultBaseDto<bool> response = new ResultBaseDto<bool>(false, "Error");
            string message = await Validate(providerDto);
            if (string.IsNullOrWhiteSpace(message))
            {
                if (!await UnitOfWork.ProviderRepository.IsExist(providerDto.Name))
                {
                    Provider provider = new Provider()
                    {
                        ApiUrl = providerDto.ApiUrl,
                        Cost = providerDto.Cost,
                        Name = providerDto.Name,
                        Status = providerDto.Status,
                        CreationDate = DateTime.Now,
                        DeletedStatus = (int)DeleteStatusEnum.NotDeleted
                    };
                    await UnitOfWork.ProviderRepository.Create(provider);
                    if (await UnitOfWork.Commit() > default(int))
                    {
                        response.SetResult(true, "Provider Added Successfully");
                    } 
                }
                else
                {
                    response.SetResult(false, "Provider exist before");
                }
            }
            else
            {
                response.SetResult(false, message);
            }
            return response;
        }

        private async Task<string> Validate(AddProviderDto request)
        {
            List<string> messages = new List<string>();
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                messages.Add("Name is required");
            }
            //if (string.IsNullOrWhiteSpace(request.ApiUrl))
            //{
            //    messages.Add("Api Url is required");
            //}
            if (request.Status <= default(int))
            {
                messages.Add("Status is required");
            }
            
            return string.Join(",", messages);
        }

       
    }
}
