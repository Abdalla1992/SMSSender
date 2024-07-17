using System.Linq.Expressions;


namespace smsSender.Data.Infrastructure.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<List<T>> GetPage<TKey>(int skipCount, int takeCount, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression = null, string includeProperties = "");

        Task<bool> Insert(T entity);

        bool SaveChanges();
    }
}
