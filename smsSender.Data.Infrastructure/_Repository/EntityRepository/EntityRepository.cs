using AutoMapper;
namespace Enterprise.Events.Data.Infrastructure.Repository
{
    public class EntityRepository<T> : Base.BaseRepository<T>, IEntityRepository<T> where T : class
    {
      
        public EntityRepository(Context.AppDbContext appDbContext, IMapper mapper) : base(appDbContext, mapper)
        {
          

        }
    }
}
