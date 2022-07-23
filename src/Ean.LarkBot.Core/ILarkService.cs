using Ean.LarkBot.Core.Models;

namespace Ean.LarkBot.Core;

public interface ILarkService
{
    Task ProcessEvent(EventDto eventDto);
}