using System.Text.Json.Serialization;

namespace Ean.LarkBot.QingYunKe.Modules;

public class TextDto
{
    [JsonPropertyName("text")] public string Text { get; set; }
}