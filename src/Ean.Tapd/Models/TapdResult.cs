using System.Text.Json.Serialization;

namespace Ean.Tapd.Models;

public class TapdResult
{
    [JsonPropertyName("status")] public int Status { get; set; }

    [JsonPropertyName("info")] public string Info { get; set; }

    [JsonPropertyName("data")] public string Data { get; set; }
}