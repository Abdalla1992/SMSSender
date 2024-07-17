using smsSender.Data.Entity.BaseEntity;
using smsSender.Data.Entity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Data.Infrastructure.Dto
{

    public class SMSLogDto
    {
        public int Id { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public string ResponseResult { get; set; }
        public int AttemptsCounter { get; set; }
        public int Status { get; set; }
        public string StatusStr { get; set; }
        public decimal Cost { get; set; }
        public string ProviderName { get; set; }

    }
}
