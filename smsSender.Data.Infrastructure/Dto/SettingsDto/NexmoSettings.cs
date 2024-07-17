using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Data.Infrastructure.Dto.SettingsDto
{
    public class NexmoSettings
    {
        public string ApiSecret { get; set; }
        public string ApiKey { get; set; }
        public string PhoneNumber { get; set; }
    }
}
