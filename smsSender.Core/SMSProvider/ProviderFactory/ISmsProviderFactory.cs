using smsSender.Core.SMSProvider.Integration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smsSender.Core.SMSProvider.ProviderFactory
{
    public interface ISmsProviderFactory
    {
        ISmsProvider CreateProvider(string providerName);
    }
}
