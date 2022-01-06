using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace lcn.menu_management
{
    public class MenusProfile : Profile
    {
        public MenusProfile()
        {
            CreateMap<MenuItem, MenuItemDto>().ForMember(p => p.Children, t => t.Ignore());
            CreateMap<MenuItemCreateDto, MenuItem>(MemberList.Source);
            CreateMap<MenuItemUpdateDto, MenuItem>(MemberList.Source);

            CreateMap<MenuItem, MenuButtonDto>(MemberList.Destination);
            CreateMap<MenuItem, MenuDto>(MemberList.Destination)
                .ForMember(p=>p.SubMenuItems,t=>t.Ignore());

            CreateMap<MenuGroup, MenuGroupDto>();
            CreateMap<MenuGroupCreateDto, MenuGroup>(MemberList.Source);
            CreateMap<MenuGroupUpdateDto, MenuGroup>(MemberList.Source);

            CreateMap<MenuItem, MenuTreeDto>(MemberList.Destination)
                .ForMember(p => p.IsGroupGrant, t => t.Ignore())
                .ForMember(p => p.IsGrant, t => t.Ignore())
                .ForMember(p => p.Children, t => t.Ignore());

        }
    }
}
