using Ean.LarkBot.Core.Processors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Ean.LarkBot.Core;

[DependsOn(
    typeof(AbpAutofacModule)
)]
public class LarkBotCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<LarkBotOptions>(options => { configuration.GetSection("LarkBot").Bind(options); });

        context.Services.AddTransient<IEventProcessor, MessageReceiveV1Processor>();
    }
}