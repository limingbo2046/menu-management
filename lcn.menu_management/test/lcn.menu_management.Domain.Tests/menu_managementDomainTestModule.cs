using lcn.menu_management.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace lcn.menu_management
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(menu_managementEntityFrameworkCoreTestModule)
        )]
    public class menu_managementDomainTestModule : AbpModule
    {
        
    }
}
