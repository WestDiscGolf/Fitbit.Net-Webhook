using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebHooks;
using Microsoft.AspNetCore.WebHooks.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FitbitNet.AspNetCore.WebHooks.Receivers.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitVerifySignatureFilter : WebHookVerifySignatureFilter, IAsyncResourceFilter
    {
        private readonly FitbitWebhookReceiverOptions _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        public FitbitVerifySignatureFilter(
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory,
            IOptions<FitbitWebhookReceiverOptions> options)
            : base(configuration, hostingEnvironment, loggerFactory)
        {
            _options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ReceiverName => FitbitConstants.ReceiverName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (_options.EnableSignatureChecking)
            {
                //var routeData = context.RouteData;
                var request = context.HttpContext.Request;
                if (HttpMethods.IsPost(request.Method))
                {
                    // 1. Confirm a secure connection
                    var errorResult = EnsureSecureConnection(ReceiverName, context.HttpContext.Request);
                    if (errorResult != null)
                    {
                        context.Result = errorResult;
                        return;
                    }

                    // 2. Get the header value
                    var header = GetRequestHeader(request, FitbitConstants.SignatureHeaderName, out errorResult);
                    if (errorResult != null)
                    {
                        context.Result = errorResult;
                        return;
                    }

                    // note: no need to urldecode as specified by fitbit in their community forum
                    // https://community.fitbit.com/t5/Web-API-Development/Subscription-Notification-signature-validation/m-p/921974/highlight/true#M2930
                    var expectedHash = Convert.FromBase64String(header);

                    // 3. get the OAuth secret from the configuration
                    var secretAsString = GetSecretKey(FitbitConstants.ReceiverName, context.RouteData, FitbitConstants.OAuthClientMinLength);

                    // "consumer_secret&"
                    var secret = Encoding.ASCII.GetBytes($"{secretAsString}&");

                    // 4. get the actual hash
                    var actualHash = await ComputeRequestBodySha1HashAsync(request, secret);

                    // 5. Verify
                    if (!SecretEqual(expectedHash, actualHash))
                    {
                        // todo: need more logging!
                        // todo: need to log remote ip, incoming signature and income message content
                        context.Result = new NotFoundResult();
                        return;
                    }
                }
            }

            await next();
        }

        /// <summary>
        /// Specify how the <see cref="FitbitVerifySignatureFilter"/> can access the OAuthClient value applicable the this application which the webhook is part of
        /// </summary>
        /// <param name="sectionKey">The section key for the Configuration. This will be the <see cref="FitbitConstants.ReceiverName"/> passed through.</param>
        /// <param name="_">The <see cref="RouteData"/> parameter is not used as part of this override.</param>
        /// <returns>The applicable <see cref="IConfigurationSection"/> which allows access to the OAuthClient value</returns>
        protected override IConfigurationSection GetSecretKeys(string sectionKey, RouteData _)
        {
            if (sectionKey == null)
            {
                throw new ArgumentNullException(nameof(sectionKey));
            }

            var key = ConfigurationPath.Combine(
                WebHookConstants.ReceiverConfigurationSectionKey,
                sectionKey,
                FitbitConstants.OAuthClientSecretKey);

            return Configuration.GetSection(key);
        }
    }
}
