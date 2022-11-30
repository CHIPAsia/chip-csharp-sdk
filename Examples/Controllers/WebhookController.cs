using System.Diagnostics;
using System.Text.Json;
using Examples.Models;
using Examples.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.Chip.Api;
using Org.Chip.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Examples.Controllers;

[Route("api/[controller]/[action]")]
public class WebhookController : Controller
{
    private readonly Configuration config;
    private readonly PurchasesApi apiInstance;

    public WebhookController()
    {
        config = new Configuration
        {
            BasePath = Config.Endpoint,
            AccessToken = Config.ApiKey,
        };

        apiInstance = new PurchasesApi(config);
    }

    [HttpPost(Name = "PaymentWebhook")]
    public IActionResult PaymentWebhook([FromBody] JsonElement value)
    {
        var data = value.GetRawText();
        var json = JsonConvert.DeserializeObject<PaymentWH>(data);
        string xsignature = Request.Headers["x-signature"][0] ?? "";

        var publicKey = Config.WebhookPublicKey;

        try
        {
            bool result = apiInstance.VerifyData(data, xsignature, publicKey);
            Debug.WriteLine($"/api/webhook/paymentwebhook EVENT: {json.EventType}");
            Debug.WriteLine($"/api/webhook/paymentwebhook VERIFIED: {result}");
        }
        catch (ApiException e)
        {
            Debug.Print("Exception when calling PurchasesApi.VerifyData: " + e.Message);
            Debug.Print("Status Code: " + e.ErrorCode);
            Debug.Print(e.StackTrace);
        }
        return Ok();
    }
}

