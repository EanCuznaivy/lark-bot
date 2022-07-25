using Ean.LarkBot.Core;
using Ean.LarkBot.Core.Models;

namespace Ean.LarkBot.WebApi.ContentProviders;

public class SwapContentProvider : IReplyContentProvider
{
    public List<string> KeyWords => new() { "换币", "swap" };

    public Task<string> GetTextAsync(EventDto eventDto)
    {
        throw new NotImplementedException();
    }
}