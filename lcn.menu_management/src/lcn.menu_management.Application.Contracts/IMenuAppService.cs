using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;

namespace lcn.menu_management
{
    public interface IMenuAppService : ICrudAppService<MenuItemDto, Guid, MenuItemRequestDto, MenuItemCreateDto, MenuItemUpdateDto>
    {
        Task<List<MenuDto>> Query(GetCurrentUserMenus query);
        Task<List<MenuButtonDto>> Query(GetCurrentUserButtons query);
        Task<List<MenuTreeDto>> Query(GetMenuTree query);
        Task GrantMenuAsync(GrantMenu input);
       Task AssignedTenant2MenuItemAsync(AssignedTenant2MenuItem input);
    }
}
