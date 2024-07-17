
using smsSender.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using smsSender.Common.Enum;


namespace smsSender.Data.Infrastructure.Context
{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SingularizeTableNames(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }

        private void SingularizeTableNames(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var entity = modelBuilder.Entity(entityType.ClrType);
                if (entityType.ClrType.GetProperty("DeletedStatus") != null)
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "DeletedStatus");
                    var filterExpression = Expression.NotEqual(property, Expression.Constant( (int)DeleteStatusEnum.Deleted));

                    var filter = Expression.Lambda(filterExpression, parameter);

                    entity.HasQueryFilter(filter);
                }

                entity.ToTable(entityType.ClrType.Name);
            }
        }


    }
}
