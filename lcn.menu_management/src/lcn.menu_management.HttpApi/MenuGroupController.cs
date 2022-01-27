using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace lcn.menu_management
{
    [RemoteService]
    [Route("api/v1/MenuGroup")]
    public class MenuGroupController : menu_managementController
    {
        private readonly IMenuGroupAppService appService;

        public MenuGroupController(IMenuGroupAppService appService)
        {
            this.appService = appService;
        }
        [HttpPost]
        [Route("AddUser2MenuGroup")]
        public async Task AddUser2MenuGroupAsync(AddUser2MenuGroup input)
        {
            await appService.AddUser2MenuGroupAsync(input);
        }
        [HttpPost]
        //[Route(nameof(CreateAsync))]
        public async Task<MenuGroupDto> CreateAsync(MenuGroupCreateDto input)
        {
            return await appService.CreateAsync(input);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await appService.DeleteAsync(id);
        }
        [HttpGet]
        //[Route(nameof(GetAsync))]
        public async Task<MenuGroupDto> GetAsync(Guid id)
        {
            return await appService.GetAsync(id);
        }
        [HttpGet]
        [Route("all")]
        public async Task<PagedResultDto<MenuGroupDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await appService.GetListAsync(input);
        }

        [HttpGet]
        [Route(nameof(GetMenuGroupByUser))]
        public async Task<List<MenuGroupDto>> Query(GetMenuGroupByUser input)
        {
            return await appService.Query(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<MenuGroupDto> UpdateAsync(Guid id, MenuGroupUpdateDto input)
        {
            return await appService.UpdateAsync(id, input);
        }
    }
}
