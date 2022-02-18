using Microsoft.Extensions.Logging;//添加该命名空间就可以使用日志
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace lcn.menu_management
{
    public class MenuAppService : CrudAppService<MenuItem, MenuItemDto, Guid, MenuItemRequestDto, MenuItemCreateDto, MenuItemUpdateDto>, IMenuAppService
    {

        private readonly IRepository<MenuGroup> _menuGroupsRepo;//菜单组
        private readonly IRepository<MenuGrant> _menuGrantsRepo;//菜单范围
        private readonly IRepository<UserMenuGroups> _userMenuGroups;//用户和菜单组的关系


        public MenuAppService(
            IRepository<MenuItem, Guid> menusRepo,
            IRepository<MenuGroup> menuGroups,
            IRepository<MenuGrant> menuGrants,
            IRepository<UserMenuGroups> userMenuGroups
            ) : base(menusRepo)
        {
            _menuGroupsRepo = menuGroups;
            _menuGrantsRepo = menuGrants;
            _userMenuGroups = userMenuGroups;
        }

        public async override Task<PagedResultDto<MenuItemDto>> GetListAsync(MenuItemRequestDto input)
        {

            var listAll = await Repository.GetListAsync();
            var result = ObjectMapper.Map<List<MenuItem>, List<MenuItemDto>>(listAll);
            result = result.OrderBy(p => p.Order).ToList();
            List<MenuItemDto> resultItem = new();
            var parentItems = result.Where(p => !p.ParentMenuItem.HasValue);//根节点
            foreach (var item in parentItems)
            {
                resultItem.Add(item);
                await AddSubItemAsync(item, result);//把根节点的子节点填充
            }

            return new PagedResultDto<MenuItemDto>(resultItem.Count, resultItem);
        }

        #region 查询方法
        /// <summary>
        /// 获取当前用户的菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuDto>> Query(GetCurrentUserMenus query)
        {

            List<MenuDto> lstResult = new();

            var menuItems = await GetCurrentUserMenuItemAsync();

            if (menuItems.Count > 0)
            {
                menuItems = menuItems.OrderBy(p => p.Order).ToList();
                var rootMenus = menuItems.Where(p => p.ParentMenuItem == null).ToList();//根菜单

                foreach (var p_item in rootMenus)//遍历根菜单
                {
                    var p_menu = ObjectMapper.Map<MenuItem, MenuDto>(p_item);//父菜单
                    await WriteMenuSubItemAsync(p_menu, menuItems);
                    lstResult.Add(p_menu);//添加到列表
                }
            }
            return lstResult;
        }
        /// <summary>
        /// 获取当前用户的按钮列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuButtonDto>> Query(GetCurrentUserButtons query)
        {


            var menuItems = await GetCurrentUserMenuItemAsync(false);
            if (query.TreeNodeId.HasValue)
            {
                menuItems = menuItems.Where(p => p.ParentMenuItem == query.TreeNodeId).ToList();
            }

            return ObjectMapper.Map<List<MenuItem>, List<MenuButtonDto>>(menuItems);
        }

        /// <summary>
        /// 获取菜单的树形结构(包含授权)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<MenuTreeDto>> Query(GetMenuTree query)
        {

            CurrentTenant.Change(query.TenantId);
            //添加租户过滤
            var lstAll = await AsyncExecuter.ToListAsync(from m in Repository select m);
            var lstTree = ObjectMapper.Map<List<MenuItem>, List<MenuTreeDto>>(lstAll).ToList();
            lstTree = lstTree.OrderBy(p => p.Order).ToList();
            List<Guid> lstGrantMenuItemIds = new List<Guid>();
            bool isGroupGrant = false;
            var ownerGroup = await _menuGroupsRepo.FindAsync(p => p.Id == query.OwnerId);//菜单组
            if (ownerGroup != null)
            {
                isGroupGrant = true;//当拥有者是菜单分组的时候,只需要对一个分组进行操作
                lstGrantMenuItemIds = _menuGrantsRepo.Where(p => p.OwnerId == query.OwnerId).Select(p => p.MenuId).ToList();//菜单组拥有的菜单
                lstTree.Where(p => lstGrantMenuItemIds.Contains(p.Id)).ToList().ForEach((p) =>
                {
                    p.IsGrant = true;
                    p.IsGroupGrant = isGroupGrant;
                });
            }
            else
            {
                lstGrantMenuItemIds = _menuGrantsRepo.Where(p => p.OwnerId == query.OwnerId).Select(p => p.MenuId).ToList();//用户拥有的菜单
                lstTree.Where(p => lstGrantMenuItemIds.Contains(p.Id)).ToList().ForEach((p) =>
                {
                    p.IsGrant = true;
                    p.IsGroupGrant = isGroupGrant;
                });

                var userGroups = _userMenuGroups.Where(p => p.UserId == query.OwnerId).Select(p => p.MenuGroupId).ToList(); //用户所在的菜单组的菜单
                var lstGroupMenuItemIds = _menuGrantsRepo.Where(p => userGroups.Contains(p.OwnerId)).Select(p => p.MenuId).ToList();//菜单组的菜单
                lstTree.Where(p => lstGroupMenuItemIds.Contains(p.Id)).ToList().ForEach((p) =>
                {
                    p.IsGrant = true;
                    p.IsGroupGrant = true;
                });
            }

            List<MenuTreeDto> lstResult = new List<MenuTreeDto>();
            //组装成父子结构
            var parent = lstTree.Where(p => !p.ParentMenuItem.HasValue).ToList();
            foreach (var p in parent)
            {
                await WriteMenuChildrenAsync(p, lstTree);
                lstResult.Add(p);
            }
            return lstResult;
        }

        #endregion

        #region 命令

        public async Task GrantMenuAsync(GrantMenu input)
        {


            bool isGroupOwner = false;//拥有菜单是用户
            var groupOwner = await _menuGroupsRepo.FindAsync(p => p.Id == input.OwnerId);
            if (groupOwner != null)
            {
                isGroupOwner = true;//拥有者是菜单组
            }

            foreach (var p in input.MenuItems)//遍历菜单和按钮
            {
                List<MenuTreeDto> lstAll = new List<MenuTreeDto>();
                await MenuTreeDtos(p, lstAll);

                if (!isGroupOwner)
                {
                    lstAll = lstAll.Where(p1 => p1.IsGroupGrant == false).ToList();//过滤出用户菜单
                }

                foreach (var item in lstAll)
                {
                    var menuGrant = await _menuGrantsRepo.FindAsync(p => p.OwnerId == input.OwnerId && p.MenuId == item.Id);//通过菜单ID和拥有者ID查找关联关系
                    if (menuGrant == null)//如果关系不存在
                    {
                        if (item.IsGrant)//传递进来的是赋予的
                        {
                            await _menuGrantsRepo.InsertAsync(new MenuGrant(GuidGenerator.Create(), input.OwnerId, item.Id, input.Provider, CurrentTenant.Id));
                        }
                    }
                    else//如果关系存在
                    {
                        if (item.IsGrant)//传递进来的是赋予的
                        {
                            continue;
                        }
                        else
                        {
                            await _menuGrantsRepo.DeleteAsync(menuGrant);//解除关系
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 为菜单指定租户，所以必须有租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AssignedTenant2MenuItemAsync(AssignedTenant2MenuItem input)
        {
            var rootMenu = await Repository.FindAsync(p => p.Id == input.RootMenuItemId);
            await UpdateTenantId(rootMenu, input.TenantId, input.FromTenantId);
        }
        #endregion

        #region 私有方法
        private async Task UpdateTenantId(MenuItem menuItem, Guid tenantId, Guid? fromTenantId)
        {
            menuItem.TenantId = tenantId;//赋值租户
            CurrentTenant.Change(fromTenantId);
            var list = await AsyncExecuter.ToListAsync(
                from m in Repository
                where m.ParentMenuItem == menuItem.Id
                select m
                );

            foreach (var item in list)
            {
                await UpdateTenantId(item, tenantId, fromTenantId);
            }

        }
        /// <summary>
        /// 属性结构的菜单组成列表
        /// </summary>
        /// <param name="treeDto">树形结构</param>
        /// <param name="lstMenuTree">列表</param>
        /// <returns></returns>
        private async Task MenuTreeDtos(MenuTreeDto treeDto, List<MenuTreeDto> lstMenuTree)
        {
            if (treeDto.Children.Count > 0)
            {
                foreach (var c in treeDto.Children)
                {
                    await MenuTreeDtos(c, lstMenuTree);
                }
            }
            lstMenuTree.Add(treeDto);
        }

        private async Task WriteMenuSubItemAsync(MenuDto p_item, List<MenuItem> menuItems)
        {
            foreach (var s_menu in menuItems.Where(p => p.ParentMenuItem == p_item.Id))//遍历该父节点的子节点
            {
                var s_item = ObjectMapper.Map<MenuItem, MenuDto>(s_menu);
                p_item.SubMenuItems.Add(s_item);//放入子节点中

                if (menuItems.Where(p => p.ParentMenuItem == s_item.Id).Count() > 0)//再遍历该孙节点
                {
                    await WriteMenuSubItemAsync(s_item, menuItems);
                }
            }
        }

        private async Task<List<MenuItem>> GetCurrentUserMenuItemAsync(bool isMenuItem = true)
        {
            if (CurrentUser.IsAuthenticated == false)
            {
                return new List<MenuItem>();
            }

            var list = await AsyncExecuter.ToListAsync(from ug in _userMenuGroups where ug.UserId == CurrentUser.Id select ug.MenuGroupId);//获取当前用户所拥有的角色组
            list.Add(CurrentUser.Id.Value);//把菜单组ID和用户ID组成菜单拥有者列表
            var lstMenuIds = await AsyncExecuter.ToListAsync(from m in _menuGrantsRepo where list.Contains(m.OwnerId) select m.MenuId);

            var menuItems = await AsyncExecuter.ToListAsync(from m in Repository where lstMenuIds.Contains(m.Id) && m.IsMenuItem == isMenuItem select m);
            return menuItems;
        }

        private async Task WriteMenuChildrenAsync(MenuTreeDto p_item, List<MenuTreeDto> menuItems)
        {
            foreach (var s_menu in menuItems.Where(p => p.ParentMenuItem == p_item.Id))//遍历该父节点的子节点
            {
                if (menuItems.Where(p => p.ParentMenuItem == s_menu.Id).Count() > 0)//再遍历该孙节点
                {
                    await WriteMenuChildrenAsync(s_menu, menuItems);
                }

                p_item.Children.Add(s_menu);//放入子节点中
            }
        }

        private async Task AddSubItemAsync(MenuItemDto menuItem, List<MenuItemDto> menuItemDtos)
        {
            var subItems = menuItemDtos.Where(p => p.ParentMenuItem == menuItem.Id).ToList();
            if (subItems.Count > 0)
            {
                foreach (var item in subItems)
                {
                    await AddSubItemAsync(item, menuItemDtos);
                    menuItem.Children.Add(item);
                }
            }

        }


        #endregion

    }
}
