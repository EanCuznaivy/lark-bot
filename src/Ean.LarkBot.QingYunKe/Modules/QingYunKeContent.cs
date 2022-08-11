using System.Text.Json.Serialization;

namespace Ean.LarkBot.QingYunKe.Modules;

public class QingYunKeContent
{
    [JsonPropertyName("result")] public int Result { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
}