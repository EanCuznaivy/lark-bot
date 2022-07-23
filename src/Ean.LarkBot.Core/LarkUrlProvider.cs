using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Ean.LarkBot.Core;

public class LarkUrlProvider : ILarkUrlProvider, ITransientDependency
{
    private readonly string _appId;
    private readonly string _appSecret;
    public string GetAccessTokenUrl => $"{LarkBotConstants.Host}{LarkBotConstants.GetAccessTokenUrl}";

    public LarkUrlProvider(IOptionsSnapshot<LarkBotOptions> larkBotOptions)
    {
        _appId = larkBotOptions.Value.AppId;
        _appSecret = larkBotOptions.Value.AppSecret;
    }
}