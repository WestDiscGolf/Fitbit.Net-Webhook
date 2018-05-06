using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FitbitNet.AspNetCore.WebHooks.Receivers.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitActionResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // determine the result
            if (context.Result != null)
            {
                // now we need to determine if its a 2xx and change it to a 204
                // or if it's a 4xx and change it to a 404 not found
                if (context.Result is StatusCodeResult statusResult)
                {
                    // todo: log
                    if (statusResult.StatusCode >= (int)HttpStatusCode.OK
                        && statusResult.StatusCode <= 299)
                    {
                        context.Result = new NoContentResult();
                    }

                    if (statusResult.StatusCode >= (int)HttpStatusCode.BadRequest
                        && statusResult.StatusCode <= 499)
                    {
                        context.Result = new NotFoundResult();
                    }
                }
                else
                {
                    // todo: log the result
                    context.Result = null;
                }
            }

            if (context.Result == null)
            {
                context.Result = new NoContentResult();
            }

            await next();
        }
    }
}