using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;

namespace lcn.menu_management
{
    public class MenuItemManager : DomainService
    {
        protected IRepository<MenuGroup, Guid> _MenuGroupsRepo;
        protected IRepository<MenuItem, Guid> _MenuItemsRepo;
        protected IRepository<MenuGrant, Guid> _MenuGrantsRepo;
        public MenuItemManager(
            IRepository<MenuGroup, Guid> menuGroups,
            IRepository<MenuItem, Guid> menuItems,
            IRepository<MenuGrant, Guid> menuGrants
            )
        {
            _MenuGroupsRepo = menuGroups;
            _MenuItemsRepo = menuItems;
            _MenuGrantsRepo = menuGrants;
        }
        public virtual async Task AddMenuItemToGroupAsync(Guid groupId, Guid menuItemId)
        {
            await AddMenuItemToGroupAsync(
                await _MenuGroupsRepo.GetAsync(groupId),
                await _MenuItemsRepo.GetAsync(menuItemId));
        }
        public virtual async Task AddMenuItemToGroupAsync(MenuGroup group, MenuItem menuItem)
        {

            if (_MenuGrantsRepo.Any(p => p.OwnerId == group.Id && p.MenuId == menuItem.Id))
            {
                return;
            }
            await _MenuGrantsRepo.InsertAsync(new MenuGrant(GuidGenerator.Create(), group.Id, menuItem.Id, "G"));
        }
    }
}
