using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace lcn.menu_management.EntityFrameworkCore
{
    [ConnectionStringName(menu_managementDbProperties.ConnectionStringName)]
    public interface Imenu_managementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuGrant> MenuGrants { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }

        public DbSet<UserMenuGroups> UserMenuGroups { get; set; }
    }
}