
using smsSender.Data.Infrastructure.Context;
using smsSender.Data.Infrastructure.IRepository;
using smsSender.Data.Infrastructure.Repository;

namespace smsSender.Data.Infrastructure.UnitOfWork
{
    public partial class UnitOfWork : IUnitOfWork
    {
        public AppDbContext AppDbContext { get; }

        public IProviderRepository ProviderRepository => new ProviderRepository(AppDbContext);

        public ISMSLogRepository SMSLogRepository => new SMSLogRepository(AppDbContext);

        public UnitOfWork(AppDbContext appContext)
        {
            AppDbContext = appContext;
        }

        public Task<int> Commit()
        {
            return AppDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {

        }
    }
}
