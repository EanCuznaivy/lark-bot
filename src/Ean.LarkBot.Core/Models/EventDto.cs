using System.Text.Json.Serialization;

namespace Ean.LarkBot.Core.Models;


public class EventDto
{
    [JsonPropertyName("schema")] public string Schema { get; set; }
    [JsonPropertyName("header")] public EventHeader Header { get; set; }
    [JsonPropertyName("event")] public Event Event { get; set; }
}

public class EventHeader
{
    [JsonPropertyName("event_id")] public string EventId { get; set; }
    [JsonPropertyName("token")] public string Token { get; set; }
    [JsonPropertyName("create_time")] public string CreateTime { get; set; }
    [JsonPropertyName("event_type")] public string EventType { get; set; }
    [JsonPropertyName("tenant_key")] public string TenantKey { get; set; }
    [JsonPropertyName("app_id")] public string AppId { get; set; }
}

public class Event
{
    [JsonPropertyName("message")] public Message Message { get; set; }
    [JsonPropertyName("sender")] public Sender Sender { get; set; }
}

public class Message
{
    [JsonPropertyName("chat_id")] public string ChatId { get; set; }
    [JsonPropertyName("chat_type")] public string ChatType { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
    [JsonPropertyName("create_time")] public string CreateTime { get; set; }
    [JsonPropertyName("mentions")] public List<Mention> Mentions { get; set; }
    [JsonPropertyName("message_id")] public string MessageId { get; set; }
    [JsonPropertyName("message_type")] public string MessageType { get; set; }
}

public class Sender
{
    [JsonPropertyName("sender_id")] public SenderId SenderId { get; set; }
    [JsonPropertyName("sender_type")] public string SenderType { get; set; }
    [JsonPropertyName("tenant_key")] public string TenantKey { get; set; }
}

public class Mention
{
    [JsonPropertyName("id")] public Dictionary<string, string> Id { get; set; }
    [JsonPropertyName("key")] public string Key { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("tenant_key")] public string TenantKey { get; set; }
}

public class SenderId
{
    [JsonPropertyName("open_id")] public string OpenId { get; set; }
    [JsonPropertyName("union_id")] public string UnionId { get; set; }
    [JsonPropertyName("user_id")] public string UserId { get; set; }
}