using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Org.Chip.Api;
using Org.Chip.Client;
using Org.Chip.Model;
using Examples.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Examples.Controllers;

[Route("api/[controller]")]
public class PaymentMethodsController : Controller
{
    private readonly Configuration config;
    private readonly PaymentMethodsApi apiInstance;

    public PaymentMethodsController()
    {
        config = new Configuration
        {
            BasePath = Config.Endpoint,
            AccessToken = Config.ApiKey,
        };
        apiInstance = new PaymentMethodsApi(config);
    }

    // GET: api/values
    [HttpGet]
    public IActionResult Get()
    {
        var brandId = Config.BrandId;
        var currency = "MYR";

        try
        {
            PaymentMethods result = apiInstance.PaymentMethods(brandId, currency);
            return Ok(result);
        }
        catch (ApiException e)
        {
            Debug.Print("Exception when calling PaymentMethodsApi.PaymentMethods: " + e.Message);
            Debug.Print("Status Code: " + e.ErrorCode);
            Debug.Print(e.StackTrace);
        }

        return NotFound();
    }
}
