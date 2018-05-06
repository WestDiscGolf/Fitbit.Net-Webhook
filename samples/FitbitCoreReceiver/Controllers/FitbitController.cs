using FitbitNet.AspNetCore.WebHooks.Receivers;
using Microsoft.AspNetCore.Mvc;

namespace FitbitCoreReceiver.Controllers
{
    public class FitbitController : ControllerBase
    {
        [FitbitWebHook]
        public IActionResult FitbitSubscription(string id, Notification[] data)
        {
            return null;
        }

        [FitbitWebHook(Id="my_id")]
        public IActionResult FitbitSubscriptionId(Notification[] data)
        {
            return Ok();
        }
    }
}
