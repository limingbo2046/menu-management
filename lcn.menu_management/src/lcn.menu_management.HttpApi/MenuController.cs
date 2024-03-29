﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("api/v1/MenuManagement")]
    public class MenuController : menu_managementController
    {
        private readonly IMenuAppService appService;

        public MenuController(IMenuAppService appService)
        {
            this.appService = appService;
        }

        [HttpPost]
        
        public async Task<MenuItemDto> CreateAsync(MenuItemCreateDto input)
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
        public async Task<MenuItemDto> GetAsync(Guid id)
        {
            return await appService.GetAsync(id);
        }
        [HttpGet]
        [Route("all")]
        public async Task<PagedResultDto<MenuItemDto>> GetListAsync(MenuItemRequestDto input)
        {
            return await appService.GetListAsync(input);
        }
        [HttpPost]
        [Route("GrantMenuItem")]
        public async Task GrantMenuAsync(GrantMenu input)
        {
            await appService.GrantMenuAsync(input);
        }

        [HttpPost]
        [Route("AssignedTenant2MenuItem")]
        public async Task AssignedTenant2MenuItemAsync(AssignedTenant2MenuItem input)
        {
            await appService.AssignedTenant2MenuItemAsync(input);
        }


        [HttpGet]
        [Route(nameof(GetCurrentUserMenus))]
        public async Task<List<MenuDto>> Query(GetCurrentUserMenus query)
        {
            return await appService.Query(query);
        }
        [HttpGet]
        [Route(nameof(GetCurrentUserButtons))]
        public async Task<List<MenuButtonDto>> Query(GetCurrentUserButtons query)
        {
            return await appService.Query(query);
        }
        [HttpGet]
        [Route(nameof(GetMenuTree))]
        public async Task<List<MenuTreeDto>> Query(GetMenuTree query)
        {
            return await appService.Query(query);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<MenuItemDto> UpdateAsync(Guid id, MenuItemUpdateDto input)
        {
            return await appService.UpdateAsync(id, input);
        }
    }
}
