using System;
using Newtonsoft.Json;

namespace FitbitNet.AspNetCore.WebHooks.Receivers
{
    /// <summary>
    /// Data transfer object which stores the data for the notification. The data in this object allows for further
    /// processing to occur once the full subscription end point processing has been complete.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// The type of collection the notification refers to eg. foods or activities
        /// </summary>
        [JsonProperty("collectionType")]
        public string CollectionType { get; set; }

        /// <summary>
        /// The date of which the notification was sent. This will not include the time or offset.
        /// </summary>
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ownerType")]
        public string OwnerType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }
    }
}
