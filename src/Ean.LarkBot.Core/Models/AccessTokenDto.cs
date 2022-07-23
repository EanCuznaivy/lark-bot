using System.Text.Json.Serialization;

namespace Ean.LarkBot.Core.Models;

public class AccessTokenDto
{
    [JsonPropertyName("app_access_token")] public string AppAccessToken { get; set; }
    [JsonPropertyName("code")] public int Code { get; set; }
    [JsonPropertyName("expire")] public int Expire { get; set; }
    [JsonPropertyName("msg")] public string Msg { get; set; }

    [JsonPropertyName("tenant_access_token")]
    public string TenantAccessToken { get; set; }
}