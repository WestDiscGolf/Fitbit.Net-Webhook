using FitbitNet.AspNetCore.WebHooks.Receivers.Filters;

namespace FitbitNet.AspNetCore.WebHooks.Receivers
{
    /// <summary>
    /// The Fitbit webhook receiver options to allow for configuration to be configurable.
    /// </summary>
    public class FitbitWebhookReceiverOptions
    {
        /// <summary>
        /// Specifies whether the body of the request is processed in <see cref="FitbitVerifySignatureFilter"/>.
        /// </summary>
        /// <remarks>
        /// Default value is `true`
        /// </remarks>
        public bool EnableSignatureChecking { get; set; } = true;
    }
}
