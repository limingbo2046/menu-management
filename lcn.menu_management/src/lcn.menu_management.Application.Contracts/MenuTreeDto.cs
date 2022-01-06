using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace lcn.menu_management
{
    public class MenuTreeDto : EntityDto<Guid>
    {
        public MenuTreeDto()
        {
            Children = new List<MenuTreeDto>();
        }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentMenuItem { get; set; }

        /// <summary>
        /// Display name of the menu item.
        /// 显示的菜单名字
        /// </summary>

        public string DisplayName { get; set; }

        /// <summary>
        /// The Display order of the menu.
        /// Default value: 1000.
        /// 排列顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 是否是菜单
        /// </summary>
        public bool IsMenuItem { get; set; }
        /// <summary>
        /// 是否已经拥有
        /// </summary>
        public bool IsGrant { get; set; }
        /// <summary>
        /// 是菜单组范围
        /// </summary>
        public bool IsGroupGrant { get; set; }

        public List<MenuTreeDto> Children { get; set; }
    }
}
