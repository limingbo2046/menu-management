using lcn.menu_management.Localization;
using Volo.Abp.Application.Services;

namespace lcn.menu_management
{
    public abstract class menu_managementAppService : ApplicationService
    {
        protected menu_managementAppService()
        {
            LocalizationResource = typeof(menu_managementResource);
            ObjectMapperContext = typeof(menu_managementApplicationModule);
        }
    }
}
