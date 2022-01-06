using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace lcn.menu_management
{
    [DependsOn(
        typeof(menu_managementHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class menu_managementConsoleApiClientModule : AbpModule
    {
        
    }
}
