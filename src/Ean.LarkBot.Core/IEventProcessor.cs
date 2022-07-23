using Ean.LarkBot.Core.Models;

namespace Ean.LarkBot.Core;

public interface IEventProcessor
{
    string EventType { get; }
    Task ProcessAsync(string accessToken, EventDto eventDto);
}