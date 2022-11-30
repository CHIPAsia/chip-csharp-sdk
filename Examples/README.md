<div align="center">
    <a href="https://code.visualstudio.com/docs/languages/csharp">
        <img
            alt="C#"
            src="https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/C_Sharp_wordmark.svg/2048px-C_Sharp_wordmark.svg.png"
            width="150">
    </a>
</div>

---

## Requirements
* .NET Core >=2.1
* .NET Framework >=4.6

## Prerequisite
You will need to replace the value on file [Config.cs](./Utils/Config.cs) with the configuration on your Developer section by logging-in to Merchant Portal with your account.

```javascript
// Utils/Config.cs
namespace Examples.Utils;

public struct Config
{
    public static string BrandId = "<<BRAND_ID>>";
    public static string ApiKey = "<<API_KEY>>";
    public static string Endpoint = "https://gate.chip-in.asia/api/v1";
    public static string BasedUrl = "<<DOMAIN_URL>>";
    public static string WebhookPublicKey = "<<WEBHOOK_PUBLIC_KEY>>";
}
```

**BRAND_ID**

Obtain your BRAND_ID from Developer section.

---
**API_KEY**

Obtain your API_KEY from Developer section.

---

**WEBHOOK_PUBLIC_KEY**

Obtain your `WEBHOOK_PUBLIC_KEY` from Developer section. You can register the URL from [API](https://developer.chip-in.asia/api) or from Merchant Portal on Developer section.

---

**DOMAIN_URL**

It is your domain URL

## Run Example
1. Install dependencies: (If the dependencies are not installed)
```bash
Install-Package RestSharp
Install-Package Newtonsoft.Json
Install-Package JsonSubTypes
Install-Package System.ComponentModel.Annotations
Install-Package BouncyCastle
```

Or you can open `NuGet Packages Manager` to install the dependencies. Make sure the `RestSharp` installed version is `106.11.x`

and visit on your browser:
 1. Run on HTTPS: https://localhost:7297/
 2. Run on HTTP: http://localhost:5222/

To test `/api/payments/paymentcallback` & `/api/webhook/paymentwebhook` to be called from our server, make sure it is connected to internet (exposed to outside). 

We recommend to use [ngrok](https://ngrok.com/) if you want to run locally for debugging. Then You can replace `DOMAIN_URL` with generated URL by `ngrok`. 

`NB: Use it at your own risk. Make sure do not expose your critical port.`
