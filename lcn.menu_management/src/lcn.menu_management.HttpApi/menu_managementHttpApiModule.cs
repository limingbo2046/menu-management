using Localization.Resources.AbpUi;
using lcn.menu_management.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace lcn.menu_management
{
    [DependsOn(
        typeof(menu_managementApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class menu_managementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(menu_managementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<menu_managementResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
