using Ean.LarkBot.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RestSharp;

namespace Ean.LarkBot.Core.Processors;

public abstract class BaseEventProcessor : IEventProcessor
{
    public abstract string EventType { get; }

    public ILogger<BaseEventProcessor> Logger { get; set; }

    public BaseEventProcessor()
    {
        Logger = NullLogger<BaseEventProcessor>.Instance;
    }

    public async Task ProcessAsync(string accessToken, EventDto eventDto)
    {
        var url = GetPostUrl(eventDto);
        var client = new RestClient(url);
        var request = new RestRequest();
        request.AddHeader("Authorization", $"Bearer {accessToken}");
        request.AddHeader("Content-Type", "application/json");
        var jsonBody = await GetJsonBodyAsync(eventDto);
        if (jsonBody == null)
        {
            return;
        }
        request.AddJsonBody(jsonBody);
        var response = await client.PostAsync(request);
        Logger.LogInformation(response.Content);
    }

    internal abstract string GetPostUrl(EventDto eventDto);
    internal abstract Task<object?> GetJsonBodyAsync(EventDto eventDto);
}