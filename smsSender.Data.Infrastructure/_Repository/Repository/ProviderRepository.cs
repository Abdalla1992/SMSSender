using AutoMapper;
using smsSender.Common.Enum;
using smsSender.Data.Entity.Entity;
using smsSender.Data.Infrastructure.Base;
using smsSender.Data.Infrastructure.Context;
using smsSender.Data.Infrastructure.IRepository;
using System.Linq.Expressions;

namespace smsSender.Data.Infrastructure.Repository
{
    public class ProviderRepository : BaseRepository<Provider> , IProviderRepository 
    {
        public ProviderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<bool> Create(Provider provider)
        {
            return await Insert(provider);
        }

        public async Task<List<Provider>> GetAll(string providerName)
        {
            Expression<Func<Provider, bool>> filter = t =>  string.IsNullOrEmpty(providerName) || t.Name.ToLower().Trim().Contains(providerName.ToLower().Trim());
            return await GetAllWithNoTracking( filter);
        }

        public async Task<List<Provider>> GetAvailableProvidersAsync()
        {
            return await GetWhereAsync(x => x.Status == (byte)ProviderStatusEnum.Active);
        }

        public async Task<bool> IsExist(string name)
        {
            return await GetAnyAsync(x => x.Name.Trim().ToLower().Equals(name.Trim().ToLower()));
        }
    }
}
