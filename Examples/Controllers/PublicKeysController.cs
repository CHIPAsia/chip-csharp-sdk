using Microsoft.AspNetCore.Mvc;
using Examples.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Examples.Controllers;

[Route("api/[controller]")]
public class PublicKeysController : Controller
{
    private readonly HttpClient client;

    public PublicKeysController()
    {
        client = new HttpClient
        {
            BaseAddress = new Uri(Config.Endpoint)
        };
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Config.ApiKey}");
    }

    // GET: api/values
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await client.GetAsync("v1/public_key/");
        var contents = await res.Content.ReadFromJsonAsync<string>();

        return Ok(contents);
    }
}

