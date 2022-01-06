using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace lcn.menu_management.EntityFrameworkCore
{
    public class menu_managementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<menu_managementHttpApiHostMigrationsDbContext>
    {
        public menu_managementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<menu_managementHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("menu_management"));

            return new menu_managementHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
