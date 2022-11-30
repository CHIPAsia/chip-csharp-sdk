using System;
using Newtonsoft.Json;

namespace Examples.Models;

public class PaymentWH
{
    [JsonProperty("id")]
    public string? ID { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("event_type")]
    public string? EventType { get; set; }
}

