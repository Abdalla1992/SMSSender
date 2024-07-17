using smsSender.Data.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Data.Entity.Entity
{
    public class SMSLog : Entity<int>
    {
        public string Receiver { get; set; }
        public string Message { get; set; }
        public string ResponseResult { get; set; }
        public int AttemptsCounter { get; set; }
        public int Status { get; set; }
        public decimal Cost { get; set; }

        [ForeignKey("Provider")]
        public int ProviderId { get; set; }
        public  Provider Provider { get; set; }
    }
}
