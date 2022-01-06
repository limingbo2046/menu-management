using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace lcn.menu_management.EntityFrameworkCore
{
    [ConnectionStringName(menu_managementDbProperties.ConnectionStringName)]
    public class menu_managementDbContext : AbpDbContext<menu_managementDbContext>, Imenu_managementDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuGrant> MenuGrants { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<UserMenuGroups> UserMenuGroups { get; set; }
        public menu_managementDbContext(DbContextOptions<menu_managementDbContext> options)
            : base(options)
        {

        }

    
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configuremenu_management();
        }
    }
}