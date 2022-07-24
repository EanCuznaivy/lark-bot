using Ean.LarkBot.Core.Models;
using Volo.Abp.DependencyInjection;

namespace Ean.LarkBot.Core.Processors;

/// <summary>
/// Ref: https://open.feishu.cn/document/uAjLw4CM/ukTMukTMukTM/reference/im-v1/message/events/receive
/// </summary>
public class MessageReceiveV1Processor : BaseEventProcessor
{
    public override string EventType => "im.message.receive_v1";
    private IEnumerable<IReplyContentProvider> _replyContentProviders;

    public MessageReceiveV1Processor(IEnumerable<IReplyContentProvider> replyContentProviders)
    {
        _replyContentProviders = replyContentProviders;
    }

    internal override string GetPostUrl(EventDto eventDto)
    {
        return $"{LarkBotConstants.Host}{LarkBotConstants.MessagesUrl}/{eventDto.Event.Message.MessageId}/reply";
    }

    internal override async Task<object?> GetJsonBodyAsync(EventDto eventDto)
    {
        var text = eventDto.Event.Message.Content;

        foreach (var replyContentProvider in _replyContentProviders)
        {
            if (replyContentProvider.KeyWords.Any(keyWord => text.Contains(keyWord)))
            {
                var json = await replyContentProvider.GetTextAsync(eventDto);
                if (json == string.Empty)
                {
                    return null;
                }

                text = json;
                break;
            }
        }

        return new
        {
            content = text,
            msg_type = "text"
        };
    }
}