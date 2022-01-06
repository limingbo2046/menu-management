using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace lcn.menu_management.EntityFrameworkCore
{
    public static class menu_managementDbContextModelCreatingExtensions
    {
        public static void Configuremenu_management(
            this ModelBuilder builder,
            Action<menu_managementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new menu_managementModelBuilderConfigurationOptions(
                menu_managementDbProperties.DbTablePrefix,
                menu_managementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
            builder.Entity<MenuItem>(b =>
            {

                b.ToTable(menu_managementDbProperties.DbTablePrefix + "MenuItems", menu_managementDbProperties.DbSchema);
                b.Property(m => m.DisplayName).IsRequired().HasMaxLength(500);
            });

            builder.Entity<MenuGroup>(b =>
            {
                b.ToTable(menu_managementDbProperties.DbTablePrefix + "MenuGroups", menu_managementDbProperties.DbSchema);
                b.Property(m => m.Name).IsRequired().HasMaxLength(256);
            });

            builder.Entity<MenuGrant>(b =>
            {
                b.ToTable(menu_managementDbProperties.DbTablePrefix + "MenuGrants", menu_managementDbProperties.DbSchema);
                b.Property(p => p.OwnerId).IsRequired();
            });


            builder.Entity<UserMenuGroups>(b => {
                b.ToTable(menu_managementDbProperties.DbTablePrefix + "UserMenuGroups", menu_managementDbProperties.DbSchema);
                b.Property(p => p.UserId).IsRequired();
                b.Property(p => p.MenuGroupId).IsRequired();
            });


        }
    }
}