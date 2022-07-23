using System.Text.Json.Serialization;

namespace Ean.LarkBot.Core.Models;

public class TextDto
{
    [JsonPropertyName("text")] public string Text { get; set; }
}