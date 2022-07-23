using Ean.LarkBot.Core.Models;
using Volo.Abp.DependencyInjection;

namespace Ean.LarkBot.Core.Processors;

/// <summary>
/// Ref: https://open.feishu.cn/document/uAjLw4CM/ukTMukTMukTM/reference/im-v1/message/events/receive
/// </summary>
public class MessageReceiveV1Processor : BaseEventProcessor, ITransientDependency
{
    public override string EventType => "im.message.receive_v1";
    

    internal override string GetPostUrl(EventDto eventDto)
    {
        return $"{LarkBotConstants.Host}{LarkBotConstants.MessagesUrl}/{eventDto.Event.Message.MessageId}/reply";
    }

    internal override object GetJsonBody(EventDto eventDto)
    {
        return new
        {
            content = eventDto.Event.Message.Content,
            msg_type = "text"
        };
    }
}