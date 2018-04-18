using System;
using FitbitNet.AspNetCore.WebHooks;
using FitbitNet.AspNetCore.WebHooks.Receivers;
using Microsoft.AspNetCore.Mvc;

namespace FitbitCoreReceiver.Controllers
{
    public class FitbitController : ControllerBase
    {
        [FitbitWebHook]
        public IAsyncResult FitbitSubscription(string id, Notification[] data)
        {
            return null;
        }

        [FitbitWebHook(Id="my_id")]
        public IAsyncResult FitbitSubscriptionId(Notification[] data)
        {
            return null;
        }
    }
}
