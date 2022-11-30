using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Org.Chip.Api;
using Org.Chip.Client;
using Org.Chip.Model;
using Examples.Utils;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Examples.Controllers;

[Route("api/[controller]/[action]")]
public class PaymentsController : ControllerBase
{
    private readonly Configuration config;
    private readonly PurchasesApi apiInstance;
    private readonly HttpClient client;

    public PaymentsController()
    {
        config = new Configuration
        {
            BasePath = Config.Endpoint,
            AccessToken = Config.ApiKey,
        };

        apiInstance = new PurchasesApi(config);

        client = new HttpClient
        {
            BaseAddress = new Uri(Config.Endpoint)
        };
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Config.ApiKey}");
    }

    [HttpGet(Name = "CreatePurchase")]
    public IActionResult CreatePurchase()
    {
        var client = new ClientDetails("test@test.com");
        var product = new Product("Test", "1", 100);
        var purchaseDetails = new PurchaseDetails(products: new List<Product>() { product, });
        var purchase = new Purchase(
            client,
            purchaseDetails,
            brandId: new System.Guid(Config.BrandId),
            successCallback: $"{Config.BasedUrl}/api/payments/paymentcallback",
            successRedirect: $"{Config.BasedUrl}/redirect?success=1",
            failureRedirect: $"{Config.BasedUrl}/redirect?success=0"
            );

        try
        {
            Purchase result = apiInstance.PurchasesCreate(purchase);
            Debug.WriteLine(result);
            return Redirect(result.CheckoutUrl);
        }
        catch (ApiException e)
        {
            Debug.Print("Exception when calling PurchasesApi.PurchasesCreate: " + e.Message);
            Debug.Print("Status Code: " + e.ErrorCode);
            Debug.Print(e.StackTrace);
        }

        return BadRequest();
    }

    [HttpPost(Name = "PaymentCallback")]
    public async Task<IActionResult> PaymentCallback([FromBody] JsonElement value)
    {
        var data = value.GetRawText();
        string xsignature = Request.Headers["x-signature"][0] ?? "";

        var res = await client.GetAsync("v1/public_key/");
        var publicKey = await res.Content.ReadFromJsonAsync<string>() ?? "";

        try
        {
            bool result = apiInstance.VerifyData(data, xsignature, publicKey);
            Debug.WriteLine($"/api/payments/paymentcallback VERIFIED: {result}");
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
