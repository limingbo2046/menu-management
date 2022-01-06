using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace lcn.menu_management.EntityFrameworkCore
{
    public class menu_managementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public menu_managementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}