using Volo.Abp.DependencyInjection;

namespace Ean.LarkBot.Core;

public class UnixTimestampProvider : ISingletonDependency
{
    private long _unixTimestamp;

    public UnixTimestampProvider()
    {
        _unixTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    public long GetUnixTimestamp()
    {
        return _unixTimestamp;
    }

    public void SetUnixTimestamp(long unixTimestamp)
    {
        _unixTimestamp = unixTimestamp;
    }
}