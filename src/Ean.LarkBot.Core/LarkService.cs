using System.Text.Json;
using Ean.LarkBot.Core.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using Volo.Abp.DependencyInjection;

namespace Ean.LarkBot.Core;

public class LarkService : ILarkService, ITransientDependency
{
    private readonly IEnumerable<IEventProcessor> _eventProcessors;
    private readonly UnixTimestampProvider _unixTimestampProvider;
    private readonly LarkBotOptions _larkBotOptions;
    private readonly ILarkUrlProvider _larkUrlProvider;

    public LarkService(ILarkUrlProvider larkUrlProvider, IEnumerable<IEventProcessor> eventProcessors,
        UnixTimestampProvider unixTimestampProvider,
        IOptionsSnapshot<LarkBotOptions> larkBotOptions)
    {
        _larkUrlProvider = larkUrlProvider;
        _eventProcessors = eventProcessors;
        _unixTimestampProvider = unixTimestampProvider;
        _larkBotOptions = larkBotOptions.Value;
    }

    public async Task ProcessEvent(EventDto eventDto)
    {
        var eventCreateTime = long.Parse(eventDto.Header.CreateTime);
        if (eventCreateTime <= _unixTimestampProvider.GetUnixTimestamp())
        {
            return;
        }

        var accessToken = await GetAccessTokenAsync();
        foreach (var eventProcessor in _eventProcessors)
        {
            if (eventProcessor.EventType != eventDto.Header.EventType) continue;
            await eventProcessor.ProcessAsync(accessToken, eventDto);
            _unixTimestampProvider.SetUnixTimestamp(eventCreateTime);
            break;
        }
    }

    private async Task<string> GetAccessTokenAsync()
    {
        var client = new RestClient(_larkUrlProvider.GetAccessTokenUrl);
        var request = new RestRequest();
        request.AddParameter("app_id", _larkBotOptions.AppId);
        request.AddParameter("app_secret", _larkBotOptions.AppSecret);
        var response = await client.ExecuteAsync(request);
        return JsonSerializer.Deserialize<AccessTokenDto>(response.Content!)!.AppAccessToken;
    }
}