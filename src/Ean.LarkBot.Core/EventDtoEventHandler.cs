using Ean.LarkBot.Core.Models;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Ean.LarkBot.Core;

public class EventDtoEventHandler : ILocalEventHandler<EventDto>, ITransientDependency
{
    private readonly ILarkService _larkService;

    public EventDtoEventHandler(ILarkService larkService)
    {
        _larkService = larkService;
    }

    public async Task HandleEventAsync(EventDto eventData)
    {
        await _larkService.ProcessEvent(eventData);
    }
}