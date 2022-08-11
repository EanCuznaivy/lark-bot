using System.Text;
using System.Text.Json;
using Ean.LarkBot.Core.Models;
using Ean.LarkBot.QingYunKe;
using Volo.Abp.DependencyInjection;

namespace Ean.LarkBot.Core.Processors;

/// <summary>
/// Ref: https://open.feishu.cn/document/uAjLw4CM/ukTMukTMukTM/reference/im-v1/message/events/receive
/// </summary>
public class MessageReceiveV1Processor : BaseEventProcessor
{
    public override string EventType => "im.message.receive_v1";
    private readonly IEnumerable<IReplyContentProvider> _replyContentProviders;
    private readonly IQingYunKeService _chatService;

    public MessageReceiveV1Processor(IEnumerable<IReplyContentProvider> replyContentProviders,
        IQingYunKeService chatService)
    {
        _replyContentProviders = replyContentProviders;
        _chatService = chatService;
    }

    internal override string GetPostUrl(EventDto eventDto)
    {
        return $"{LarkBotConstants.Host}{LarkBotConstants.MessagesUrl}/{eventDto.Event.Message.MessageId}/reply";
    }

    internal override async Task<object?> GetJsonBodyAsync(EventDto eventDto)
    {
        //var text = await _chatService.ChatAsync(JsonSerializer.Deserialize<TextDto>(eventDto.Event.Message.Content)!
        //.Text);
        var text = eventDto.Event.Message.Content;

        if (eventDto.Event.Message.Content.Contains("@"))
        {
            return new
            {
                content = Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(new TextDto
                {
                    Text = "弄啥？"
                })),
                msg_type = "text"
            };
        }

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