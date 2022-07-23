using Ean.LarkBot.Core.Models;

namespace Ean.LarkBot.Core;

public interface IReplyContentProvider
{
    List<string> KeyWords { get; }
    string GetText(EventDto eventDto);
}