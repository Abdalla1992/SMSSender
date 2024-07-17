

using smsSender.Data.Entity.Entity;

namespace smsSender.Data.Infrastructure.IRepository
{
    public interface IProviderRepository
    {
      
        Task<bool> Create(Provider provider);
        Task<List<Provider>> GetAll(string providerName);
        Task<List<Provider>>  GetAvailableProvidersAsync();
        Task<bool> IsExist(string name);
    }
}
