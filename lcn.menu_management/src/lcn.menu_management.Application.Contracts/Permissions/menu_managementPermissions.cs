using Volo.Abp.Reflection;

namespace lcn.menu_management.Permissions
{
    public class menu_managementPermissions
    {
        public const string GroupName = "menu_management";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(menu_managementPermissions));
        }
    }
}