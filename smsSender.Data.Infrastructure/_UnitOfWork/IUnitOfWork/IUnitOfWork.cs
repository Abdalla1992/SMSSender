using smsSender.Data.Infrastructure.IRepository;
using System;
using System.Threading.Tasks;

namespace smsSender.Data.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProviderRepository ProviderRepository { get; }
        ISMSLogRepository SMSLogRepository { get; }

        Task<int> Commit();
    }
}
