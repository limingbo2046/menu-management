using Volo.Abp.Modularity;

namespace lcn.menu_management
{
    [DependsOn(
        typeof(menu_managementApplicationModule),
        typeof(menu_managementDomainTestModule)
        )]
    public class menu_managementApplicationTestModule : AbpModule
    {

    }
}
