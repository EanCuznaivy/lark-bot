using Ean.LarkBot.Core;
using Microsoft.OpenApi.Models;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace Ean.LarkBot.WebApi;

[DependsOn(
    typeof(AbpAspNetCoreModule),
    typeof(LarkBotCoreModule)
)]
public class LarkBotWebApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        services.AddControllers();

        services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Lark Bot API", Version = "v1"});
                options.OperationFilter<SwaggerFileUploadFilter>();
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }
}