using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace lcn.menu_management.EntityFrameworkCore
{
    public class menu_managementHttpApiHostMigrationsDbContext : AbpDbContext<menu_managementHttpApiHostMigrationsDbContext>
    {
        public menu_managementHttpApiHostMigrationsDbContext(DbContextOptions<menu_managementHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configuremenu_management();
        }
    }
}
