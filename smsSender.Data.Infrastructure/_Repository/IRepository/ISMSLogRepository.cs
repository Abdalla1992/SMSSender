

using smsSender.Data.Entity.Entity;

namespace smsSender.Data.Infrastructure.IRepository
{
    public interface ISMSLogRepository
    {
      
        Task<bool> Create(SMSLog provider);
        Task<List<SMSLog>> GetAll(string phoneNumberSearch);
    }
}
