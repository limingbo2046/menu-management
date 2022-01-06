using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace lcn.menu_management
{
    [DependsOn(
        typeof(menu_managementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class menu_managementApplicationContractsModule : AbpModule
    {

    }
}
