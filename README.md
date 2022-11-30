# Chip C# library

<a name="frameworks-supported"></a>
## Frameworks supported
- .NET Core >=2.1
- .NET Framework >=4.6
- Mono/Xamarin >=vNext

<a name="dependencies"></a>
## Dependencies

- [RestSharp](https://www.nuget.org/packages/RestSharp) - 106.11.4 or later
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 12.0.3 or later
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/) - 1.7.0 or later
- [System.ComponentModel.Annotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) - 4.7.0 or later
- [BouncyCastle](https://www.nuget.org/packages/BouncyCastle) - 1.8.1 or later

The DLLs included in the package may not be the latest version. We recommend using [NuGet](https://docs.nuget.org/consume/installing-nuget) to obtain the latest version of the packages:
```
Install-Package RestSharp
Install-Package Newtonsoft.Json
Install-Package JsonSubTypes
Install-Package System.ComponentModel.Annotations
Install-Package BouncyCastle
```

NOTE: RestSharp versions greater than 105.1.0 have a bug which causes file uploads to fail. See [RestSharp#742](https://github.com/restsharp/RestSharp/issues/742)

<a name="installation"></a>
## Installation

### Download generated binary
1. Download `DLL`
2. Include the DLL (under the `bin` folder) in the C# project.

### Generate `DLL`
1. Generate the DLL using your preferred tool (e.g. `dotnet build`)
2. Include the DLL (under the `bin` folder) in the C# project

### Include on project
1. Clone repo
2. Include project to `.sln`
3. Add as `Project References` on your project dependencies


<a name="usage"></a>
## Usage

<a name="getting-started"></a>
## Getting Started

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.Chip.Api;
using Org.Chip.Client;
using Org.Chip.Model;

namespace Example
{
    public class Example
    {
        public static void Main()
        {

            Configuration config = new Configuration();
            config.BasePath = "https://gate.chip-in.asia/api/v1/";
            config.AccessToken = "Ys1hUYPruZyMQc_eXrSrMmRFxJpbccPvOiV0Oglk2VOM3eosCkZvHh6IIJRzyaII0PL2jp6ZbzFkxaSastfUpg==";
            var apiInstance = new PurchasesApi(config);
            var client = new ClientDetails("asd@asd.com");
            var products = new List<Product>();
            products.Add(new Product("Test", "1", 100));
            var details = new PurchaseDetails(products: products);
            var data = new Purchase(client, details, brandId: new System.Guid("a0da394b-0e3c-4002-956f-e02b47341db6"));
            try
            {
                Purchase result = apiInstance.PurchasesCreate(data);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling PurchasesApi.PurchasesCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }

        }
    }
}
```
