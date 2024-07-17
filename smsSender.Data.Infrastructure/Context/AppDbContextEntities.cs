using smsSender.Data.Entity;
using smsSender.Data.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace smsSender.Data.Infrastructure.Context
{
    public partial class AppDbContext
    {
        #region DBSet
        public DbSet<Provider> Provider { get; set; }
        public DbSet<SMSLog> SmsLog { get; set; }

        #endregion
    }
}
