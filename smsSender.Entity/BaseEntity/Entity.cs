using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Data.Entity.BaseEntity
{
    public class Entity<T>
    {
        public virtual T Id
        {
            get;
             set;
        }

        public DateTime CreationDate
        {
            get;
             set;
        }

        public int DeletedStatus
        {
            get;
             set;
        }
    }
}
