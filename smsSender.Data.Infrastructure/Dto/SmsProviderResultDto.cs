using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Data.Infrastructure.Dto
{
    public class SmsProviderResultDto
    {
        public int Status { get; set; }
        //public int AttemptsCounter { get; set; }
        public string ResponseResult { get; set; }
    }
}
