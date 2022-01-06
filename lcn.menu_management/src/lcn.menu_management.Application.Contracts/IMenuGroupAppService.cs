using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace lcn.menu_management
{
    public interface IMenuGroupAppService : ICrudAppService<MenuGroupDto, Guid, PagedAndSortedResultRequestDto, MenuGroupCreateDto, MenuGroupUpdateDto>
    {
        Task<List<MenuGroupDto>> Query(GetMenuGroupByUser input);
        Task AddUser2MenuGroupAsync(AddUser2MenuGroup input);
    }
}
