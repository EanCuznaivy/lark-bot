using Ean.LarkBot.Core.Models;

namespace Ean.LarkBot.Core;

public interface IReplyContentProvider
{
    List<string> KeyWords { get; }
    Task<string> GetTextAsync(EventDto eventDto);
}