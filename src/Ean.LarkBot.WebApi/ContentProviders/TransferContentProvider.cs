using System.Text;
using System.Text.Json;
using Ean.LarkBot.Core;
using Ean.LarkBot.Core.Models;

namespace Ean.LarkBot.WebApi.ContentProviders;

public class TransferContentProvider : IReplyContentProvider
{
    public List<string> KeyWords => new() { "ELF_" };

    public string GetText(EventDto eventDto)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(new TextDto { Text = "暂不支持转账哦" });
        return Encoding.UTF8.GetString(bytes);
    }
}