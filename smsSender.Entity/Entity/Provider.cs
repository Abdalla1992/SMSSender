using smsSender.Data.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Data.Entity.Entity
{
    public class Provider : Entity<int>
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public decimal Cost { get; set; }
        public string ApiUrl { get; set; }

    }
}
