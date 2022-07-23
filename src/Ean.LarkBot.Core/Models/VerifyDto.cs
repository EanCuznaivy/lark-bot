using System.Text.Json.Serialization;

namespace Ean.LarkBot.Core.Models;

public class VerifyDto
{
    [JsonPropertyName("challenge")] public string Challenge { get; set; }
    [JsonPropertyName("token")] public string Token { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
}