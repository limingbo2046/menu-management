using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace lcn.menu_management
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(menu_managementDomainSharedModule)
    )]
    public class menu_managementDomainModule : AbpModule
    {

    }
}
