using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace FitbitNet.AspNetCore.WebHooks.Receivers.Extensions
{
    /// <summary>
    /// Extension methods for setting up Fitbit Subscription WebHooks in an <see cref="IMvcCoreBuilder" />.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class FitbitMvcCoreBuilderExtensions
    {
        /// <summary>
        /// <para>
        /// Add Fitbit WebHook configuration and services to the specified <paramref name="builder"/>.
        /// </para>
        /// <para>
        /// The '<c>WebHooks:fitbit:SecretKey:{id}</c>' configuration value contains the secret key for 
        /// Fitbit URIs of the form '<c>https://{host}/api/webhooks/incoming/fitbit/{id}?verify={secret key}</c>'.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="IMvcCoreBuilder" /> to configure.</param>
        /// <returns>The <paramref name="builder"/>.</returns>
        public static IMvcCoreBuilder AddFitbitWebHooks(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return AddFitbitWebHooks(builder, options => { });
        }

        /// <summary>
        /// <para>
        /// Add Fitbit WebHook configuration and services to the specified <paramref name="builder"/>.
        /// </para>
        /// <para>
        /// The '<c>WebHooks:fitbit:SecretKey:{id}</c>' configuration value contains the secret key for 
        /// Fitbit URIs of the form '<c>https://{host}/api/webhooks/incoming/fitbit/{id}?verify={secret key}</c>'.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="IMvcCoreBuilder" /> to configure.</param>
        /// <param name="setupAction">
        /// The setup action for <see cref="FitbitWebhookReceiverOptions"/> to determine how the receivers will process requests.
        /// </param>
        /// <returns>The <paramref name="builder"/>.</returns>
        public static IMvcCoreBuilder AddFitbitWebHooks(this IMvcCoreBuilder builder, Action<FitbitWebhookReceiverOptions> setupAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            FitbitServiceCollectionSetup.AddFitbitServices(builder.Services, setupAction);

            return builder
                .AddJsonFormatters()
                .AddWebHooks();
        }
    }
}
