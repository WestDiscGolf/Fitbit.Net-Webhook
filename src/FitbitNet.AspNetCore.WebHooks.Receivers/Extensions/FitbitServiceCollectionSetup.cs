using System;
using FitbitNet.AspNetCore.WebHooks.Receivers.Filters;
using FitbitNet.AspNetCore.WebHooks.Receivers.Metadata;
using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FitbitNet.AspNetCore.WebHooks.Receivers.Extensions
{
    internal static class FitbitServiceCollectionSetup
    {
        public static void AddFitbitServices(IServiceCollection services, Action<FitbitWebhookReceiverOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // register the options action
            services.Configure(setupAction);

            WebHookMetadata.Register<FitbitMetadata>(services);

            services.TryAddSingleton<FitbitVerifySubscriberFilter>();
            services.TryAddSingleton<FitbitVerifySignatureFilter>();
        }
    }
}
