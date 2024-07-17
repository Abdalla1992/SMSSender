
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace smsSender.Data.Infrastructure.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Properties
        public Context.AppDbContext AppDbContext { get; }
        #endregion
        #region Constructor
        internal BaseRepository(Context.AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
       
        }
        #endregion
        #region Methods

        #region Create
        public async Task<bool> Insert(T entity)
        {
            await AppDbContext.Set<T>().AddAsync(entity);
            return true;
        }
        #endregion
     

        #region Retrieve
       
        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null)
        {
            return await AppDbContext.Set<T>().AnyAsync(filter);
        }
        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }
        public async Task<List<T>> GetAllWithNoTracking(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }
        public async Task<List<T>> GetPage<TKey>(int skipCount, int takeCount, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, string includeProperties = "")
        {
            IQueryable<T> query = AppDbContext.Set<T>().Where(filter);
            skipCount *= takeCount;

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

           
            return await query.ToListAsync();

        }
        #endregion

        public bool SaveChanges()
        {
            return AppDbContext.SaveChanges() > default(int);
        }
        #endregion

    }
}