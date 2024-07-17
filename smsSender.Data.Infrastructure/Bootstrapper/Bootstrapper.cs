using smsSender.Data.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace smsSender.Data.Infrastructure
{
    public static class Bootstrapper
    {
        public static void AddRepository(this IServiceCollection services)
        {
           // services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped(typeof(UnitOfWork.IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            
        }
    }
}
    