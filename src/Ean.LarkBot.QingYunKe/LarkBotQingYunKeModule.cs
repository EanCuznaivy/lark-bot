using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Ean.LarkBot.QingYunKe;

[DependsOn(
    typeof(AbpAutofacModule)
)]
public class LarkBotQingYunKeModule : AbpModule
{
    
}