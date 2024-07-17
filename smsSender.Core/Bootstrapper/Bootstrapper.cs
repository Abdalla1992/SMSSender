

using Microsoft.Extensions.DependencyInjection;
using smsSender.Core.AppService;
using smsSender.Core.IAppService;
using smsSender.Core.SMSProvider.Integration.Interface;
using smsSender.Core.SMSProvider.Integration.Logic;
using smsSender.Core.SMSProvider.ProviderFactory;
using System.Reflection;

namespace smsSender.Core
{
    public static class Bootstrapper
    {
        public static void AddAppService(this IServiceCollection services)
        {
            Type[] appServices = Assembly.Load(typeof(ProviderAppService).Assembly.GetName()).GetTypes().Where(a => a.IsClass).ToArray();
            Type[] iAppServices = Assembly.Load(typeof(IProviderAppService).Assembly.GetName()).GetTypes().Where(a => a.IsInterface).ToArray();

            foreach (Type iAppService in iAppServices)
            {
                Type classType = appServices.FirstOrDefault(x => iAppService.IsAssignableFrom(x));
                if (classType != null)
                {
                    services.AddScoped(iAppService, classType);
                }
            }

            services.AddScoped<ISmsProvider, NexmoSmsProvider>();
            services.AddScoped<ISmsProvider, TwilioSmsProvider>();
            services.AddSingleton<ISmsProviderFactory, SmsProviderFactory>();
        }
    }
}
