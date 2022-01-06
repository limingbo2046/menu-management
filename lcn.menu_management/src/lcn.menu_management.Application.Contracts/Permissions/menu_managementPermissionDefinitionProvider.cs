using lcn.menu_management.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace lcn.menu_management.Permissions
{
    public class menu_managementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(menu_managementPermissions.GroupName, L("Permission:menu_management"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<menu_managementResource>(name);
        }
    }
}