using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Data.Infrastructure.Dto
{
    public class AddProviderDto
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public decimal Cost { get; set; }
        public string ApiUrl { get; set; }
    }
}
