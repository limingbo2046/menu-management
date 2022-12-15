using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace lcn.menu_management
{
    public class MenuGroupAppService : CrudAppService<MenuGroup, MenuGroupDto, Guid, MenuGroupRequestDto, MenuGroupCreateDto, MenuGroupUpdateDto>, IMenuGroupAppService
    {
        protected IRepository<UserMenuGroups> _UserMenuGroupsRepo;
        public MenuGroupAppService(
            IRepository<MenuGroup, Guid> menuGroups,
            IRepository<UserMenuGroups> userMenuGroups
            ) : base(menuGroups)
        {
            _UserMenuGroupsRepo = userMenuGroups;
        }
        public async override Task<PagedResultDto<MenuGroupDto>> GetListAsync(MenuGroupRequestDto input)
        {
            var menu_group_list = await AsyncExecuter.ToListAsync(Repository.WhereIf(!String.IsNullOrWhiteSpace(input.MenuGroupName), p => p.Name.Contains(input.MenuGroupName.Trim())));
            var list = ObjectMapper.Map<List<MenuGroup>, List<MenuGroupDto>>(menu_group_list);
            return new PagedResultDto<MenuGroupDto>(list.Count, list);
        }
        public async Task<List<MenuGroupDto>> Query(GetMenuGroupByUser input)
        {
            CurrentTenant.Change(input.TenantId);

            var user_menugroups = await AsyncExecuter.ToListAsync(
                from um in _UserMenuGroupsRepo
                where
                um.UserId == input.UserId

                select um.MenuGroupId);//在用户与菜单组的关联关系集合中获取菜单组的ID

            var result = await AsyncExecuter.ToListAsync(Repository.Where(p => user_menugroups.Contains(p.Id)));//在ID集合中获取菜单组

            return ObjectMapper.Map<List<MenuGroup>, List<MenuGroupDto>>(result);
        }


        public async Task AddUser2MenuGroupAsync(AddUser2MenuGroup input)
        {

            CurrentTenant.Change(input.TenantId);//自己找用户的租户ID很难，只好通过传递进来
            var groupIds = await AsyncExecuter.ToListAsync(_UserMenuGroupsRepo.Where(p => p.UserId == input.UserId).Select(p => p.MenuGroupId));
            //var userGroups = _UserMenuGroupsRepo.Where(p => p.UserId == input.UserId).ToList();
            //var groupIds = userGroups.Select(p => p.MenuGroupId).ToList();
            var removeGroup = groupIds.Except(input.MenuGroupIds).ToList();//找出被排除的菜单组
            if (removeGroup.Count > 0)//删掉一部分
            {
                foreach (var item in removeGroup)
                {
                    await _UserMenuGroupsRepo.DeleteAsync(p => p.MenuGroupId == item && p.UserId == input.UserId);
                }
            }
            var addGroup = input.MenuGroupIds.Except(groupIds).ToList();
            //取差集

            if (addGroup.Count > 0) //添加一部分
            {
                foreach (var id in addGroup)
                {
                    await _UserMenuGroupsRepo.InsertAsync(new UserMenuGroups(id, input.UserId, input.TenantId));
                }
            }
        }
    }
}
