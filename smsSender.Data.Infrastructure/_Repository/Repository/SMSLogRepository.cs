using AutoMapper;
using smsSender.Data.Entity.Entity;
using smsSender.Data.Infrastructure.Base;
using smsSender.Data.Infrastructure.Context;
using smsSender.Data.Infrastructure.IRepository;
using System.Linq.Expressions;

namespace smsSender.Data.Infrastructure.Repository
{
    public class SMSLogRepository : BaseRepository<SMSLog> , ISMSLogRepository 
    {
        public SMSLogRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<bool> Create(SMSLog SMSLog)
        {
            return await Insert(SMSLog);
        }

        public async Task<List<SMSLog>> GetAll(string phoneNumberSearch)
        {
            Expression<Func<SMSLog, bool>> filter = t => string.IsNullOrEmpty(phoneNumberSearch) || t.Receiver.ToLower().Trim().Contains(phoneNumberSearch.ToLower().Trim());
            return await GetAllWithNoTracking(filter, $"{nameof(Provider)}");
        }
    }
}
