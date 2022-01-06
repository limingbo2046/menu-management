using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace lcn.menu_management
{
    [DependsOn(
        typeof(menu_managementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class menu_managementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "menu_management";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(menu_managementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
