using System.Text.Json.Serialization;

namespace Ean.LarkBot.Core.Models;

public class EncryptedBody
{
    [JsonPropertyName("encrypt")] public string Encrypt { get; set; }
}