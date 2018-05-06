using FitbitNet.AspNetCore.WebHooks.Receivers.Filters;
using Microsoft.AspNetCore.WebHooks.Metadata;

namespace FitbitNet.AspNetCore.WebHooks.Receivers.Metadata
{
    /// <summary>
    /// Class which defines the <see cref="FitbitMetadata" /> for the <see cref="FitbitWebHookAttribute" /> attribute. 
    /// This specifies the type of body type the requests are expecting as well as defining the receiver name.
    /// </summary>
    public class FitbitMetadata : WebHookMetadata, IWebHookFilterMetadata
    {
        private readonly FitbitVerifySubscriberFilter _fitbitVerifySubscriberFilter;
        private readonly FitbitVerifySignatureFilter _fitbitVerifySignatureFilter;
        private readonly FitbitActionResultFilter _fitbitActionResultFilter;

        /// <summary>
        /// Basic constructor defining that the receiver name is <see cref="FitbitConstants.ReceiverName"/> taking in the dependencies the 
        /// webhook receiver requires
        /// </summary>
        public FitbitMetadata(
            FitbitVerifySubscriberFilter fitbitVerifySubscriberFilter,
            FitbitVerifySignatureFilter fitbitVerifySignatureFilter,
            FitbitActionResultFilter fitbitActionResultFilter)
            : base(FitbitConstants.ReceiverName)
        {
            _fitbitVerifySubscriberFilter = fitbitVerifySubscriberFilter;
            _fitbitVerifySignatureFilter = fitbitVerifySignatureFilter;
            _fitbitActionResultFilter = fitbitActionResultFilter;
        }

        /// <summary>
        /// Fitbit webhook only supports the Json body type
        /// </summary>
        public override WebHookBodyType BodyType => WebHookBodyType.Json;

        /// <inheritdoc />
        public void AddFilters(WebHookFilterMetadataContext context)
        {
            context.Results.Add(_fitbitVerifySubscriberFilter);
            context.Results.Add(_fitbitVerifySignatureFilter);
            context.Results.Add(_fitbitActionResultFilter);
        }
    }
}
