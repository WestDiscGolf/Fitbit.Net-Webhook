using Microsoft.AspNetCore.WebHooks;

namespace FitbitNet.AspNetCore.WebHooks.Receivers
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitWebHookAttribute : WebHookAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public FitbitWebHookAttribute() : base("fitbit")
        {
        }
    }
}
