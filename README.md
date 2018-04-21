# Fitbit.Net-Webhook
Asp.NET Core 2.1 Webhook for Fitbit subscription call back end points dealing with verify requests and signature verification.

## Disclaimer
This is pre-release software based on pre-release software use at your own risk.

## Installation

First you will need to install the package via NuGet:

```
Install-Package FitbitNet.AspNetCore.WebHooks.Receivers
```

This currently only works, due to the preview nature of 2.1, if you start with a new ASP.NET Core 2 web application and then install the package. This will update the underlying mvc packages to the 2.1 build the webhooks works with. This will only been required while the 2.1 packages are in flux.

## Getting started

Once the nuget package has been installed you will need to configure the Fitbit.Net webhook receiver by using the `IServiceCollection` extension method in `ConfigureServices` in your `Startup` class.

If you are using the `AddMvcCore` extension method then you can register it as below:
```
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvcCore()
        .AddFitbitWebHooks();
}
```

Else if you are using the more full `AddMvc` extension method then you can register it as below:

```
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvc()
        .AddFitbitWebHooks();
}
```
## Setting up your webhook

You will need to add a controller to handle the request when it comes in. You can setup a generic catch all end point:

```
public class FitbitController : ControllerBase
{
    [FitbitWebHook]
    public IAsyncResult FitbitSubscription(string id, Notification[] data)
    {
        // add code here
    }
}
```
Or if you want to have an end point to handle specific subscription id then you can specify it as below replacing "my_id_here" with your specific route id:
```
public class FitbitController : ControllerBase
{
    [FitbitWebHook(Id="my_id_here")]
    public IAsyncResult FitbitSubscriptionId(Notification[] data)
    {
        // add code here
    }
}
```
Note: The id you specify above is the id which will match the route of the subscription end point, NOT the Fitbit Subscriber ID which you can set in the dev dashboard.

## appsettings.json
You will need to specify the the secret key for the specific end point. This is the value the end point requires to verify the end point. This can be found when you setup your end point in the Fitbit Dev dashboard.

You will also require the `Client Secret` of the application which you've setup in the Fitbit Dev Dashboard. This is used for the payload signature validation.

An example appsettings.json:
```
{
  "WebHooks": {
    "fitbit": {
      "SecretKey": {
        "my_id": "** verify key value here "
      },
      "OAuthClientSecret": "** OAuth Client Secret here **"
    }
  }
}
```
## Running the WebHook
Once the above is done and the corresponding values have been configured in the Fitbit developer dashboard then it should be ready to go.

Please note the subscription end points do not get activated until the end point has been been verified. This is done through using the webhook secret defined above.

If running the webhook locally you can use `ngrok` to work with your development environment. More details can be found https://adamstorr.azurewebsites.net/blog/aspnetcore-webhooks-running-the-github-webhook with the Github example.

## Extensions
### Signature Checking

You can disable the signature verification through setting an option in the `ConfigureServices` setup call and marking `EnableSignatureChecking` in the `FitbitWebhookReceiverOptions` options class as `false`.

```
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvcCore()
        .AddFitbitWebHooks(options =>
        {
            options.EnableSignatureChecking = false;
        });
}
```
Note: the same can be done for the `AddMvc` version as well.