using smsSender.Common.Enum;
using smsSender.Core.IAppService;
using smsSender.Core.SMSProvider.ProviderFactory;
using smsSender.Data.Entity.Entity;
using smsSender.Data.Infrastructure.Dto;
using smsSender.Data.Infrastructure.IRepository;
using smsSender.Data.Infrastructure.Repository;
using smsSender.Data.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Twilio.Http;

namespace smsSender.Core.AppService
{
    public class SMSSenderAppService : ISMSSenderAppService
    {

        private IUnitOfWork UnitOfWork;
        private readonly ISmsProviderFactory _providerFactory;
        private readonly IServiceProvider _serviceProvider;

        public SMSSenderAppService(IUnitOfWork _unitOfWork, ISmsProviderFactory providerFactory)
        {
            UnitOfWork = _unitOfWork;
            _providerFactory = providerFactory;
        }

        public async Task<List<SMSLogDto>> GetAll(string phoneNumberSearch)
        {
            List<SMSLogDto> SMSLogList = new List<SMSLogDto>();
            List<SMSLog> sMSLogs = await UnitOfWork.SMSLogRepository.GetAll(phoneNumberSearch);
            if (sMSLogs?.Count() > default(int))
            {
                SMSLogList = sMSLogs.Select(s => new SMSLogDto()
                {
                    AttemptsCounter = s.AttemptsCounter,
                    Cost = s.Cost,
                    Id = s.Id,
                    Message = s.Message,
                    ProviderName = s.Provider.Name,
                    Receiver = s.Receiver,
                    ResponseResult = s.ResponseResult,
                    Status = s.Status,
                    StatusStr = Enum.GetName(typeof(SmsStatusEnum), s.Status)
                }).ToList();
            }
            return SMSLogList;
        }

        public async Task<ResultBaseDto<bool>> Send(SMSDto smsDto)
        {
            ResultBaseDto<bool> response = new ResultBaseDto<bool>(false, "Error");
            string message = await Validate(smsDto);
            if (string.IsNullOrWhiteSpace(message))
            {
                Provider selectedProviderInfo = new Provider();
                SmsProviderResultDto result = new();
                Random _random = new Random();
                int attempts = default;
                List<Provider> availableProviders = await UnitOfWork.ProviderRepository.GetAvailableProvidersAsync();
                if (availableProviders.Any())
                {
                    #region Send Message
                    decimal LowestCost = availableProviders.Where(x => x.Status == (byte)ProviderStatusEnum.Active).Min(x => x.Cost);
                    availableProviders = availableProviders.Where(x => x.Cost == LowestCost).ToList();
                    // Load balance among the best providers
                    while (attempts < availableProviders.Count)
                    {
                        attempts++;
                        selectedProviderInfo = availableProviders[_random.Next(availableProviders.Count)];
                        var selectedProvider = _providerFactory.CreateProvider(selectedProviderInfo.Name);
                        result = await selectedProvider.SendSmsAsync(smsDto.recipient, smsDto.message);
                        if (result.Status == (byte)SmsStatusEnum.Sent) { break; }
                   
                    }
                    #endregion

                    bool isLogged = await LogSMS(smsDto, result, selectedProviderInfo, attempts);

                    response.SetResult(false, result.ResponseResult);
                }
                else
                {
                    response.SetResult(false, "No Available Providers");
                }


            }
            else
            {
                response.SetResult(false, message);
            }
            return response;
        }

        private async Task<bool> LogSMS(SMSDto smsDto, SmsProviderResultDto result, Provider provider, int attempts)
        {
            SMSLog smsLog = new SMSLog()
            {
                Status = result.Status,
                Cost = provider.Cost,
                ProviderId = provider.Id,
                Message = smsDto.message,
                Receiver = smsDto.recipient,
                AttemptsCounter = attempts,
                ResponseResult = result.ResponseResult,
                CreationDate = DateTime.Now,
                DeletedStatus = (int)DeleteStatusEnum.NotDeleted,
            };

            await UnitOfWork.SMSLogRepository.Create(smsLog);
            if (await UnitOfWork.Commit() > default(int))
            {
                return true;
            }
            return false;
        }

        private async Task<string> Validate(SMSDto request)
        {
            List<string> messages = new List<string>();
            if (string.IsNullOrWhiteSpace(request.message))
            {
                messages.Add("message is required");
            }
            if (string.IsNullOrWhiteSpace(request.recipient))
            {
                messages.Add("recipient is required");
            }
            if (!Regex.IsMatch(request.recipient, @"^[0-9+]+$"))
            {
                messages.Add("recipient invalid");
            }


            return string.Join(",", messages);
        }


    }
}
