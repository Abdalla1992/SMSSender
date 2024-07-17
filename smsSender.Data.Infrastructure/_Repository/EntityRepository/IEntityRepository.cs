namespace Enterprise.Events.Data.Infrastructure.Repository
{
    public interface IEntityRepository<T> : Base.IBaseRepository<T> where T : class
    {
    }
}
